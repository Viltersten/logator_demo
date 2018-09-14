using System;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Api.Services
{
  public class UtilityService : IUtilityService
  {
    public UtilityService(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    private readonly IConfiguration Configuration;

    public string UrlApi => Configuration["Url:api"];
    public string UrlWeb => Configuration["Url:web"];
    public SmtpClient Server => new SmtpClient(
      Configuration["Smtp:server"],
      Convert.ToInt32(Configuration["Smtp:port"]))
    {
      EnableSsl = true,
      UseDefaultCredentials = false,
      Credentials = new NetworkCredential(
        Configuration["Smtp:user"],
        Configuration["Smtp:password"])
    };
    public string Salt => Configuration["Security:salt"];
    public byte[] SecurityKey => Encoding.UTF8.GetBytes(Configuration["Security:key"]);
    public DateTime TokenExpiration => DateTime.UtcNow.AddSeconds(Convert.ToDouble(Configuration["Security:tokenSeconds"]));

    public MailMessage Message(string email, string subject, string body)
    {
      return new MailMessage
      {
        IsBodyHtml = true,
        From = new MailAddress(Configuration["Smtp:sender"], Configuration["Smtp:name"]),
        To = { new MailAddress(email) },
        Subject = subject,
        Body = body
      };
    }

    public string Hash(string text)
    {
      HashAlgorithm algorithm = new HMACSHA256(SecurityKey);

      byte[] plain = Encoding.UTF8.GetBytes(text + Salt);
      byte[] hashed = algorithm.ComputeHash(plain);

      return Convert.ToBase64String(hashed);
    }
  }
}