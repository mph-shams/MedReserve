using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MedReserve.Application.DTOs.Schedules;
using MediatR;

namespace MedReserve.Application.Features.Schedules.Commands.CreateSchedule;

public class CreateScheduleCommand : IRequest<Result<int>>
{
    public int UserId { get; set; }
    public CreateScheduleRequest Data { get; set; } = new();
}

public class CreateScheduleHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CreateScheduleCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateScheduleCommand request, CancellationToken ct)
    {
        var doctor = (await _unitOfWork.Repository<Doctor>().GetAllAsync())
            .FirstOrDefault(d => d.UserId == request.UserId);

        if (doctor == null)
            return Result<int>.Failure("Medical profile not found for this user.");

        if (!TimeSpan.TryParse(request.Data.StartTime, out var start) ||
            !TimeSpan.TryParse(request.Data.EndTime, out var end))
        {
            return Result<int>.Failure("Invalid time format (e.g., 08:00).");
        }

        var schedule = new Schedule
        {
            DoctorId = doctor.Id,
            DayOfWeek = request.Data.DayOfWeek,
            StartTime = start,
            EndTime = end,
            SlotDuration = 30 
        };

        await _unitOfWork.Repository<Schedule>().AddAsync(schedule);
        var success = await _unitOfWork.SaveChangesAsync(ct) > 0;

        return success ? Result<int>.Success(schedule.Id) : Result<int>.Failure("An error occurred while saving the schedule.");
    }
}