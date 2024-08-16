
using DTO.DTOs.RolesDTO;

namespace BusinessLayer.Abstract
{
    public interface IRoleRepository
    {
        public Task<List<ResultRoleDto>> GetAllRoleAsync();
        public Task CreateRoleAsync(CreateRoleDto createRoleDto);
        public Task DeleteRoleAsync(int id);
        public Task UpdateRoleAsync(UpdateRoleDto updateRoleDto);

        public Task<string> GetUserRoleNameAsync(int userId);
        public Task<int> GetUserRoleIdAsync(int userId);

    }
}
