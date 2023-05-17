﻿using Domain.Entities;
using System.Data;
using Domain.Shared;
using Application.Common.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result>? CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public bool AuthenticateUserAsync(string requestEmail, string requestPassword)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == requestEmail && u.Password == requestPassword);

        return user is not null;
    }
}