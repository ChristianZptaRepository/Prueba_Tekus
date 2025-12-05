using MediatR;
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
        private readonly IMediator _mediator;
        private readonly CreateProviderHandler _createHandler;
        private readonly GetAllProvidersHandler _getAllProvidersHandler;
        private readonly IProviderRepository _repository;

        public ProvidersController(CreateProviderHandler createHandler, IProviderRepository repository, GetAllProvidersHandler getAllProvidersHandler, IMediator mediator)
        {
            _createHandler = createHandler;
            _repository = repository;
            _getAllProvidersHandler = getAllProvidersHandler;
            _mediator = mediator;
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
        [HttpPut("{id}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(404)] // Not Found
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProviderCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("El ID de la URL no coincide con el cuerpo de la solicitud.");
            }

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
