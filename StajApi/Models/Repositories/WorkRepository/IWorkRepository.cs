using StajApi.DTOs.WorkDTO;

namespace StajApi.Models.Repositories.WorkRepository
{
    public interface IWorkRepository
    {
        Task<List<ResultWorkDto>> GetAllWorkAsync();
        void AddWorkAsync(CreateWorkDto createWorkDto);
        void UpdateWorkAsync(UpdateWorkDto updateWorkDto);
        void DeleteWorkAsync(int id);

    }
}
