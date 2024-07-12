

using DTO.DTOs.WorkDTO;

namespace BusinessLayer.Abstract
{
    public interface IWorkRepository
    {
        Task<List<ResultWorkDto>> GetAllWorkAsync();
        void AddWorkAsync(CreateWorkDto createWorkDto);
        void UpdateWorkAsync(UpdateWorkDto updateWorkDto);
        void DeleteWorkAsync(int id);

    }
}
