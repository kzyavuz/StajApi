using StajApi.DTOs.UserRoleDTO;

namespace StajApi.Models.Repositories.UserRoleRepository
{
    public interface IUserRoleRepository
    {
        Task<List<ResultUserRoleDto>> GetAllUserRoleAsync();
    }
}
