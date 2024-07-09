using StajApi.DTOs;

namespace StajApi.Models.Repositories.DealerRepository
{
    public interface IWorkRepository
    {
        Task<List<ResultWorkDto>> GetAllDealerAsync();
        void AddWorkAsync(CreateWorkDto createWorkDto);
        void UpdateWorkAsync(UpdateWorkDto updateWorkDto);
        void DeleteWorkAsync(int id);

    }
}
