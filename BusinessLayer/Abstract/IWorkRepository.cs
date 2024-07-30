

using DTO.DTOs.WorkDTO;

namespace BusinessLayer.Abstract
{
    public interface IWorkRepository
    {
        Task<List<ResultWorkDto>> GetAllWorkAsync();
        Task<List<ResultWorkDto>> GetActiveWorkAsync();
        Task<List<ResultWorkDto>> GetPassiveWorkAsync();
        Task<ResultWorkDto> GetDetailsWorkAsync(int id);

        public Task<bool> AddWorkAsync(CreateWorkDto createWorkDto);
        public Task<bool> UpdateWorkAsync(UpdateWorkDto updateWorkDto);
        public Task<bool> DeleteWorkAsync(DeleteWorkDto deleteWorkDto);

    }
}
