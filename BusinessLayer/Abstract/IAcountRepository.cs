using DTO.DTOs.EmployeeDTO;
using DTO.DTOs.WorkDTO;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BusinessLayer.Abstract
{
    public interface IAcountRepository
    {
        public Task<ResultEmployeeDto> GetUserByIdAsync(string userId);
        public Task<List<ResultWorkDto>> GetUserWorkByIdAsync(string userId);
        public Task<IActionResult> SignIn(LoginDto loginDto);
        public Task<bool> SignUp(CreateEmployeeDto createEmployeeDto);
    }
}
