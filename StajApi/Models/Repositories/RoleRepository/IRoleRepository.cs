using StajApi.DTOs.EMployeeDTO;
using StajApi.DTOs.RolesDTO;

namespace StajApi.Models.Repositories.RoleRepository
{
    public interface IRoleRepository
    {
        Task<List<ResultRoleDto>> GetAllRoleAsync();
        void CreateRole(CreateRoleDto createRoleDto);
        void DeleteRole(int id);
        void UpdateRole(UpdateRoleDto updateRoleDto);
    }
}
