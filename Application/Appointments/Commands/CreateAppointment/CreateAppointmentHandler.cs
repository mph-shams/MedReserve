using Application.Appointments.Commands.CreateAppointment;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using MediatR;

public class CreateAppointmentHandler : IRequestHandler<CreateAppointmentCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateAppointmentHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<int>> Handle(CreateAppointmentCommand request, CancellationToken ct)
    {
        var doctor = await _unitOfWork.Repository<Doctor>().GetByIdAsync(request.DoctorId);
        if (doctor == null)
        {
            return Result<int>.Failure("پزشکی با این مشخصات یافت نشد. دقت کنید که باید ID جدول Doctors را ارسال کنید.");
        }

        var appointment = new Appointment
        {
            DoctorId = request.DoctorId,
            PatientId = request.PatientId,
            AppointmentDate = request.AppointmentDate,
            Status = AppointmentStatus.Pending
        };

        await _unitOfWork.Repository<Appointment>().AddAsync(appointment);
        await _unitOfWork.SaveChangesAsync(ct); 

        return Result<int>.Success(appointment.Id);
    }
}