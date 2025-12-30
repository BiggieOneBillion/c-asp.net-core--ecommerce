using System;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity;

public class Users
{
  public  string Name { get; set; } = string.Empty;

  public string Email { get; set; } = string.Empty;

  public string Password { get; set; } = string.Empty;

   public UserId Id { get; set; }

   public Users(string name, string email, string password, Guid userId)
    {
        Name = name;
        Email = email;
        Password = password; //! remember to harsh password before storing in real application
        Id = UserId.Create(userId);
    }

}
