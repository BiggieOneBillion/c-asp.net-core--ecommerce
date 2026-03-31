using System;
using Ecommerce.CORE.ValueObjects;
using Ecommerce.CORE.Enums;
using System.Collections.Generic;

using Ecommerce.CORE.Common;

namespace Ecommerce.CORE.Entity;

public class Users : ISoftDelete, IAuditable
{
    public UserId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Customer;

    // Email Verification
    public bool IsEmailVerified { get; set; }
    public string? EmailVerificationToken { get; set; }

    // Password Reset
    public string? PasswordResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }

    // Account Lockout
    public DateTime? LockoutEnd { get; set; }
    public int AccessFailedCount { get; set; }

    // Navigation properties
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }

    // Audit
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public string? LastModifiedBy { get; set; }

    private Users() { }

    public Users(string name, string email, string passwordHash, Guid userId, UserRole role = UserRole.Customer)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Id = UserId.Create(userId);
        Role = role;
        IsEmailVerified = false;
        AccessFailedCount = 0;
        IsDeleted = false;
    }

    public void SoftDelete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }

    public void VerifyEmail()
    {
        IsEmailVerified = true;
        EmailVerificationToken = null;
    }

    public void UpdatePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
        PasswordResetToken = null;
        ResetTokenExpires = null;
    }

    public bool IsLockedOut => LockoutEnd.HasValue && LockoutEnd.Value > DateTime.UtcNow;
}
