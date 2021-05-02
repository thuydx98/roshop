using System.Threading.Tasks;

namespace ROS.Common.Mail
{
	public interface IMailService
	{
		Task<bool> SendAsync(MailMessage mailMessage, bool keepReceiver = false);
	}
}
