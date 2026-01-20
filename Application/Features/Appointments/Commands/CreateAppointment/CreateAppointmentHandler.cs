using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using MedReserve.Application.Features.Appointments.Commands.CreateAppointment;
using Infrastructure.Identity;

namespace Application.Features.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentHandler : IRequestHandler<CreateAppointmentCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TelegramBotService _botService;

        public CreateAppointmentHandler(IUnitOfWork unitOfWork, TelegramBotService botService)
        {
            _unitOfWork = unitOfWork;
            _botService = botService;
        }

        public async Task<Result<int>> Handle(CreateAppointmentCommand request, CancellationToken ct)
        {
            var hasConflict = (await _unitOfWork.Repository<Appointment>().GetAllAsync())
                .Any(a => a.DoctorId == request.DoctorId &&
                          a.AppointmentDate == request.AppointmentDate &&
                          a.Status != AppointmentStatus.Cancelled);

            if (hasConflict)
                return Result<int>.Failure("This time is already booked!");

            var appointment = new Appointment
            {
                DoctorId = request.DoctorId,
                PatientId = request.PatientId,
                AppointmentDate = request.AppointmentDate,
                Status = AppointmentStatus.Pending
            };

            await _unitOfWork.Repository<Appointment>().AddAsync(appointment);
            var saveResult = await _unitOfWork.SaveChangesAsync(ct);

            if (saveResult > 0)
            {
                var patient = await _unitOfWork.Repository<User>().GetByIdAsync(request.PatientId);

                if (patient?.TelegramChatId != null)
                {
                    var message = "🔔 New Appointment Confirmed!\n\n" +
                                  $"📅 Date: {appointment.AppointmentDate:yyyy-MM-dd HH:mm}\n" +
                                  $"📌 Status: {appointment.Status}\n" +
                                  "Thank you for using MedReserve.";

                    await _botService.SendNotification(patient.TelegramChatId.Value, message);
                }
            }

            return Result<int>.Success(appointment.Id);
        }
    }
}