using BusinessLayer.Abstract;
using DTO.DTOs.EmployeeDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StajApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeList()
        {
            var values = await _employeeRepository.GetAllEmployeeAsync();
            return Ok(values);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateEmployeeDto createEmployeeDto)
        {
            _employeeRepository.SignUp(createEmployeeDto);
            return Ok(" Kayıt işlemi başrılı bir şekilde gerçkelştirildi.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            _employeeRepository.SignIn(loginDto);
            return Ok(" Giriş işlemi başrılı bir şekilde gerçkelştirildi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            _employeeRepository.DeleteEmployee(id);
            return Ok("Personel basarılı bir şekilde silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
            _employeeRepository.UpdateEmployee(updateEmployeeDto);
            return Ok("Personel başarılı bir şekilde güncellendi");
        }

    }
}
