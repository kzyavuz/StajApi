using BusinessLayer.Abstract;
using DTO.DTOs.EmployeeDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace StajApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAcountRepository _acountRepository;

        public AccountController(IAcountRepository acountRepository)
        {
            _acountRepository = acountRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _acountRepository.SignIn(loginDto);
            return result;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Register(CreateEmployeeDto createEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(new { Errors = errors });
            }

            try
            {
                var result = await _acountRepository.SignUp(createEmployeeDto);

                if (result)
                {
                    return Ok(new { message = "Personel başarıyla eklendi" });
                }

                return StatusCode(500, new { message = "Personel eklenirken hata oluştu!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var user = await _acountRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetUserWorkProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var user = await _acountRepository.GetUserWorkByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
