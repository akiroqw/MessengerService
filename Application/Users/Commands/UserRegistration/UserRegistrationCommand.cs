﻿using Application.Common.Abstractions.Messaging;
using Domain.Primitives.Result;

namespace Application.Users.Commands.UserRegistration;

public sealed record UserRegistrationCommand(
    string Username,
    string Password) : ICommand<Result<RegistrationResponse>>;