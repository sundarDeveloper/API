using System.Diagnostics;

namespace CityInfo.API.Services
{
  public class LocalMailService
  {
    private string _mailTo = "mailto@nomail.com";
    private string _mailFrom = "mailfrom@nomail.com";
    public void Send(string subject, string message)
    {
      Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with LocalMailService.");
      Debug.WriteLine($"Subject : {subject}");
      Debug.WriteLine($"Message: {message}");
    }
  }
}
