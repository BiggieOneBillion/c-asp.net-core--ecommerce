using System;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity;

public class Users
{
  public required string Name { get; set; } 

  public string Email { get; set; } = string.Empty;

  public string Password { get; set; } = string.Empty;

   public UserId UserId { get; set; }

   public Users(string name, string email, string password, Guid userId)
    {
        Name = name;
        Email = email;
        Password = password; //! remember to harsh password before storing in real application
        UserId = UserId.Create(userId);
    }


}
