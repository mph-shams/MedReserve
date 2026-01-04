using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace MedReserve.Application.Features.Admin.Queries
{

    public record UserDto(int Id, string Username, string Role); //u.Email
    public record GetAllUsersQuery() : IRequest<Result<List<UserDto>>>;

    public class GetAllUsersHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetAllUsersQuery, Result<List<UserDto>>>
    {
        public async Task<Result<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken ct)
        {
            var users = (await _unitOfWork.Repository<User>().GetAllAsync()) 
                .Select(u => new UserDto(u.Id, u.Username, u.Role.ToString())) //u.Email
                .ToList();
            return Result<List<UserDto>>.Success(users);
        }
    }
}
