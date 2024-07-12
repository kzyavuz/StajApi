

using DTO.DTOs.UserRoleDTO;

namespace BusinessLayer.Abstract
{
    public interface IUserRoleRepository
    {
        Task<List<ResultUserRoleDto>> GetAllUserRoleAsync();
    }
}
