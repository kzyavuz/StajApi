using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StajApi.DTOs.WorkDTO;
using StajApi.Models.Repositories.WorkRepository;

namespace StajApi.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost]
        public async Task<IActionResult> CreateWork(CreateWorkDto createWorkDto)
        {
            _workRepository.AddWorkAsync(createWorkDto);
            return Ok("İş atama işlemi basarılı");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWork(UpdateWorkDto updateWorkDto)
        {
            _workRepository.UpdateWorkAsync(updateWorkDto);
            return Ok("İş atama güncellemesi başarılı");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWork(int id)
        {
            _workRepository.DeleteWorkAsync(id);
            return Ok("Silme işlemi başarılı");
        }

    }
}
