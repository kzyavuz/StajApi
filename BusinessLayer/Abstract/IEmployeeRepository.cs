using DTO.DTOs.EmployeeDTO;
using DTO.DTOs.WorkDTO;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Abstract
{
    public interface IEmployeeRepository
    {
        Task<List<ResultEmployeeDto>> GetAllEmployeeAsync();
        Task<List<ResultEmployeeDto>> GetUpdateEmployeeAsync();
        Task<List<ResultEmployeeDto>> GetActiveEmployeeAsync();
        Task<List<ResultEmployeeDto>> GetPassiveEmployeeAsync();
        Task<List<ResultEmployeeDto>> GetDeleteEmployeeAsync();
        ResultEmployeeDto GetDetailsEmployee(int id);
        Task<List<ResultWorkDto>> GetWorkEmployee(int id);

        public Task<bool> ConvertStatusPassive(ConvertEmployeeStatusDto convertEmployeeStatusDto);
        public Task<bool> ConvertStatusActive(ConvertEmployeeStatusDto convertEmployeeStatusDto);
        public Task<bool> UpdateMyPassword(UpdtaeMyPasswordDto updtaeMyPasswordDto);
        public Task<bool> DeleteEmployee(DeleteEmployeeDto deleteEmployeeDto);
        public Task<bool> UpdateEmployee(UpdateEmployeeDto updateEmployeeDto);
    }
}
