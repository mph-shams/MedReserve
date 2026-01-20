using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Appointments.Commands.CancelAppointment;

public record CancelAppointmentCommand(int Id) : IRequest<Result<bool>>;

public class CancelAppointmentHandler : IRequestHandler<CancelAppointmentCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    public CancelAppointmentHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<bool>> Handle(CancelAppointmentCommand request, CancellationToken ct)
    {
        var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(request.Id);
        if (appointment == null) return Result<bool>.Failure("Appointment not found.");

        appointment.Status = AppointmentStatus.Cancelled; 
        _unitOfWork.Repository<Appointment>().Update(appointment);
        await _unitOfWork.SaveChangesAsync(ct);
        return Result<bool>.Success(true);
    }
}