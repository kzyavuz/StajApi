using Dapper;
using StajApi.DTOs.UserRoleDTO;
using StajApi.Models.DepperContext;

namespace StajApi.Models.Repositories.UserRoleRepository
{
    public class UserRoleRepository: IUserRoleRepository
    {
        private readonly Context _context;

        public UserRoleRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultUserRoleDto>> GetAllUserRoleAsync()
        {
            string query = "Select * From UserRole";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultUserRoleDto>(query);
                return values.ToList();
            }
        }
    }
}
