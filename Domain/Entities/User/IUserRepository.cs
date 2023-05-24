﻿using Domain.Primitives.Maybe;
using Domain.Primitives.Result;

namespace Domain.Entities.User;

public interface IUserRepository
{
    Task<Result<User?>> CreateUserAsync(User user, CancellationToken cancellationToken);
    Task<Maybe<User?>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Maybe<User?>> GetUserByEmail(string email, CancellationToken cancellationToken);
    Task<IEnumerable<User>> GetAllUsersAsync();
}
