using Dapper;
using BusinessLayer.Abstract;
using DTO.DepperContext;
using DTO.DTOs.EmployeeDTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Concrete
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly Context _context;

        public EmployeeRepository(IConfiguration configuration, Context context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<bool> SignUp(CreateEmployeeDto createEmployeeDto)
        {
            // Şifreyi hash'le
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(createEmployeeDto.Password);

            // SQL sorgusunu hazırla
            var query = "INSERT INTO Employee(EmployeeName, EmployeeSurName, UserName, Email, Password, ProfileImage, Status, CreateDateTime, ActiveDateTime) VALUES(@name, @surname, @userName, @mail, @password, @profileImage, @status, @createDateTime, @activeDateTime)";

            // Parametreleri oluştur
            var parameters = new DynamicParameters();
            parameters.Add("@name", createEmployeeDto.EmployeeName);
            parameters.Add("@surname", createEmployeeDto.EmployeeSurName);
            parameters.Add("@userName", createEmployeeDto.UserName);
            parameters.Add("@mail", createEmployeeDto.Email);
            parameters.Add("@password", passwordHash);
            parameters.Add("@status", true);
            parameters.Add("@createDateTime", DateTime.Now);
            parameters.Add("@activeDateTime", DateTime.Now);

            if (createEmployeeDto.ProfilePicture != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await createEmployeeDto.ProfilePicture.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();
                    parameters.Add("@profileImage", Convert.ToBase64String(imageBytes));
                }
            }
            // Veritabanı bağlantısını oluştur ve sorguyu çalıştır
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    await connection.ExecuteAsync(query, parameters);
                    return true;
                }
                catch (Exception ex)
                {
                    // Hata durumunda false döndür
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }


        public async Task<bool> DeleteEmployee(DeleteEmployeeDto deleteEmployeeDto)
        {
            string query = "UPDATE Employee SET Status = 0, PassiveDateTime = @passiveDateTime WHERE EmployeeID = @EmployeeID";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeID", deleteEmployeeDto.EmployeeID);
            parameters.Add("@passiveDateTime", DateTime.Now);
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    await connection.ExecuteAsync(query, parameters);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public ResultEmployeeDto GetDetailsEmployee(int id)
        {
            string query = "Select * From Employee where EmployeeID = @employeeID";
            var parameters = new DynamicParameters();
            parameters.Add("employeeID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<ResultEmployeeDto>(query, parameters);
                return values;
            }
        }
    
    public async Task<List<ResultEmployeeDto>> GetAllEmployeeAsync()
        {
            string query = "Select * From Employee Order By CreateDateTime Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEmployeeDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultEmployeeDto>> GetActiveEmployeeAsync()
        {
            string query = "Select * From Employee Where Status = 1 Order By ActiveDateTime Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEmployeeDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultEmployeeDto>> GetPassiveEmployeeAsync()
        {
            string query = "Select * From Employee Where Status = 0 Order By PassiveDateTime Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEmployeeDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultEmployeeDto>> GetUpdateEmployeeAsync()
        {
            string query = "Select * From Employee Where UpdateDateTime IS NOT NULL Order By UpdateDateTime Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEmployeeDto>(query);
                return values.ToList();
            }
        }

        public async Task<bool> UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(updateEmployeeDto.Password);

            var query = new StringBuilder( "Update Employee Set EmployeeName = @employeeName, EmployeeSurName = @employeeSurName, Email = @email, UpdateDateTime = @updateDateTime, UserName = @userName");
            var parameters = new DynamicParameters();

            parameters.Add("@EmployeeID", updateEmployeeDto.EmployeeID);
            parameters.Add("@employeeName", updateEmployeeDto.EmployeeName);
            parameters.Add("@EmployeeSurName",updateEmployeeDto.EmployeeSurName);
            parameters.Add("@email", updateEmployeeDto.Email);
            parameters.Add("@updateDateTime", DateTime.Now);
            parameters.Add("@userName", updateEmployeeDto.UserName);
            if (updateEmployeeDto.Password.Length < 60)
            {
                query.Append(", Password = @password");
                parameters.Add("@password", passwordHash);
            }
            query.Append(" where EmployeeID = @employeeID");

            using (var connection = _context.CreateConnection())
            {
                try
                {
                    await connection.ExecuteAsync(query.ToString(), parameters);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<IActionResult> SignIn(LoginDto loginDto)
        {
            var sql = "SELECT * FROM Employee WHERE Email = @Email";

            var parameters = new DynamicParameters();
            parameters.Add("@Email", loginDto.Email);

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<ResultEmployeeDto>(sql, parameters);

                if (user != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                {
                    var token = GenerateJwtToken(user);
                    return new OkObjectResult(new { token });
                }

                return new UnauthorizedResult();
            }
        }

        private string GenerateJwtToken(ResultEmployeeDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.EmployeeID.ToString()),
                    new Claim(ClaimTypes.Name, user.EmployeeName),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(0.5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
