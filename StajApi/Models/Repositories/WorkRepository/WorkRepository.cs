using Dapper;
using StajApi.DTOs.WorkDTO;
using StajApi.Models.DepperContext;

namespace StajApi.Models.Repositories.WorkRepository
{
    public class WorkRepository : IWorkRepository
    {
        private readonly Context _context;

        public WorkRepository(Context context)
        {
            _context = context;
        }

        public async void AddWorkAsync(CreateWorkDto createWorkDto)
        {
            string query = "insert into Work (WorkName, EmployeeID, DealerID, Status) values (@workName, @employeeID, @dealerID, @status)";
            var paremeters = new DynamicParameters();
            paremeters.Add("@workName", createWorkDto.WorkName);
            paremeters.Add("@employeeID", createWorkDto.EmployeeID);
            paremeters.Add("@dealerID", createWorkDto.DealerID);
            paremeters.Add("@status", createWorkDto.Status);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, paremeters);
            }
        }

        public async void DeleteWorkAsync(int id)
        {
            string query = "delete from Work where WorkID = @workID";
            var paremeters = new DynamicParameters();
            paremeters.Add("@workID", id);
            using (var connections = _context.CreateConnection())
            {
                await connections.ExecuteAsync(query, paremeters);
            }
        }

        public async Task<List<ResultWorkDto>> GetAllWorkAsync()
        {
            string query = "Select * from Work";
            using (var connections = _context.CreateConnection())
            {
                var values = await connections.QueryAsync<ResultWorkDto>(query);
                return values.ToList();
            }
        }

        public async void UpdateWorkAsync(UpdateWorkDto updateWorkDto)
        {
            string query = "update work set WorkName = @workName, DealerID = @dealerID, EmployeeID = @employeeID, Status = @status where WorkID = @workID";
            var paremeters = new DynamicParameters();
            paremeters.Add("@workName", updateWorkDto.WorkName);
            paremeters.Add("@employeeID", updateWorkDto.EmployeeID);
            paremeters.Add("@dealerID", updateWorkDto.DealerID);
            paremeters.Add("@workID", updateWorkDto.WorkID);
            paremeters.Add("@status", updateWorkDto.Status);
            using (var connections = _context.CreateConnection())
            {
                await connections.ExecuteAsync(query, paremeters);
            }
        }
    }
}
