using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DTO.DTOs.AccountDTO;
using DTO.DTOs.EmployeeDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StajApi.Controllers
{
    [Route("api/[controller]/[action]/")]
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

        [HttpGet]
        public async Task<IActionResult> EmployeeUpdateList()
        {
            var values = await _employeeRepository.GetUpdateEmployeeAsync();
            return Ok(values);
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeActiveList()
        {
            var values = await _employeeRepository.GetActiveEmployeeAsync();
            return Ok(values);
        }

        [HttpGet]
        public async Task<IActionResult> EmployeePassiveList()
        {
            var values = await _employeeRepository.GetPassiveEmployeeAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateEmployeeDto createEmployeeDto)
        {
            var result = await _employeeRepository.SignUp(createEmployeeDto);

            if (result)
            {
                return Ok(new { Message = "Kullanıcı başarıyla kaydedildi." });
            }
            else
            {
                return StatusCode(500, new { Message = "Bir hata oluştu." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            await _employeeRepository.SignIn(loginDto);
            return Ok(" Giriş işlemi başrılı bir şekilde gerçkelştirildi.");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(DeleteEmployeeDto deleteEmployeeDto)
        {
            var result = await _employeeRepository.DeleteEmployee(deleteEmployeeDto);
            if (result)
            {
                return Ok(new { Message = "Kullanıcı başarıyla silindi." });
            }
            else
            {
                return StatusCode(500, new { Message = "Silme işleminde hata oldu." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
            var result = await _employeeRepository.UpdateEmployee(updateEmployeeDto);
            if (result)
            {
                return Ok(new { Message = "Kullanıcı başarıyla güncellendi." });
            }
            else
            {
                return StatusCode(500, new { Message = "Güncelleme işleminde hata oldu" });
            }
        }

    }
}
