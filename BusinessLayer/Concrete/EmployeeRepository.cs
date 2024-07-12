using Dapper;
using BusinessLayer.Abstract;
using DTO.DepperContext;
using DTO.DTOs.EmployeeDTO;

namespace BusinessLayer.Concrete
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly Context _context;

        public EmployeeRepository(Context context)
        {
            _context = context;
        }
        public async void CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(createEmployeeDto.Password);
            string query = "insert into Employee (EmployeeName, EmployeeSurName, Status, Email, Password) values (@employeeName, @employeeSurName,@status, @email, @password)";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeName", createEmployeeDto.EmployeeName);
            parameters.Add("@employeeSurName", createEmployeeDto.EmployeeSurName);
            parameters.Add("@email", createEmployeeDto.Email);
            parameters.Add("@password", createEmployeeDto.Password);
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
    }
}
