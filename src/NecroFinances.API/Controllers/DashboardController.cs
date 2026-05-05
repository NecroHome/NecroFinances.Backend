using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NecroFinances.Application.Interfaces;
using System.Security.Claims;

namespace NecroFinances.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(
            IDashboardService dashboardService
            )
        {
            _dashboardService = dashboardService;
        }
        [Authorize]
        [HttpGet("GetDashboard")]
        public async Task<IActionResult> GetDashboard(DateTime inicio, DateTime fim)
        {
            string user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!long.TryParse(user, out long userID))
            {
                return Unauthorized();
            }
            
            if (inicio > fim)
            {
                return BadRequest("Data inválida.");
            }

            return Ok(await _dashboardService.GetDashboard(inicio, fim, userID));
        }
    }
}
