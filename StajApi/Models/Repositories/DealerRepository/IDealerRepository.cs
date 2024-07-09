using StajApi.DTOs;

namespace StajApi.Models.Repositories.DealerRepository
{
    public interface IDealerRepository
    {
        Task<List<ResaultDealerDto>> GetAllDealerAsync();
        void CreateDealer(CreateDealerDto createDealerDto);
        void DeleteDealer(int id);
        void UpdateDealer(UpdateDealerDto updateDealerDto);
    }
}
