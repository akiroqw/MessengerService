﻿using Application.Common.Abstractions.Messaging;
using Domain.Primitives.Result;

namespace Application.Users.Commands.AuthenticateUser;

public record AuthenticateUserCommand(string Email, 
    string Password) : ICommand<Result>;