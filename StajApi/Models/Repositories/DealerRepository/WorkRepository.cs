using Dapper;
using StajApi.DTOs;
using StajApi.Models.DepperContext;

namespace StajApi.Models.Repositories.DealerRepository
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
            string query = "insert into Work (WorkName, EmployeeID, DealerID) values (@workName, @dealerID, @employeeID)";
            var paremeters = new DynamicParameters();
            paremeters.Add("@workName", createWorkDto.WorkName) ;
            paremeters.Add("@employeeID", createWorkDto.EmployeID) ;
            paremeters.Add("@dealerID", createWorkDto.DealerID);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, paremeters);
            }
        }

        public void DeleteWorkAsync(int id)
        {
            string query = "delete from Work where WorkID = @workID";
            var paremeters = new DynamicParameters();
            paremeters.Add("@workID", )
        }

        public Task<List<ResultWorkDto>> GetAllDealerAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateWorkAsync(UpdateWorkDto updateWorkDto)
        {
            throw new NotImplementedException();
        }
    }
}
