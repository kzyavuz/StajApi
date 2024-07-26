using DTO.DTOs.DealerDTO;

namespace BusinessLayer.Abstract

{
    public interface IDealerRepository
    {
        Task<List<ResaultDealerDto>> GetAllDealerAsync();
        void CreateDealer(CreateDealerDto createDealerDto);
        void DeleteDealer(int id);
        void UpdateDealer(UpdateDealerDto updateDealerDto);
    }
}
