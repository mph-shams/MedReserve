using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace MedReserve.Application.Features.Doctors.Commands
{
    // Application/Features/Doctors/Commands/CreateDoctorInfo/CreateDoctorInfoCommand.cs
    public record CreateDoctorInfoCommand(
        int UserId,
        string Specialty,
        string Bio,
        decimal ConsultationFee) : IRequest<Result<int>>;

    public class CreateDoctorInfoHandler : IRequestHandler<CreateDoctorInfoCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateDoctorInfoHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<Result<int>> Handle(CreateDoctorInfoCommand request, CancellationToken ct)
        {
            // بررسی اینکه آیا این کاربر وجود دارد و نقش او پزشک است یا خیر
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.UserId);
            if (user == null || user.Role != UserRole.Doctor)
                return Result<int>.Failure("کاربر مورد نظر یافت نشد یا دسترسی پزشک ندارد.");

            var doctor = new Doctor
            {
                UserId = request.UserId,
                Specialty = request.Specialty,
                Bio = request.Bio,
                ConsultationFee = request.ConsultationFee
            };

            await _unitOfWork.Repository<Doctor>().AddAsync(doctor);
            await _unitOfWork.SaveChangesAsync(ct);

            return Result<int>.Success(doctor.Id); // این همان آیدی است که باید در نوبت‌دهی استفاده شود
        }
    }
}
