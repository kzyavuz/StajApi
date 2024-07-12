using DTO.DepperContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StajApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccontController : ControllerBase
    {
        private readonly Context context;
        private readonly IConfiguration _configuration;
    }
}
