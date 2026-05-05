using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NecroFinances.Application.Dtos;
using NecroFinances.Application.Interfaces;
using System.Security.Claims;

namespace NecroFinances.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatrimonioController : ControllerBase
    {
        private readonly IPatrimonioService _patrimonioService;

        public PatrimonioController(
            IPatrimonioService patrimonioService
            )
        {
            _patrimonioService = patrimonioService;
        }

        [Authorize]
        [HttpGet("GetPatrimonioByDate")]
        public async Task<IActionResult> GetPatrimonioByDate(DateTime inicio, DateTime fim)
        {
            string user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!long.TryParse(user, out long userID))
            {
                return Unauthorized();
            }

            if (inicio > fim)
            {
                return BadRequest("Data inválida");
            }

            return Ok(await _patrimonioService.GetPatrimonioByDate(inicio, fim, userID));
        }

        [Authorize]
        [HttpPost("UpdatePatrimonio")]
        public async Task<IActionResult> UpdatePatrimonio([FromBody] PatrimonioDTO dto)
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

            return Ok(await _patrimonioService.UpdatePatrimonio(dto, userID));
        }
    }
}
