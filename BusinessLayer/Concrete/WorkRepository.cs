using BusinessLayer.Abstract;
using Dapper;
using DTO.DepperContext;
using DTO.DTOs.WorkDTO;
namespace BusinessLayer.Concrete
{
    public class WorkRepository : IWorkRepository
    {
        private readonly Context _context;

        public WorkRepository(Context context)
        {
            _context = context;
        }

        public async Task<bool> AddWorkAsync(CreateWorkDto createWorkDto)
        {
            string query = "insert into Work (WorkName,WorkDescription, WorkPrice, District, City, WorkLocal, WorkEmployeeCount, EmployeeID, Status,CreateDateTime, ActiveDateTime) values (@workName,@workDescription, @workPrice, @district, @city, @workLocal, @workEmployeeCount, @employeeID, @status, @createDateTime, @activeDateTime)";
            var paremeters = new DynamicParameters();
            paremeters.Add("@workName", createWorkDto.WorkName);
            paremeters.Add("@workDescription", createWorkDto.WorkDescription);
            paremeters.Add("@workPrice", createWorkDto.WorkPrice);
            paremeters.Add("@district", createWorkDto.District);
            paremeters.Add("@city", createWorkDto.City);
            paremeters.Add("@workLocal", createWorkDto.WorkLocal);
            paremeters.Add("@workEmployeeCount", createWorkDto.WorkEmployeeCount);
            paremeters.Add("@employeeID", createWorkDto.EmployeeID);
            paremeters.Add("@createDateTime", DateTime.Now);
            paremeters.Add("@activeDateTime", DateTime.Now);
            paremeters.Add("@status", true);
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    await connection.ExecuteAsync(query, paremeters);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteWorkAsync(DeleteWorkDto deleteWorkDto)
        {
            string query = "update Work set Status = 0, PassiveDateTime = @passiveDateTime where WorkID = @workID";
            var paremeters = new DynamicParameters();
            paremeters.Add("@workID", deleteWorkDto.WorkID);
            paremeters.Add("@passiveDateTime", DateTime.Now);
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    await connection.ExecuteAsync(query, paremeters);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public async Task<List<ResultWorkDto>> GetAllWorkAsync()
        {
            string query = "Select WorkID, WorkName, WorkPrice, District, City, CreateDateTime, EmployeeID from Work order by CreateDateTime desc";
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
            string query = "Select * from Work where Status = 1 order by ActiveDateTime desc";
            using (var connections = _context.CreateConnection())
            {
                var values = await connections.QueryAsync<ResultWorkDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultWorkDto>> GetPassiveWorkAsync()
        {
            string query = "Select * from Work where Status = 0 order by PassiveDateTime desc";
            using (var connections = _context.CreateConnection())
            {
                var values = await connections.QueryAsync<ResultWorkDto>(query);
                return values.ToList();
            }
        }

        public async Task<bool> UpdateWorkAsync(UpdateWorkDto updateWorkDto)
        {
            string query = "update work set WorkName = @workName, WorkDescription = @workDescription, WorkPrice = @workPrice, District = @district, City = @city, WorkLocal = @workLocal, WorkEmployeeCount = @workEmployeeCount, EmployeeID = @employeeID, Status = @status where WorkID = @workID";
            var paremeters = new DynamicParameters();
            paremeters.Add("@workName", updateWorkDto.WorkName);
            paremeters.Add("@workDescription", updateWorkDto.WorkDescription);
            paremeters.Add("@workPrice", updateWorkDto.WorkPrice);
            paremeters.Add("@district", updateWorkDto.District);
            paremeters.Add("@city", updateWorkDto.City);
            paremeters.Add("@workLocal", updateWorkDto.WorkLocal);
            paremeters.Add("@workEmployeeCount", updateWorkDto.WorkEmployeeCount);
            paremeters.Add("@employeeID", updateWorkDto.EmployeeID);
            paremeters.Add("@workID", updateWorkDto.WorkID);
            paremeters.Add("@status", updateWorkDto.Status);
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    await connection.ExecuteAsync(query, paremeters);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
