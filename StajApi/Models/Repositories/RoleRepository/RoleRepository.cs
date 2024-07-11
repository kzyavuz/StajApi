using Dapper;
using StajApi.DTOs.EMployeeDTO;
using StajApi.DTOs.RolesDTO;
using StajApi.Models.DepperContext;

namespace StajApi.Models.Repositories.RoleRepository
{
    public class RoleRepository: IRoleRepository
    {

        private readonly Context _context;

        public RoleRepository(Context context)
        {
            _context = context;
        }
        public async void CreateRole(CreateRoleDto createRoleDto)
        {
            string query = "insert into Roles (RoleName, Status) values (@roleName, @status)";
            var parameters = new DynamicParameters();
            parameters.Add("@roleName", createRoleDto.RoleName);
            parameters.Add("@status", true);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteRole(int id)
        {
            string query = "Delete From Roles where RoleID = @roleID";
            var parameters = new DynamicParameters();
            parameters.Add("@roleID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultRoleDto>> GetAllRoleAsync()
        {
            string query = "Select * From Roles";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultRoleDto>(query);
                return values.ToList();
            }
        }

        public async void UpdateRole(UpdateRoleDto updateRoleDto)
        {
            string query = "Update Roles Set RoleName = @roleName, Status = @status where RoleID = @roleID";
            var parameters = new DynamicParameters();

            parameters.Add("@roleID", updateRoleDto.RoleID);
            parameters.Add("@roleName", updateRoleDto.RoleName);
            parameters.Add("@status", updateRoleDto.Status);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

    }
}
