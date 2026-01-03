using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using MedReserve.Application.Features.Appointments.Commands.CreateAppointment;

namespace Application.Features.Appointments.Commands.CreateAppointment{ 

    public class CreateAppointmentHandler : IRequestHandler<CreateAppointmentCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateAppointmentHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<Result<int>> Handle(CreateAppointmentCommand request, CancellationToken ct)
        {
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
}