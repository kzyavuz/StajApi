using DTO.DTOs.EmployeeDTO;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Abstract
{
    public interface IEmployeeRepository
    {
        Task<List<ResultEmployeeDto>> GetAllEmployeeAsync();
        Task<List<ResultEmployeeDto>> GetUpdateEmployeeAsync();
        Task<List<ResultEmployeeDto>> GetActiveEmployeeAsync();
        Task<List<ResultEmployeeDto>> GetPassiveEmployeeAsync();

        Task<IActionResult> SignIn(LoginDto loginDto);

        public Task<bool> SignUp(CreateEmployeeDto createEmployeeDto);
        public Task<bool> DeleteEmployee(DeleteEmployeeDto deleteEmployeeDto);
        public Task<bool> UpdateEmployee(UpdateEmployeeDto updateEmployeeDto);
    }
}
