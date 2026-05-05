using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NecroFinances.Application.Dtos.Settings;
using NecroFinances.Application.Interfaces;
using System.Security.Claims;

namespace NecroFinances.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MesController : ControllerBase
    {
        private readonly IMesService _mesService;
        public MesController(
            IMesService mesService
            ) 
        { 
            _mesService = mesService;
        }

        [Authorize]
        [HttpGet("GetMesByDate")]
        public async Task<IActionResult> GetMesByDate(DateTime inicio, DateTime fim)
        {
            string user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!long.TryParse(user, out long userID))
            {
                return Unauthorized();
            }

            if(inicio > fim)
            {
                return BadRequest("Dada inválida");
            }

            return Ok(await _mesService.GetMesByDate(inicio, fim, userID));
        }

        [Authorize]
        [HttpPut("UpdateMes")]
        public async Task<IActionResult> UpdateMes([FromBody] MesDTO dto)
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

            return Ok(await _mesService.UpdateMes(dto, userID));
        }
    }
}
