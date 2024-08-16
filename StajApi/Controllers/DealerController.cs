using BusinessLayer.Abstract;
using DTO.DTOs.DealerDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StajApi.Controllers
{
    [Route("api/[controller]/[action]")]
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

        //[HttpPost]
        //public async Task<IActionResult> CreateDealer(CreateDealerDto createDealerDto)
        //{
        //    _dealerRepository.CreateDealer(createDealerDto);
        //    return Ok("Basrılı bir sekilde ekleme işlemi gerçkelstirildi");
        //}

        //[HttpPost]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    _dealerRepository.DeleteDealer(id); 
        //    return Ok("Bayi basarılı bir şekilde silindi");
        //}

        //[HttpPost]
        //public async Task<IActionResult> UpdateDealer(UpdateDealerDto updateDealerDto)
        //{
        //    _dealerRepository.UpdateDealer(updateDealerDto);
        //    return Ok("Bayi başarılı bir şekilde güncellendi");
        //}

    }
}
