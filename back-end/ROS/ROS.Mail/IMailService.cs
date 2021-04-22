using System.Threading.Tasks;

namespace ROS.Common.Mail
{
	public interface IMailService
	{
		Task SendAsync(MailMessage mailMessage, bool keepReceiver = false);
	}
}
