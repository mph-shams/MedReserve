using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MedReserve.Application.DTOs.Doctors;
using MediatR;

namespace Application.Features.Schedules.Queries.GetDoctorSchedules;

public class GetDoctorSchedulesQuery : IRequest<Result<List<ScheduleDto>>>
{
    public int DoctorId { get; set; }
}

public class GetDoctorSchedulesHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetDoctorSchedulesQuery, Result<List<ScheduleDto>>>
{
    public async Task<Result<List<ScheduleDto>>> Handle(GetDoctorSchedulesQuery request, CancellationToken ct)
    {
        var schedules = (await _unitOfWork.Repository<Schedule>().GetAllAsync())
            .Where(s => s.DoctorId == request.DoctorId)
            .OrderBy(s => s.DayOfWeek)
            .Select(s => new ScheduleDto
            {
                Id = s.Id,
                DayOfWeek = s.DayOfWeek,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                SlotDuration = s.SlotDuration
            })
            .ToList();

        return Result<List<ScheduleDto>>.Success(schedules);
    }
}