using BusinessLayer.Abstract;
using Dapper;
using DTO.DepperContext;
using DTO.DTOs.UserRoleDTO;

namespace BusinessLayer.Concrete
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
