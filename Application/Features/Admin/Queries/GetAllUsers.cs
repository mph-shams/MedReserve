using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MedReserve.Application.DTOs.Admin;
using MediatR;

namespace MedReserve.Application.Features.Admin.Queries
{

    //u.Email for DTO!
    public record GetAllUsersQuery() : IRequest<Result<List<UserDto>>>;

    public class GetAllUsersHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetAllUsersQuery, Result<List<UserDto>>>
    {
        public async Task<Result<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken ct)
        {
            var users = (await _unitOfWork.Repository<User>().GetAllAsync())
                .Select(u => new UserDto {Id = u.Id, Username = u.Username, Role = u.Role.ToString()})
                .ToList();
            return Result<List<UserDto>>.Success(users);
        }
    }
}
