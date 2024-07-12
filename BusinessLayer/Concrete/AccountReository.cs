
using BusinessLayer.Abstract;

namespace BusinessLayer.Concrete
{
    public class AccountReository : IAccountRepository
    {
        //private readonly Context _context;
        //private readonly IConfiguration _configuration;

        //public AccountReository(Context context, IConfiguration configuration)
        //{
        //    _context = context;
        //    _configuration = configuration;
        //}

        //public async Task<IActionResult> SignIn(SignInDto signUpDto)
        //{

        //    var passwordHash = BCrypt.Net.BCrypt.HashPassword(signUpDto.Password);
        //    var sql = "INSERT INTO Users (Username, Email, PasswordHash, Role) VALUES (@Username, @Email, @PasswordHash, @Role)";

        //    using (var connection = _context.CreateConnection())
        //    {
        //        var result = await connection.ExecuteAsync(sql, new { model.Username, model.Email, PasswordHash = passwordHash, Role = "User" });
        //        return Ok(result);
        //    }
        //}

        //public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
