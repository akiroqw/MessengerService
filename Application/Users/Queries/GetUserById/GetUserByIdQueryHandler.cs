﻿using Application.Common.Abstractions.Messaging;
using Application.Common.Interfaces;
using Domain.Primitives.Result;

namespace Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);

        return new UserResponse(result.Value!);
    }
}