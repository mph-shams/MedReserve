using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MedReserve.Application.DTOs.MedicalFiles;
using MediatR;

namespace Application.Features.MedicalFiles.Queries.GetMedicalFilesByAppointment;

public record GetMedicalFilesByAppointmentQuery(int AppointmentId) : IRequest<Result<List<MedicalFileDto>>>;

public class GetByAppointmentHandler : IRequestHandler<GetMedicalFilesByAppointmentQuery, Result<List<MedicalFileDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetByAppointmentHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<List<MedicalFileDto>>> Handle(GetMedicalFilesByAppointmentQuery request, CancellationToken ct)
    {
        var files = (await _unitOfWork.Repository<MedicalFile>().GetAllAsync())
            .Where(f => f.AppointmentId == request.AppointmentId)
            .Select(f => new MedicalFileDto{Id = f.Id,FileName = f.FileName,ContentType = f.ContentType,Size = f.Size})
            .ToList();
        return Result<List<MedicalFileDto>>.Success(files);
    }
}