using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Enums;
using MediatR;

namespace MedReserve.Application.Features.Admin.Commands
{
    public record UpdateUserRoleCommand(int UserId, UserRole NewRole) : IRequest<Result<bool>>;

    public class UpdateUserRoleHandler(IUnitOfWork _unitOfWork) : IRequestHandler<UpdateUserRoleCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(UpdateUserRoleCommand request, CancellationToken ct)
        {
            var user = await _unitOfWork.Repository<Domain.Entities.User>().GetByIdAsync(request.UserId); //
            if (user == null) return Result<bool>.Failure("User not found.");

            user.Role = request.NewRole; //
            _unitOfWork.Repository<Domain.Entities.User>().Update(user);

            return await _unitOfWork.SaveChangesAsync(ct) > 0
                ? Result<bool>.Success(true)
                : Result<bool>.Failure("An error occurred while updating the user role.");
        }
    }
}
