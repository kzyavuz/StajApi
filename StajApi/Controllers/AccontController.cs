//using BusinessLayer.Abstract;
//using DTO.DTOs.AccountDTO;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;

//namespace StajApi.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class AccountController : ControllerBase
//    {
//        private readonly IAccountRepository _accountRepository;

//        public AccountController(IAccountRepository accountRepository)
//        {
//            _accountRepository = accountRepository;
//        }

//        [HttpPost("signup")]
//        public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
//        {
//            var result = await _accountRepository.SignUp(signUpDto);

//            if (result)
//            {
//                return Ok(new { Message = "Kullanıcı başarıyla kaydedildi." });
//            }
//            else
//            {
//                return StatusCode(500, new { Message = "Bir hata oluştu." });
//            }
//        }
//    }
//}
