using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace MedReserve.Application.Features.Schedules.Commands.CreateSchedule;

public record CreateScheduleCommand(int DoctorId, DayOfWeek Day, TimeSpan StartTime, TimeSpan EndTime) : IRequest<Result<int>>;

public class CreateScheduleHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CreateScheduleCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateScheduleCommand request, CancellationToken ct)
    {
        var schedule = new Schedule
        {
            DoctorId = request.DoctorId,
            DayOfWeek = (int)request.Day,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            SlotDuration = 30 
        };

        await _unitOfWork.Repository<Schedule>().AddAsync(schedule);
        await _unitOfWork.SaveChangesAsync(ct);
        return Result<int>.Success(schedule.Id);
    }
}