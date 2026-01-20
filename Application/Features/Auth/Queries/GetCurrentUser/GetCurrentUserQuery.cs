using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using MedReserve.Application.DTOs.Auth;

namespace MedReserve.Application.Features.Auth.Queries.GetCurrentUser
{
    public record GetCurrentUserQuery(int UserId) : IRequest<Result<UserProfileDto>>;

    public class GetCurrentUserHandler(IUnitOfWork _uow) : IRequestHandler<GetCurrentUserQuery, Result<UserProfileDto>>
    {
        public async Task<Result<UserProfileDto>> Handle(GetCurrentUserQuery request, CancellationToken ct)
        {
            var user = await _uow.Repository<Domain.Entities.User>().GetByIdAsync(request.UserId);
            if (user == null) return Result<UserProfileDto>.Failure("User not found.");

            return Result<UserProfileDto>.Success(new UserProfileDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role.ToString(),
                IsVerified = user.IsVerified,
                CreatedAt = user.CreatedAt
            });
        }

    }

}