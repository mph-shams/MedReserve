using Application.Appointments.Commands.CreateAppointment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedReserve.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AppointmentsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("my-appointments")]
        public async Task<IActionResult> GetMyList()
        {
            // var result = await _mediator.Send(new GetPatientAppointmentsQuery());
            return Ok("Patient's Appointment List");
        }

        
        [HttpPatch("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            // var result = await _mediator.Send(new CancelAppointmentCommand(id));
            return Ok("The appointment has been successfully cancelled !");
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] int status)
        {
            // var result = await _mediator.Send(new UpdateAppointmentStatusCommand(id, status));
            return Ok("The appointment status has been updated !");
        }
    }
}
