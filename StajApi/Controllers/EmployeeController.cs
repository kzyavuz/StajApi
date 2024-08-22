using BusinessLayer.Abstract;
using DTO.DepperContext;
using DTO.DTOs.EmployeeDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace StajApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly Context _context;

        public EmployeeController(IEmployeeRepository employeeRepository, Context context)
        {
            _employeeRepository = employeeRepository;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeList()
        {
            var values = await _employeeRepository.GetAllEmployeeAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeUpdateList()
        {
            var values = await _employeeRepository.GetUpdateEmployeeAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeActiveList()
        {
            var values = await _employeeRepository.GetActiveEmployeeAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeDeleteList()
        {
            var values = await _employeeRepository.GetDeleteEmployeeAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> EmployeePassiveList()
        {
            var values = await _employeeRepository.GetPassiveEmployeeAsync();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult EmployeeDetailsList(EmployeeIDDto employeeIDDto)
        {
            var values = _employeeRepository.GetDetailsEmployee(employeeIDDto);
            return Ok(values);
        }


        [HttpPost]
        public async Task<IActionResult> EmployeeWorksList(EmployeeIDDto employeeIDDto)
        {
            var values = await _employeeRepository.GetWorkEmployee(employeeIDDto);
            return Ok(values);
        }


        [HttpPost]
        public async Task<IActionResult> EmployeeConvertStatusActive(ConvertEmployeeStatusDto convertEmployeeStatusDto)
        {
            var values = await _employeeRepository.ConvertStatusActive(convertEmployeeStatusDto);

            if (values)
            {
                return Ok(new { message = "Personel durumu aktif olarak değisti" });
            }

            return StatusCode(500, new { message = "Personel durumu değisiken hata oldu!" });
        }
        
        [HttpPost]
        public async Task<IActionResult> MyPasswordEdit(UpdtaeMyPasswordDto updtaeMyPasswordDto)
        {
            updtaeMyPasswordDto.EmployeeID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (updtaeMyPasswordDto.EmployeeID == null)
            {
                return Unauthorized();
            }
            var values = await _employeeRepository.UpdateMyPassword(updtaeMyPasswordDto);

            if (values)
            {
                return Ok(new { message = "Şifre başarılı bir şekilde değisti" });
            }

            return StatusCode(500, new { message = "Şifre değisirken hata oldu!" });
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeConvertStatusPassive(ConvertEmployeeStatusDto convertEmployeeStatusDto)
        {
            var values = await _employeeRepository.ConvertStatusPassive(convertEmployeeStatusDto);

            if (values)
            {
                return Ok(new { message = "Personel durumu pasif olarak değisti" });
            }

            return StatusCode(500, new { message = "Personel durumu değisiken hata oldu!" });
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
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(new { Errors = errors });
            }

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
