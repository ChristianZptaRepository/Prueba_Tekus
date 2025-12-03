using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.Application.DTOs;
using Tekus.Application.Features.Providers;
using Tekus.Domain.Interfaces;

namespace Tekus.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        private readonly CreateProviderHandler _createHandler;
        private readonly GetAllProvidersHandler _getAllProvidersHandler;
        private readonly IProviderRepository _repository;

        public ProvidersController(CreateProviderHandler createHandler, IProviderRepository repository, GetAllProvidersHandler getAllProvidersHandler)
        {
            _createHandler = createHandler;
            _repository = repository;
            _getAllProvidersHandler = getAllProvidersHandler;
        }

        [HttpPost]
        public async Task<ActionResult<ProviderDto>> Create([FromBody] CreateProviderRequest request)
        {
            var result = await _createHandler.Handle(request);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedListDto<ProviderDto>>> GetAll([FromQuery] GetAllProvidersRequest request)
        {
            var providers = await _getAllProvidersHandler.Handle(request);
            return Ok(providers);
        }
    }
}
