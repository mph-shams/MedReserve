using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Features.MedicalFiles.Queries.GetMedicalFilesByAppointment;

public record MedicalFileDto(int Id, string FileName, string ContentType, ulong Size);
public record GetMedicalFilesByAppointmentQuery(int AppointmentId) : IRequest<Result<List<MedicalFileDto>>>;

public class GetByAppointmentHandler : IRequestHandler<GetMedicalFilesByAppointmentQuery, Result<List<MedicalFileDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetByAppointmentHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<List<MedicalFileDto>>> Handle(GetMedicalFilesByAppointmentQuery request, CancellationToken ct)
    {
        var files = (await _unitOfWork.Repository<MedicalFile>().GetAllAsync())
            .Where(f => f.AppointmentId == request.AppointmentId)
            .Select(f => new MedicalFileDto(f.Id, f.FileName, f.ContentType, f.Size))
            .ToList();
        return Result<List<MedicalFileDto>>.Success(files);
    }
}