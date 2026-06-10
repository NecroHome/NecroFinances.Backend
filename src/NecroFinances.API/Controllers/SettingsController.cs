using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NecroFinances.Application.Dtos;
using NecroFinances.Application.Interfaces;
using NecroFinances.Application.Models;
using System.Security.Claims;

namespace NecroFinances.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(
            ISettingsService settingsService
            )
        {
            _settingsService = settingsService;
        }

        [Authorize]
        [HttpGet("GetSettingsByDate")]
        public async Task<IActionResult> GetSettingsByDate(DateOnly inicio, DateOnly fim)
        {
            string user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!long.TryParse(user, out long userID))
            {
                return Unauthorized();
            }

            if (inicio > fim)
            {
                return BadRequest("Dados inválidos.");
            }

            return Ok(await _settingsService.GetSettingsByDate(inicio, fim, userID));
        }

        [Authorize]
        [HttpPost("UpdateSettings")]
        public async Task<IActionResult> UpdateSettings([FromBody] SettingsDTO dto)
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

            return Ok(await _settingsService.UpdateSettings(dto, userID));
        }
    }
}
