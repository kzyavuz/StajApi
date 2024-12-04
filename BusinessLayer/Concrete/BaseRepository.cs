using Dapper;
using DTO.DepperContext;
using System.Data;

namespace BusinessLayer.Concrete
{
    public class BaseRepository
    {
        protected readonly Context context;

        protected async void ExecuteParametersAsync(string query, DynamicParameters parameters)
        {
            try
            {
                using (IDbConnection connection = context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, parameters);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected async Task<List<TResult>> ExecuteListQueryAsync<TResult>(string query)
        {
            try
            {
                using (IDbConnection connection = context.CreateConnection())
                {
                    var values = await connection.QueryAsync<TResult>(query);
                    return values.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<TResult>();
            }
        }

        protected async Task<bool> ExecuteNonQueryAsync(string query, DynamicParameters parameters)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, parameters);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Context getContext()
        {
            return context;
        }
    }
}
