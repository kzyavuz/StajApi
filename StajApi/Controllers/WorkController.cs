using BusinessLayer.Abstract;
using DTO.DTOs.WorkDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StajApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly IWorkRepository _workRepository;

        public WorkController(IWorkRepository workRepository)
        {
            _workRepository = workRepository;
        }

        [HttpPost]
        public async Task<IActionResult> ListWork()
        {
            var values = await _workRepository.GetAllWorkAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> ListWorkPassive()
        {
            var result = await _workRepository.GetPassiveWorkAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> ListDeleteWork()
        {
            var result = await _workRepository.GetDeleteWorkAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> ListWorkActive()
        {
            var result = await _workRepository.GetActiveWorkAsync();
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> WorkDetails(int id)
        {
            var values = await _workRepository.GetDetailsWorkAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWork(CreateWorkDto createWorkDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors });
            }
            var values = await _workRepository.AddWorkAsync(createWorkDto);

            if (values)
            {
                return Ok(new { Message = "İş başarıyla eklendi. {VS67}" });
            }
            else
            {
                return StatusCode(500, new { Message = "İş eklenirken hata oluştu. {VS71}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateWork(UpdateWorkDto updateWorkDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(new { errors });
            }
            var values = await _workRepository.UpdateWorkAsync(updateWorkDto);

            if (values)
            {
                return Ok(new { Message = "İş başarıyla güncellendi. {VS87}" });
            }
            else
            {
                return StatusCode(500, new { Message = "İş güncellenirken hata oluştu. {VS91}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteWork(DeleteWorkDto deleteWorkDto)
        {
            var result = await _workRepository.DeleteWorkAsync(deleteWorkDto);
            if (result)
            {
                return Ok(new { Message = "Kullanıcı başarılı bir şekilde silindi." });
            }
            else
            {
                return StatusCode(500, new { Message = "Kullanıcı silinirken hata oluştu." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> WorkConvertStatusActive(ConverStatusWorkDto converStatusWorkDto)
        {
            var values = await _workRepository.ConvertStatusActive(converStatusWorkDto);

            if (values)
            {
                return Ok(new { message = "İş durumu aktif olarak değisti" });
            }

            return StatusCode(500, new { message = "İş durumu değisiken hata oldu!" });
        }

        [HttpPost]
        public async Task<IActionResult> WorkConvertStatusPassive(ConverStatusWorkDto converStatusWorkDto)
        {
            var values = await _workRepository.ConvertStatusPassive(converStatusWorkDto);

            if (values)
            {
                return Ok(new { message = "İş durumu pasif olarak değisti" });
            }

            return StatusCode(500, new { message = "İş durumu değisiken hata oldu!" });
        }

    }
}
