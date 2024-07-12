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

        public async void SignUp(CreateEmployeeDto createEmployeeDto)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(createEmployeeDto.Password);
            string query = "insert into Employee (EmployeeName, EmployeeSurName, Status, Email, Password) values (@employeeName, @employeeSurName,@status, @email, @password)";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeName", createEmployeeDto.EmployeeName);
            parameters.Add("@employeeSurName", createEmployeeDto.EmployeeSurName);
            parameters.Add("@email", createEmployeeDto.Email);
            parameters.Add("@password", passwordHash);
            parameters.Add("@status", true);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteEmployee(int id)
        {
            string query = "Delete From Employee where EmployeeID = @employeeID";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultEmployeeDto>> GetAllEmployeeAsync()
        {
            string query = "Select * From Employee";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEmployeeDto>(query);
                return values.ToList();
            }
        }

        public async void UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
            string query = "Update Employee Set EmployeeName = @employeeName, EmployeeSurName = @employeeSurName, Status = @status, Email = @email, Password = @password where EmployeeID = @employeeID";
            var parameters = new DynamicParameters();

            parameters.Add("@employeeName", updateEmployeeDto.EmployeeName);
            parameters.Add("@EmployeeID", updateEmployeeDto.EmployeeID);
            parameters.Add("@EmployeeSurName", updateEmployeeDto.EmployeeSurName);
            parameters.Add("@email", updateEmployeeDto.Email);
            parameters.Add("@password", updateEmployeeDto.Password);
            parameters.Add("@status", updateEmployeeDto.Status);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
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
