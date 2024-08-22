

using DTO.DTOs.EmployeeDTO;
using DTO.DTOs.WorkDTO;

namespace BusinessLayer.Abstract
{
    public interface IWorkRepository
    {
        Task<List<ResultWorkDto>> GetAllWorkAsync();
        Task<List<ResultWorkDto>> GetActiveWorkAsync();
        Task<List<ResultWorkDto>> GetPassiveWorkAsync();
        Task<List<ResultWorkDto>> GetDeleteWorkAsync();
        Task<ResultWorkDto> GetDetailsWorkAsync(WorkIDDto workIDDto);

        public Task<bool> AddWorkAsync(CreateWorkDto createWorkDto);
        public Task<bool> UpdateWorkAsync(UpdateWorkDto updateWorkDto);
        public Task<bool> DeleteWorkAsync(WorkIDDto workIDDto);

        public Task<bool> ConvertStatusPassive(ConverStatusWorkDto converStatusWorkDto);
        public Task<bool> ConvertStatusActive(ConverStatusWorkDto converStatusWorkDto);

    }
}
