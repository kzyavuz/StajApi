
using DTO.DTOs.RolesDTO;

namespace BusinessLayer.Abstract
{
    public interface IRoleRepository
    {
        Task<List<ResultRoleDto>> GetAllRoleAsync();
        void CreateRole(CreateRoleDto createRoleDto);
        void DeleteRole(int id);
        void UpdateRole(UpdateRoleDto updateRoleDto);
    }
}
