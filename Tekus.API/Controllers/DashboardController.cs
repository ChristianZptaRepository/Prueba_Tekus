using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.Application.Features.Dashboard;

namespace Tekus.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly GetDashboardStatsHandler _handler;

        public DashboardController(GetDashboardStatsHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public async Task<IActionResult> GetSummary()
        {
            var result = await _handler.Handle();
            return Ok(result);
        }
    }
}
