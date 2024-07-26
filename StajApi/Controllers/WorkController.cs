using BusinessLayer.Abstract;
using DTO.DTOs.WorkDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StajApi.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly IWorkRepository _workRepository;

        public WorkController(IWorkRepository workRepository)
        {
            _workRepository = workRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ListWork()
        {
            var values = await _workRepository.GetAllWorkAsync();
            return Ok(values);
        }

        [HttpGet]
        public async Task<IActionResult> ListWorkPassive()
        {
            var result = await _workRepository.GetPassiveWorkAsync();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ListWorkActive()
        {
            var result = await _workRepository.GetActiveWorkAsync();
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateWork(CreateWorkDto createWorkDto)
        {
            _workRepository.AddWorkAsync(createWorkDto);
            return Ok("İş atama işlemi basarılı");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateWork(UpdateWorkDto updateWorkDto)
        {
            _workRepository.UpdateWorkAsync(updateWorkDto);
            return Ok("İş atama güncellemesi başarılı");
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
                return StatusCode(500, new { Message = "Kullanıcı silinemedi" });
            }
        }

    }
}
