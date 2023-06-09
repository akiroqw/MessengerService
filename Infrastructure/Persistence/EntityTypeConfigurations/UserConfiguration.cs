﻿using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        builder.HasIndex(user => user.Id)
            .IsUnique();

        builder.HasIndex(user => user.Username).IsUnique();
        builder.Property(user => user.Username)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(user => user.Password)
            .IsRequired()
            .HasMaxLength(128);

        builder.HasMany(u => u.SentChats)
            .WithOne(c => c.Sender)
            .HasForeignKey(c => c.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.ReceivedChats)
            .WithOne(c => c.Receiver)
            .HasForeignKey(c => c.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.UserGroups)
            .WithOne(ug => ug.User)
            .HasForeignKey(ug => ug.UserId);

        builder.Property(user => user.CreationTime).IsRequired();
    }
}