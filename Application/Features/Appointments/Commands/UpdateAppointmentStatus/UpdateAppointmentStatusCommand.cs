using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Enums;
using MedReserve.Application.DTOs.Appointments;
using MediatR;

namespace Application.Features.Appointments.Commands.UpdateAppointmentStatus;

public record UpdateAppointmentStatusCommand(int Id, UpdateAppointmentStatusRequest Data) : IRequest<Result<bool>>;

public class UpdateAppointmentStatusHandler(IUnitOfWork _unitOfWork) : IRequestHandler<UpdateAppointmentStatusCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateAppointmentStatusCommand request, CancellationToken ct)
    {
        var appointment = await _unitOfWork.Repository<Domain.Entities.Appointment>().GetByIdAsync(request.Id);

        if (appointment == null)
            return Result<bool>.Failure("Appointment not found!");

        appointment.Status = (AppointmentStatus)request.Data.Status;

        _unitOfWork.Repository<Domain.Entities.Appointment>().Update(appointment);

        var success = await _unitOfWork.SaveChangesAsync(ct) > 0;

        return success
            ? Result<bool>.Success(true)
            : Result<bool>.Failure("An error occurred during the update!");
    }
}