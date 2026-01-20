using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using MedReserve.Application.DTOs.Auth;

namespace MedReserve.Application.Features.Auth.Commands.ChangePassword
{

    public record ChangePasswordCommand(int UserId, ChangePasswordRequest Data) : IRequest<Result<bool>>;

    public class ChangePasswordHandler(IUnitOfWork _uow) : IRequestHandler<ChangePasswordCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(ChangePasswordCommand request, CancellationToken ct)
        {
            var user = await _uow.Repository<Domain.Entities.User>().GetByIdAsync(request.UserId);
            if (user == null) return Result<bool>.Failure("User not found.");

            if (!BCrypt.Net.BCrypt.Verify(request.Data.CurrentPassword, user.PasswordHash))
                return Result<bool>.Failure("Current password is incorrect.");

            if (request.Data.NewPassword != request.Data.ConfirmNewPassword)
                return Result<bool>.Failure("New password and confirmation do not match.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Data.NewPassword);
            _uow.Repository<Domain.Entities.User>().Update(user);

            return await _uow.SaveChangesAsync(ct) > 0
                ? Result<bool>.Success(true)
                : Result<bool>.Failure("An error occurred while updating the password.");
        }

    }
}
