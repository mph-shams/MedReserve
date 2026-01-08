using MedReserve.Application.Features.Doctors.Commands.CreateDoctorInfo;
using MediatR;
using MedReserve.Application.Features.Doctors.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedReserve.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DoctorsController(IMediator mediator) => _mediator = mediator;

        [Authorize(Roles = "Doctor")]
        [HttpPost("profile")]
        public async Task<IActionResult> UpsertProfile(CreateDoctorInfoCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string specialty)
        {
            var result = await _mediator.Send(new GetDoctorsBySpecialtyQuery(specialty));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var result = await _mediator.Send(new GetDoctorDetailsQuery(id));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllDoctorsQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
