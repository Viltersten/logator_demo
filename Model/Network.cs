using System;
using System.Collections.Generic;

namespace Model
{
  public class Network
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Member> Members { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? AccessedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
  }
}
