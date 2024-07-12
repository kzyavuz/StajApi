using DTO.DTOs.EmployeeDTO;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Abstract
{
    public interface IEmployeeRepository
    {
        Task<List<ResultEmployeeDto>> GetAllEmployeeAsync();
        void SignUp(CreateEmployeeDto createEmployeeDto);
        Task<IActionResult> SignIn(LoginDto loginDto);
        void DeleteEmployee(int id);
        void UpdateEmployee(UpdateEmployeeDto updateEmployeeDto);
    }
}
