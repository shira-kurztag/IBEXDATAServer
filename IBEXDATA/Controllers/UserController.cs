using IBEXDATA.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
       

        [Route("GetAllAdmin")]
        [HttpGet]
        public async Task<IActionResult>  GetAllAdmin()
        {
            var admin = await _userService.GetAllAdmin();
            return Ok(admin);
        }

    }
}
