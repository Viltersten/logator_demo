using Model;

namespace Api.Services
{
  public interface IEmailService
  {
    void EmailNetworkClaim(Network network, string email);
    void EmailPasswordReset(string email);
  }
}