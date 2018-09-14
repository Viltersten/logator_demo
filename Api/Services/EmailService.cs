using System.Net.Mail;
using Model;

namespace Api.Services
{
  public class EmailService : IEmailService
  {
    public EmailService(IUtilityService utility) { Utility = utility; }

    private readonly IUtilityService Utility;

    public void EmailNetworkClaim(Network network, string email)
    {
      MailMessage message = Utility.Message(
        email,
        "Network " + network.Name + " requested",
        "The new network " + network.Name
                           + " awaits you to <a href='" + Utility.UrlWeb + "claim"
                           + "?networkId=" + network.Id
                           + "&name=" + network.Name
                           + "&email=" + email
                           + "'>claim it</a>.");

      Utility.Server.Send(message);
    }

    public void EmailPasswordReset(string email)
    {
      MailMessage message = Utility.Message(
        email,
        "Password reset request",
        "You may reset the password of your account by clicking "
        + "<a href='" + Utility.UrlWeb + "reset"
        + "?email=" + email
        + "'>here</a>.");

      Utility.Server.Send(message);
    }
  }
}