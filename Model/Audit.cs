using System;

namespace Model
{
  public class Audit
  {
    public int Id { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Target { get; set; }
    public string Summary { get; set; }
    public string Details { get; set; }
  }
}