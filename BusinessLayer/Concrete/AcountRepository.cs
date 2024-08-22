using BusinessLayer.Abstract;
using Dapper;
using DTO.DepperContext;
using DTO.DTOs.EmployeeDTO;
using DTO.DTOs.WorkDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Concrete
{
    public class AcountRepository : IAcountRepository
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public AcountRepository(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ResultEmployeeDto> GetUserByIdAsync(string userId)
        {
            string query = "SELECT * FROM Employee WHERE EmployeeID = @UserId";
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var user = await connection.QuerySingleOrDefaultAsync<ResultEmployeeDto>(query, parameters);
                    return user;
                }
                catch (Exception ex)
                {

                    return null;
                }
            }
        }

        public async Task<List<ResultWorkDto>> GetUserWorkByIdAsync(string userId)
        {
            string query = "SELECT * FROM Work WHERE EmployeeID = @UserId and Status = @status order by CreateDateTime desc";
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            parameters.Add("@status", 1);

            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var user = await connection.QueryAsync<ResultWorkDto>(query, parameters);
                    return user.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public async Task<bool> SignUp(CreateEmployeeDto createEmployeeDto)
        {
            using (var connection = _context.CreateConnection())
            {
                var userNameExists = await connection.QueryFirstOrDefaultAsync<bool>(
                    "SELECT COUNT(1) FROM Employee WHERE UserName = @userName",
                    new { userName = createEmployeeDto.UserName });

                if (userNameExists)
                {
                    throw new Exception("Bu kullanıcı adı zaten kayıtlı.");
                }

                var emailExists = await connection.QueryFirstOrDefaultAsync<bool>(
                    "SELECT COUNT(1) FROM Employee WHERE Email = @mail",
                    new { mail = createEmployeeDto.Email });

                if (emailExists)
                {
                    throw new Exception("Bu e-posta zaten kayıtlı.");
                }

                var registrationNumberExists = await connection.QueryFirstOrDefaultAsync<bool>(
                    "SELECT COUNT(1) FROM Employee WHERE RegistrationNumber = @registrationNumber",
                    new { registrationNumber = createEmployeeDto.RegistrationNumber });

                if (registrationNumberExists)
                {
                    throw new Exception("Bu TC kimlik numarası zaten kayıtlı.");
                }

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(createEmployeeDto.Password);

                var query = "INSERT INTO Employee(EmployeeName, EmployeeSurName, UserName, Email, Password, ProfileImage, Status, Status2, CreateDateTime, ActiveDateTime, RoleID, RegistrationNumber, Birthday, Gender, Age, Unit, Class, Task, PhoneNumber) VALUES(@name, @surname, @userName, @mail, @password, @profileImage, @status, @status2, @createDateTime, @activeDateTime, @roleID, @registrationNumber, @birthday, @gender, @age, @unit, @class, @task, @phoneNumber)";

                var parameters = new DynamicParameters();
                parameters.Add("@name", createEmployeeDto.EmployeeName);
                parameters.Add("@surname", createEmployeeDto.EmployeeSurName);
                parameters.Add("@userName", createEmployeeDto.UserName);
                parameters.Add("@mail", createEmployeeDto.Email);
                parameters.Add("@password", passwordHash);
                parameters.Add("@status", true);
                parameters.Add("@status2", true);
                parameters.Add("@createDateTime", DateTime.Now);
                parameters.Add("@activeDateTime", DateTime.Now);
                parameters.Add("@roleID", 1);
                parameters.Add("@registrationNumber", createEmployeeDto.RegistrationNumber);
                parameters.Add("@birthday", createEmployeeDto.Birthday);
                parameters.Add("@gender", createEmployeeDto.Gender);

                if (DateTime.Now.Year - createEmployeeDto.Birthday.Year >= 18)
                    parameters.Add("@age", DateTime.Now.Year - createEmployeeDto.Birthday.Year);
                else
                    throw new Exception("Yaş bilgisi 18 den büyük olmalıdır.");

                parameters.Add("@unit", createEmployeeDto.Unit);
                parameters.Add("@class", createEmployeeDto.Class);
                parameters.Add("@task", createEmployeeDto.Task);
                parameters.Add("@phoneNumber", createEmployeeDto.PhoneNumber);

                if (createEmployeeDto.ProfilePicture != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await createEmployeeDto.ProfilePicture.CopyToAsync(memoryStream);
                        var imageBytes = memoryStream.ToArray();
                        parameters.Add("@profileImage", Convert.ToBase64String(imageBytes));
                    }
                }

                try
                {
                    await connection.ExecuteAsync(query, parameters);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }


        public async Task<IActionResult> SignIn(LoginDto loginDto)
        {
            try
            {
                var sql = "SELECT * FROM Employee WHERE Email = @email and Status2 = @status2 and Status = @status";
                var parameters = new DynamicParameters();
                parameters.Add("@email", loginDto.Email);
                parameters.Add("@status2", 1);
                parameters.Add("@status", 1);

                using (var connection = _context.CreateConnection())
                {
                    var user = await connection.QueryFirstOrDefaultAsync<ResultEmployeeDto>(sql, parameters);

                    if (user != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                    {
                        var token = GenerateJwtToken(user);
                        var roleID = user.RoleID;
                        return new OkObjectResult(new { token, roleID });
                    }

                    return new UnauthorizedResult();
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        private string GenerateJwtToken(ResultEmployeeDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("JWT Key is not configured properly.");
            }
            var keyBytes = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.EmployeeID.ToString()),
                    new Claim(ClaimTypes.Role, user.RoleID.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
