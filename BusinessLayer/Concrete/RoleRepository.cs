using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using Dapper;
using DTO.DepperContext;
using DTO.DTOs.RolesDTO;

namespace BusinessLayer.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        private readonly Context _context;

        public RoleRepository(Context context)
        {
            _context = context;
        }

        public async Task CreateRoleAsync(CreateRoleDto createRoleDto)
        {
            string query = "INSERT INTO Roles (RoleName, Status) VALUES (@roleName, @status)";
            var parameters = new DynamicParameters();
            parameters.Add("@roleName", createRoleDto.RoleName);
            parameters.Add("@status", true);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteRoleAsync(int id)
        {
            string query = "DELETE FROM Roles WHERE RoleID = @roleID";
            var parameters = new DynamicParameters();
            parameters.Add("@roleID", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultRoleDto>> GetAllRoleAsync()
        {
            string query = "SELECT * FROM Roles";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultRoleDto>(query);
                return values.ToList();
            }
        }

        public async Task UpdateRoleAsync(UpdateRoleDto updateRoleDto)
        {
            string query = "UPDATE Roles SET RoleName = @roleName, Status = @status WHERE RoleID = @roleID";
            var parameters = new DynamicParameters();
            parameters.Add("@roleID", updateRoleDto.RoleID);
            parameters.Add("@roleName", updateRoleDto.RoleName);
            parameters.Add("@status", updateRoleDto.Status);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        // Rolün Adını Dön
        public async Task<string> GetUserRoleNameAsync(int userId)
        {
            var sql = @"SELECT r.Name
                        FROM Employees e
                        JOIN Roles r ON e.RoleID = r.RoleID
                        WHERE e.Id = @UserId";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QuerySingleOrDefaultAsync<string>(sql, new { UserId = userId });
                return values;
            }
        }

        //Rollün ID sini dön
        public async Task<int> GetUserRoleIdAsync(int userId)
        {
            var sql = @"SELECT RoleId
                        FROM Employees
                        WHERE Id = @UserId";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.ExecuteScalarAsync<int>(sql, new { UserId = userId });
                return (int)values;
            }
        }
    }
}
