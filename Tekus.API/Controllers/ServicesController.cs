using MediatR;
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
        private IMediator _mediator;
        private readonly CreateServiceHandler _createHandler;
        private readonly GetAllServicesHandler _getAllServicesHandler;

        public ServicesController(CreateServiceHandler createHandler, GetAllServicesHandler getAllServicesHandler, IMediator mediator)
        {
            _createHandler = createHandler;
            _getAllServicesHandler = getAllServicesHandler;
            _mediator = mediator;
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

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateServiceCommand command)
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
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
