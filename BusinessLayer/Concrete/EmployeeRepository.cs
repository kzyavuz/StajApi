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
using Microsoft.AspNetCore.Http;
using DTO.DTOs.WorkDTO;

namespace BusinessLayer.Concrete
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly Context _context;

        public EmployeeRepository(Context context)
        {
            _context = context;
        }

        private async Task<bool> CreateConnection(string query, DynamicParameters parameters)
        {
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


        public async Task<bool> UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {

            var query = new StringBuilder("UPDATE Employee SET EmployeeName = @employeeName, EmployeeSurName = @employeeSurName, Email = @email, UpdateDateTime = @updateDateTime, UserName = @userName, RoleID = @roleID, RegistrationNumber = @registrationNumber, Birthday = @birthday, Gender = @gender, Age = @age, Unit = @unit, Class = @class, Task = @task, PhoneNumber = @phoneNumber");
            var parameters = new DynamicParameters();

            parameters.Add("@EmployeeID", updateEmployeeDto.EmployeeID);
            parameters.Add("@employeeName", updateEmployeeDto.EmployeeName);
            parameters.Add("@employeeSurName", updateEmployeeDto.EmployeeSurName);
            parameters.Add("@email", updateEmployeeDto.Email);
            parameters.Add("@updateDateTime", DateTime.Now);
            parameters.Add("@userName", updateEmployeeDto.UserName);
            parameters.Add("@roleID", updateEmployeeDto.RoleID);
            parameters.Add("@registrationNumber", updateEmployeeDto.RegistrationNumber);
            parameters.Add("@birthday", updateEmployeeDto.Birthday);
            parameters.Add("@gender", updateEmployeeDto.Gender);

            if (DateTime.Now.Year - updateEmployeeDto.Birthday.Year >= 18)
                parameters.Add("@age", DateTime.Now.Year - updateEmployeeDto.Birthday.Year);
            else
                throw new Exception("Yaş bilgisi 18 den büyük olmalıdır.");

            parameters.Add("@unit", updateEmployeeDto.Unit);
            parameters.Add("@class", updateEmployeeDto.Class);
            parameters.Add("@task", updateEmployeeDto.Task);
            parameters.Add("@phoneNumber", updateEmployeeDto.PhoneNumber);

            if (updateEmployeeDto.ProfilePicture != null)
            {
                query.Append(", ProfileImage = @profileImage");

                using (var memoryStream = new MemoryStream())
                {
                    await updateEmployeeDto.ProfilePicture.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();
                    parameters.Add("@profileImage", Convert.ToBase64String(imageBytes));
                }
            }

            if (updateEmployeeDto.Password != null)
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(updateEmployeeDto.Password);

                if (updateEmployeeDto.Password.Length > 5 && updateEmployeeDto.Password.Length < 51)
                {
                    query.Append(", Password = @password");
                    parameters.Add("@password", passwordHash);
                }
                else
                {
                    throw new Exception("Şifre uzunluğu 6 ila 50 karekter arasında olmalısır.");
                }
            }

            query.Append(" WHERE EmployeeID = @employeeID");

            return await CreateConnection(query.ToString(), parameters);
        }

        public async Task<bool> UpdateMyPassword(UpdtaeMyPasswordDto updtaeMyPasswordDto)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(updtaeMyPasswordDto.Password);
            string query = "Update Employee set Password = @password where EmployeeID = @employeeID";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeID", updtaeMyPasswordDto.EmployeeID);
            parameters.Add("@password", passwordHash);
            return await CreateConnection(query, parameters);
        }

        public async Task<bool> DeleteEmployee(DeleteEmployeeDto deleteEmployeeDto)
        {
            string query = "UPDATE Employee SET Status = @status, Status2 = @status2, DeleteDateTime = @deleteDateTime WHERE EmployeeID = @EmployeeID";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeID", deleteEmployeeDto.EmployeeID);
            parameters.Add("@deleteDateTime", DateTime.Now);
            parameters.Add("@status", false);
            parameters.Add("@status2", false);

            return await CreateConnection(query, parameters);
        }


        public async Task<bool> ConvertStatusPassive(ConvertEmployeeStatusDto convertEmployeeStatusDto)
        {
            var query = "UPDATE Employee SET Status2 = @status2, PassiveDateTime = @passiveDateTime WHERE EmployeeId = @employeeId";

            var parameters = new DynamicParameters();
            parameters.Add("@status2", false);
            parameters.Add("@employeeId", convertEmployeeStatusDto.EmployeeID);
            parameters.Add("@passiveDateTime", DateTime.Now);

            return await CreateConnection(query, parameters);
        }


        public async Task<bool> ConvertStatusActive(ConvertEmployeeStatusDto convertEmployeeStatusDto)
        {
            var query = "UPDATE Employee SET Status2 = @status2, ActiveDateTime = @activeDateTime WHERE EmployeeId = @employeeId";

            var parameters = new DynamicParameters();
            parameters.Add("@status2", true);
            parameters.Add("@employeeId", convertEmployeeStatusDto.EmployeeID);
            parameters.Add("@activeDateTime", DateTime.Now);

            return await CreateConnection(query, parameters);
        }


        public ResultEmployeeDto GetDetailsEmployee(EmployeeIDDto employeeIDDto)
        {
            string query = "Select * From Employee where EmployeeID = @employeeID";
            var parameters = new DynamicParameters();
            parameters.Add("employeeID", employeeIDDto.EmployeeID);
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<ResultEmployeeDto>(query, parameters);
                return values;
            }
        }

        public async Task<List<ResultWorkDto>> GetWorkEmployee(EmployeeIDDto employeeIDDto)
        {
            string query = "Select * From Work where EmployeeID = @employeeID";
            var parameters = new DynamicParameters();
            parameters.Add("employeeID", employeeIDDto.EmployeeID);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultWorkDto>(query, parameters);
                return values.ToList();
            }
        }

        public async Task<List<ResultEmployeeDto>> GetAllEmployeeAsync()
        {
            string query = "Select * From Employee where Status = @status and CreateDateTime is not null Order By CreateDateTime Desc";
            var parameters = new DynamicParameters();
            parameters.Add("status", 1);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEmployeeDto>(query, parameters);
                return values.ToList();
            }
        }

        public async Task<List<ResultEmployeeDto>> GetActiveEmployeeAsync()
        {
            string query = "Select * From Employee Where Status2 = 1 and Status = 1 and ActiveDateTime is not null Order By ActiveDateTime Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEmployeeDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultEmployeeDto>> GetPassiveEmployeeAsync()
        {
            string query = "Select * From Employee Where Status2 = 0 and Status = 1 and PassiveDateTime is not null Order By PassiveDateTime Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEmployeeDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultEmployeeDto>> GetDeleteEmployeeAsync()
        {
            string query = "Select * From Employee Where Status = 0 and DeleteDateTime is not null Order By DeleteDateTime Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEmployeeDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultEmployeeDto>> GetUpdateEmployeeAsync()
        {
            string query = "Select * From Employee Where UpdateDateTime IS NOT NULL and Status = 1 Order By UpdateDateTime Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEmployeeDto>(query);
                return values.ToList();
            }
        }
    }
}
