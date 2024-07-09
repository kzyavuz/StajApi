using Dapper;
using StajApi.DTOs;
using StajApi.Models.DepperContext;

namespace StajApi.Models.Repositories.DealerRepository
{
    public class DealerRepository : IDealerRepository
    {
        private readonly Context _context;

        public DealerRepository(Context context)
        {
            _context = context;
        }

        public async void CreateDealer(CreateDealerDto createDealerDto)
        {
            string query = "insert into Dealer (DealerName, DealerVariant, Status) values (@dealerName, @dealerVariant,@status)";
            var parameters = new DynamicParameters();
            parameters.Add("@dealerName", createDealerDto.DealerName);
            parameters.Add("@dealerVariant", createDealerDto.DealerVariant);
            parameters.Add("@status", true);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteDealer(int id)
        {
            string query = "Delete From Dealer where DealerID = @dealerID";
            var parameters = new DynamicParameters();
            parameters.Add("@dealerID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResaultDealerDto>> GetAllDealerAsync()
        {
            string query = "Select * From Dealer";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResaultDealerDto>(query);
                return values.ToList();
            }
        }

        public async void UpdateDealer(UpdateDealerDto updateDealerDto)
        {
            string query = "Update Dealer Set DealerName = @dealerName, DealerVariant = @dealerVariant, Status = @status where DealerID = @dealerId";
            var parameters = new DynamicParameters();
            
            parameters.Add("@dealerName", updateDealerDto.DealerName);
            parameters.Add("@dealerId", updateDealerDto.DealerID);
            parameters.Add("@dealerVariant", updateDealerDto.DealerVariant);
            parameters.Add("@status", updateDealerDto.Status);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
