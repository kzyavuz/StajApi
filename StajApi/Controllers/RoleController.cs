using BusinessLayer.Abstract;
using DTO.DTOs.RolesDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace StajApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;

        public RoleController(IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

        [HttpGet("UserRoleList")]
        public async Task<IActionResult> UserRoleList()
        {
            var values = await _userRoleRepository.GetAllUserRoleAsync();
            return Ok(values);
        }

        [HttpGet("RoleList")]
        public async Task<IActionResult> RoleList()
        {
            var values = await _roleRepository.GetAllRoleAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDto createRoleDto)
        {
            _roleRepository.CreateRole(createRoleDto);
            return Ok("Basrılı bir sekilde Rol ekleme işlemi gerçkelstirildi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRole(int id)
        {
            _roleRepository.DeleteRole(id);
            return Ok("Rol basarılı bir şekilde silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto updateRoleDto)
        {
            _roleRepository.UpdateRole(updateRoleDto);
            return Ok("Rol başarılı bir şekilde güncellendi");
        }
    }
}
