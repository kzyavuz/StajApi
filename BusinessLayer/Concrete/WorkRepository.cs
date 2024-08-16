using BusinessLayer.Abstract;
using Dapper;
using DTO.DepperContext;
using DTO.DTOs.EmployeeDTO;
using DTO.DTOs.WorkDTO;
using Microsoft.AspNetCore.Identity;
using System.Text;
namespace BusinessLayer.Concrete
{
    public class WorkRepository : IWorkRepository
    {
        private readonly Context _context;

        public WorkRepository(Context context)
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
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public async Task<bool> UpdateWorkAsync(UpdateWorkDto updateWorkDto)
        {
            var query = new StringBuilder("update Work set WorkName = @workName, WorkDescription = @workDescription, WorkPrice = @workPrice, District = @district, City = @city, WorkLocal = @workLocal, UpdateDateTime = @updateDateTime, EmployeeID = @employeeID where WorkID = @workID");
            var parameters = new DynamicParameters();
            parameters.Add("@workName", updateWorkDto.WorkName);
            parameters.Add("@workDescription", updateWorkDto.WorkDescription);
            parameters.Add("@workPrice", updateWorkDto.WorkPrice);
            parameters.Add("@district", updateWorkDto.District);
            parameters.Add("@city", updateWorkDto.City);
            parameters.Add("@workLocal", updateWorkDto.WorkLocal);
            parameters.Add("@updateDateTime", DateTime.Now);
            parameters.Add("@employeeID", updateWorkDto.EmployeeID);
            parameters.Add("@workID", updateWorkDto.WorkID);

            return await CreateConnection(query.ToString(), parameters);
        }

        public async Task<bool> ConvertStatusPassive(ConverStatusWorkDto converStatusWorkDto)
        {
            var query = "UPDATE Work SET Status2 = @status2, PassiveDateTime = @passiveDateTime WHERE WorkID = @workID";

            var parameters = new DynamicParameters();
            parameters.Add("@status2", 0);
            parameters.Add("@workID", converStatusWorkDto.WorkID);
            parameters.Add("@passiveDateTime", DateTime.Now);

            return await CreateConnection(query, parameters);
        }

        public async Task<bool> ConvertStatusActive(ConverStatusWorkDto converStatusWorkDto)
        {
            var query = "UPDATE Work SET Status2 = @status2, ActiveDateTime = @activeDateTime WHERE WorkID = @workID";

            var parameters = new DynamicParameters();
            parameters.Add("@status2", 1);
            parameters.Add("@workID", converStatusWorkDto.WorkID);
            parameters.Add("@activeDateTime", DateTime.Now);

            return await CreateConnection(query, parameters);
        }

        public async Task<bool> AddWorkAsync(CreateWorkDto createWorkDto)
        {
            string query = "insert into Work (WorkName,WorkDescription, WorkPrice, District, City, WorkLocal, EmployeeID, Status, CreateDateTime, ActiveDateTime, Status2) values (@workName,@workDescription, @workPrice, @district, @city, @workLocal, @employeeID, @status, @createDateTime, @activeDateTime, @status2)";
            var paremeters = new DynamicParameters();
            paremeters.Add("@workName", createWorkDto.WorkName);
            paremeters.Add("@workDescription", createWorkDto.WorkDescription);
            paremeters.Add("@workPrice", createWorkDto.WorkPrice);
            paremeters.Add("@district", createWorkDto.District);
            paremeters.Add("@city", createWorkDto.City);
            paremeters.Add("@workLocal", createWorkDto.WorkLocal);
            paremeters.Add("@employeeID", createWorkDto.EmployeeID);
            paremeters.Add("@createDateTime", DateTime.Now);
            paremeters.Add("@activeDateTime", DateTime.Now);
            paremeters.Add("@status", true);
            paremeters.Add("@status2", true);

            return await CreateConnection(query, paremeters);
        }

        public async Task<bool> DeleteWorkAsync(DeleteWorkDto id)
        {
            string query = "update Work set Status = 0, Status2 = 0, DeleteDateTime = @deleteDateTime where WorkID = @workID";
            var parameters = new DynamicParameters();
            parameters.Add("@workID", id.WorkID);
            parameters.Add("@deleteDateTime", DateTime.Now);

            return await CreateConnection(query, parameters);
        }

        public async Task<List<ResultWorkDto>> GetAllWorkAsync()
        {
            string query = "SELECT w.WorkID, w.WorkName, w.City, w.District, e.EmployeeName, e.EmployeeSurName, w.CreateDateTime FROM Work w LEFT JOIN Employee e ON w.employeeID = e.employeeID ORDER BY w.CreateDateTime DESC;";
            using (var connections = _context.CreateConnection())
            {
                var values = await connections.QueryAsync<ResultWorkDto>(query);
                return values.ToList();
            }
        }


        public async Task<ResultWorkDto> GetDetailsWorkAsync(int id)
        {
            string query = "Select * from Work where WorkID = @workID";
            var parameters = new DynamicParameters();
            parameters.Add("@workID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<ResultWorkDto>(query, parameters);
                return values;
            }
        }

        public async Task<List<ResultWorkDto>> GetActiveWorkAsync()
        {
            string query = "SELECT w.WorkID, w.WorkName, w.City, w.District, e.EmployeeName, e.EmployeeSurName, w.ActiveDateTime FROM Work w LEFT JOIN Employee e ON w.employeeID = e.employeeID where w.Status2 = 1 and w.Status = 1 ORDER BY w.ActiveDateTime DESC;";

            using (var connections = _context.CreateConnection())
            {
                var values = await connections.QueryAsync<ResultWorkDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultWorkDto>> GetUpdateWorkAsync()
        {
            string query = "SELECT w.WorkID, w.WorkName, w.City, w.District, e.EmployeeName, e.EmployeeSurName, w.UpdateDateTime FROM Work w LEFT JOIN Employee e ON w.employeeID = e.employeeID where w.Status2 = 1 and w.Status = 1 and w.UpdateDateTime is not null ORDER BY w.UpdateDateTime DESC;";

            using (var connections = _context.CreateConnection())
            {
                var values = await connections.QueryAsync<ResultWorkDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultWorkDto>> GetPassiveWorkAsync()
        {
            string query = "SELECT w.WorkID, w.WorkName, w.City, w.District, e.EmployeeName, e.EmployeeSurName, w.PassiveDateTime FROM Work w LEFT JOIN Employee e ON w.employeeID = e.employeeID where w.Status2 = 0 and w.Status = 1 ORDER BY w.PassiveDateTime DESC;";

            using (var connections = _context.CreateConnection())
            {
                var values = await connections.QueryAsync<ResultWorkDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultWorkDto>> GetDeleteWorkAsync()
        {
            string query = "SELECT w.WorkID, w.WorkName, w.City, w.District, e.EmployeeName, e.EmployeeSurName, w.DeleteDateTime FROM Work w LEFT JOIN Employee e ON w.employeeID = e.employeeID where w.Status = 0 ORDER BY w.DeleteDateTime DESC;";

            using (var connections = _context.CreateConnection())
            {
                var values = await connections.QueryAsync<ResultWorkDto>(query);
                return values.ToList();
            }
        }


    }
}
