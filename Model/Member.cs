using System;
using System.Collections.Generic;

namespace Model
{
  public class Member
  {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    //public string Token { get; set; }
    public double Lat { get; set; }
    public double Lng { get; set; }
    public string Address { get; set; }
    public List<Tag> Tags { get; set; }
    public Network Network { get; set; }
    public Privileges Privilege { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? AccessedOn { get; set; }
    public DateTime? DeletedOn { get; set; }

    public enum Privileges
    {
      Creator,
      Admin,
      Super,
      Common,
      Suspended,
      Other
    }
  }
}