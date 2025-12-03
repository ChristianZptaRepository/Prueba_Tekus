using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.Application.DTOs;
using Tekus.Application.Features.Providers;
using Tekus.Application.Features.Services;

namespace Tekus.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly CreateServiceHandler _createHandler;
        private readonly GetAllServicesHandler _getAllServicesHandler;

        public ServicesController(CreateServiceHandler createHandler, GetAllServicesHandler getAllServicesHandler)
        {
            _createHandler = createHandler;
            _getAllServicesHandler = getAllServicesHandler;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceDto>> Create([FromBody] CreateServiceRequest request)
        {
            var result = await _createHandler.Handle(request);

            return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
        }
        [HttpGet]
        public async Task<ActionResult<PaginatedListDto<ServiceDto>>> GetAll([FromQuery] GetAllServicesRequest request)
        {
            var providers = await _getAllServicesHandler.Handle(request);
            return Ok(providers);
        }
    }
}
