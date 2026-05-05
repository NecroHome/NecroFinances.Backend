using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NecroFinances.Application.Dtos;
using NecroFinances.Application.Interfaces;
using System.Security.Claims;

namespace NecroFinances.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GastosController : ControllerBase
    {
        private readonly IGastosService _gastosService;
        public GastosController(
            IGastosService gastosService)
        {
            _gastosService = gastosService;
        }

        [Authorize]
        [HttpPost("AddGasto")]
        public async Task<IActionResult> AddGasto([FromBody] GastosDTO dto)
        {
            string user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!long.TryParse(user, out long userID))
            {
                return Unauthorized();
            }

            if (dto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            return Ok(await _gastosService.AddGasto(dto, userID));
        }

        [Authorize]
        [HttpPut("UpdateGasto")]
        public async Task<IActionResult> UpdateGasto([FromBody] GastosDTO dto)
        {
            string user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!long.TryParse(user, out long userID))
            {
                return Unauthorized();
            }

            if (dto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            return Ok(await _gastosService.UpdateGasto(dto, userID));
        }

        [Authorize]
        [HttpDelete("DeleteGasto")]
        public async Task<IActionResult> DeleteGasto(long gastoId)
        {
            string user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!long.TryParse(user, out long userID))
            {
                return Unauthorized();
            }

            return Ok(await _gastosService.DeleteGasto(gastoId, userID));
        }
    }
}
