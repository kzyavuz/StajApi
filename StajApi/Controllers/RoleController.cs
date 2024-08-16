using BusinessLayer.Abstract;
using DTO.DTOs.RolesDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace StajApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> RoleList()
        {
            var values = await _roleRepository.GetAllRoleAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDto createRoleDto)
        {
            await _roleRepository.CreateRoleAsync(createRoleDto);
            return Ok("Basrılı bir sekilde Rol ekleme işlemi gerçkelstirildi.");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _roleRepository.DeleteRoleAsync(id);
            return Ok("Rol basarılı bir şekilde silindi");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto updateRoleDto)
        {
            await _roleRepository.UpdateRoleAsync(updateRoleDto);
            return Ok("Rol başarılı bir şekilde güncellendi");
        }
    }
}
