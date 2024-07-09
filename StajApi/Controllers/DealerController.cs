using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StajApi.DTOs;
using StajApi.Models.Repositories.DealerRepository;

namespace StajApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerController : ControllerBase
    {
        private readonly IDealerRepository _dealerRepository;

        public DealerController(IDealerRepository dealerRepository)
        {
            _dealerRepository = dealerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> DealerList()
        {
            var values = await _dealerRepository.GetAllDealerAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDealer(CreateDealerDto createDealerDto)
        {
            _dealerRepository.CreateDealer(createDealerDto);
            return Ok("Basrılı bir sekilde ekleme işlemi gerçkelstirildi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDealer(int id)
        {
            _dealerRepository.DeleteDealer(id);
            return Ok("Bayi basarılı bir şekilde silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDealer(UpdateDealerDto updateDealerDto)
        {
            _dealerRepository.UpdateDealer(updateDealerDto);
            return Ok("Bayi başarılı bir şekilde güncellendi");
        }

    }
}
