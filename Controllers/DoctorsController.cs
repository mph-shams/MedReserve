using MediatR;
using MedReserve.Application.Features.Doctors.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MedReserve.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DoctorsController(IMediator mediator) => _mediator = mediator;

        [HttpPost("complete-profile")]
        public async Task<IActionResult> CompleteProfile(CreateDoctorInfoCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
