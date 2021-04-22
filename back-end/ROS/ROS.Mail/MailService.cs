using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using ROS.Common.Extensions;
using System.Text.Json;

namespace ROS.Common.Mail
{
	public class MailService : IMailService
	{
		private readonly MailServerSetting _mailServerSetting;
		private readonly ILogger _logger;

		public MailService(MailServerSetting mailServerSetting, ILogger<MailService> logger)
		{
			_mailServerSetting = mailServerSetting;
			_logger = logger;
		}

		public async Task SendAsync(MailMessage mailMessage, bool keepReceiver = false)
		{
			try
			{
				string signature = "<p><em>This email was sent automatically by ROS. Please don't reply this email.</em></p>";

				mailMessage.From = _mailServerSetting.FromAddress;
				mailMessage.DisplayName = _mailServerSetting.DisplayName;
				mailMessage.HtmlMessage = ReplaceContentTest(mailMessage) + signature;
				mailMessage.To = ReplaceEmailTest(mailMessage.To);
				mailMessage.Cc = ReplaceEmailTest(mailMessage.Cc);
				mailMessage.Bcc = ReplaceEmailTest(mailMessage.Bcc);
				mailMessage.Subject = ReplaceSubjectTest(mailMessage.Subject);
				mailMessage.HtmlMessage = mailMessage.HtmlMessage.ReplaceDomain(_mailServerSetting.SiteUrl);

				if (mailMessage.To.IsEmpty() && !keepReceiver)
				{
					if (!mailMessage.Cc.IsEmpty())
					{
						mailMessage.To = mailMessage.Cc;
						mailMessage.Cc = string.Empty;
					}
					else if (!mailMessage.Bcc.IsEmpty())
					{
						mailMessage.To = mailMessage.Bcc;
						mailMessage.Bcc = string.Empty;
					}
					else
					{
						_logger.LogInformation("No receiver", "From: " + _mailServerSetting.FromAddress, "To: " + mailMessage.To, "CC: " + mailMessage.Cc, "Bcc: " + mailMessage.Bcc, "Message:" + mailMessage.HtmlMessage);
						return;
					}
				}

				// Send mail
				using (var client = new SmtpClient())
				{
					client.ServerCertificateValidationCallback = (s, c, h, e) => true;
					await client.ConnectAsync(_mailServerSetting.Host, _mailServerSetting.Port, SecureSocketOptions.Auto);

					client.AuthenticationMechanisms.Remove("XOAUTH2");
					await client.AuthenticateAsync(_mailServerSetting.UserName, _mailServerSetting.Password);
					await client.SendAsync(mailMessage.ToMimeMessage());

					client.Disconnect(true);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "MailContent:" + JsonSerializer.Serialize(mailMessage));
			}
		}

		private string ReplaceEmailTest(string emailAdress)
		{
			if (string.IsNullOrWhiteSpace(emailAdress)) return string.Empty;

			return string.IsNullOrWhiteSpace(_mailServerSetting.EmailTest) ?
				emailAdress : _mailServerSetting.EmailTest;
		}

		private string ReplaceSubjectTest(string subject)
		{
			if (!_mailServerSetting.EmailTest.IsEmpty())
			{
				return "[StandardApi Email Test] - " + subject;
			}

			return subject;
		}

		private string ReplaceContentTest(MailMessage mailMessage)
		{
			return string.IsNullOrWhiteSpace(_mailServerSetting.EmailTest) ?
				mailMessage.HtmlMessage :
				string.Concat(mailMessage.HtmlMessage,
				"<br/><br/>--"
				+ "<br/><br/> PLEASE NOTE: THIS EMAIL WAS SENT FOR TESTING PURPOSE. DO NOT DO ANYTHING."
				+ "<br/><br/>Testing purpose: Email addresses will be sent to: <br/><br/> "
				+ "To: " + mailMessage.To + "<br/><br/>"
				+ "Bcc: " + mailMessage.Bcc + "<br/><br/>"
				+ "Cc: " + mailMessage.Cc
				+ (mailMessage.ExtensionString.IsEmpty() ? "" : "<br/><br/>--<br/><br/>" + mailMessage.ExtensionString));
		}
	}
}
