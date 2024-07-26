
using DTO.DTOs.AccountDTO;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Abstract
{
    public interface IAccountRepository
    {
        //public Task< IActionResult> SignUp(SignUpDto signUpDto);
        //public Task< IActionResult> SignIn(SignInDto signInDto);

        public Task<bool> SignUp(SignUpDto signUpDto);
    }
}
