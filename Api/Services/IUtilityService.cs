using System;
using System.Net.Mail;

namespace Api.Services
{
  public interface IUtilityService
  {
    string UrlApi { get; }
    string UrlWeb { get; }
    SmtpClient Server { get; }
    string Salt { get; }
    byte[] SecurityKey { get; }
    DateTime TokenExpiration { get; }

    MailMessage Message(string email, string subject, string body);
    string Hash(string text);
  }
}