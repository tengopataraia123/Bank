using Bank.DTOs;
using Bank.Exceptions;
using Bank.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        public UserController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet(nameof(GetUser)+"/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                return Ok(await service.GetUserById(id));
            }catch(UserDoesNotExist ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(nameof(RegisterUser))]
        public async Task<IActionResult> RegisterUser(UserDTO user)
        {
            try
            {
                await service.RegisterUser(user);
                return Ok();
            }catch(UserAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
