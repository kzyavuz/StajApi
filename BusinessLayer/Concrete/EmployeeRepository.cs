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
using Newtonsoft.Json.Linq;
using DTO.DTOs.AccountDTO;

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
            var query = "INSERT INTO Employee(EmployeeName, EmployeeSurName, UserName, Email, Password, Status, CreateDateTime) VALUES(@name, @surname, @userName, @mail, @password, @status, @createDateTime)";

            // Parametreleri oluştur
            var parameters = new DynamicParameters();
            parameters.Add("@name", createEmployeeDto.EmployeeName);
            parameters.Add("@surname", createEmployeeDto.EmployeeSurName);
            parameters.Add("@userName", createEmployeeDto.UserName);
            parameters.Add("@mail", createEmployeeDto.Email);
            parameters.Add("@password", passwordHash);
            parameters.Add("@status", true);
            parameters.Add("@createDateTime", DateTime.Now);

            // Veritabanı bağlantısını oluştur ve sorguyu çalıştır
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    await connection.ExecuteAsync(query, parameters);
                    return true;
                }
                catch (Exception)
                {
                    // Hata durumunda false döndür
                    return false;
                }
            }
        }

        public async Task<bool> DeleteEmployee(DeleteEmployeeDto deleteEmployeeDto)
        {
            string query = "UPDATE Employee SET Status = 0 WHERE EmployeeID = @EmployeeID";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeID", deleteEmployeeDto.EmployeeID);
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
            string query = "Select * From Employee Where Status = 1 Order By CreateDateTime Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEmployeeDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultEmployeeDto>> GetPassiveEmployeeAsync()
        {
            string query = "Select * From Employee Where Status = 0 Order By CreateDateTime Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEmployeeDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultEmployeeDto>> GetUpdateEmployeeAsync()
        {
            string query = "Select * From Employee Where UpdateDateTime IS NOT NULL Order By CreateDateTime Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEmployeeDto>(query);
                return values.ToList();
            }
        }

        public async Task<bool> UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
            string query = "Update Employee Set EmployeeName = @employeeName, EmployeeSurName = @employeeSurName, Status = @status, Email = @email, Password = @password, UpdateDateTime = @updateDateTime where EmployeeID = @employeeID";
            var parameters = new DynamicParameters();

            parameters.Add("@employeeName", updateEmployeeDto.EmployeeName);
            parameters.Add("@EmployeeID", updateEmployeeDto.EmployeeID);
            parameters.Add("@EmployeeSurName", updateEmployeeDto.EmployeeSurName);
            parameters.Add("@email", updateEmployeeDto.Email);
            parameters.Add("@password", updateEmployeeDto.Password);
            parameters.Add("@status", updateEmployeeDto.Status);
            parameters.Add("@updateDateTime", updateEmployeeDto.UpdateDateTime);

            using (var connection = _context.CreateConnection())
            {
                try
                {
                    await connection.ExecuteAsync(query, parameters);
                    return true;
                }
                catch (Exception)
                {
                    // Hata durumunda false döndür
                    return false;
                }
            }
        }

        public async Task<IActionResult> SignIn(LoginDto loginDto)
        {
            var sql = "SELECT * FROM Employee WHERE UserName = @userName";

            var parameters = new DynamicParameters();
            parameters.Add("@userName", loginDto.UserName);

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<ResultEmployeeDto>(sql, parameters);

                if (user != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                {
                    var token = GenerateJwtToken(user);
                    return new OkObjectResult(new { token });
                }

                return new OkObjectResult(new { user });

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
