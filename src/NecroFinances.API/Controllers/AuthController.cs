using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NecroFinances.Application.Interfaces;
using NecroFinances.Application.Models;

namespace NecroFinances.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(
            IUserService userService,
            ITokenService tokenService
            )
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            try
            {
                var user = await _userService.RegisterAsync(userDTO);
                return Ok(new { message = "Usuário criado com sucesso", user.Username });
            } catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO dto)
        {
            UserModel user = await _userService.AuthenticateAsync(dto);

            if (user == null)
            {
                return Unauthorized("Usuário ou senha inválidos");
            }

            TokenResponse token = _tokenService.GenerateToken(user);

            return Ok(token);
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenResponse token)
        {

            return Ok(_tokenService.RefreshToken(token.RefreshToken));
        }

        [Authorize]
        [HttpGet("logout")]
        public IActionResult Logout(long userID)
        {
            _userService.Logout(userID);
            return Ok();
        }
    }
}
