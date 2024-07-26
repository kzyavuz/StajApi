
using BusinessLayer.Abstract;
using Dapper;
using DTO.DepperContext;
using DTO.DTOs.AccountDTO;
using DTO.DTOs.DealerDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Concrete
{
    public class AccountReository : IAccountRepository
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public AccountReository(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //public async Task<IActionResult> SignIn(SignInDto signInDto)
        //{
        //    var passwordHash = BCrypt.Net.BCrypt.HashPassword(signInDto.Password);
        //    var sql = "INSERT INTO Users (UserName, Password, Role) VALUES (@Username, @PasswordHash, @Role)";

        //    using (var connection = _context.CreateConnection())
        //    {
        //        try
        //        {
        //            await connection.ExecuteAsync(sql, new { signInDto.UserName, signInDto.Password = passwordHash, Role = "User" });
        //            return Ok(new { Message = "Kullanıcı başarıyla giriş yaptı." });
        //        }
        //        catch (Exception ex)
        //        {
        //            return StatusCode(500, new { Message = "Bir hata oluştu.", Error = ex.Message });
        //        }
        //    }
        //}

        public async Task<bool> SignUp(SignUpDto signUpDto)
        {
            // Şifreyi hash'le
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(signUpDto.Password);

            // SQL sorgusunu hazırla
            var query = @"
                INSERT INTO Users (EmplyeeName, EmplyeeSurName, UserName, Email, Password, Status) 
                VALUES (@name, @surname, @userName, @mail, @password, @status)";

            // Parametreleri oluştur
            var parameters = new DynamicParameters();
            parameters.Add("@name", signUpDto.EmplyeeName);
            parameters.Add("@surname", signUpDto.EmplyeeSurName);
            parameters.Add("@userName", signUpDto.UserName);
            parameters.Add("@mail", signUpDto.Email);
            parameters.Add("@password", passwordHash);
            parameters.Add("@status", true);

            // Veritabanı bağlantısını oluştur ve sorguyu çalıştır
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    await connection.ExecuteAsync(query, parameters);
                    return true;
                }
                catch (Exception)
                {
                    // Hata durumunda false döndür
                    return false;
                }
            }
        }
    }
}
