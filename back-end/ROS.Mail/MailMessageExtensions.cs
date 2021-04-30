using MimeKit;
using System;

namespace ROS.Common.Mail
{
	public static class MailMessageExtensions
	{
		public static MimeMessage ToMimeMessage(this MailMessage mailMessage)
		{
			if (mailMessage == null)
			{
				throw new ArgumentNullException(nameof(mailMessage));
			}

			if (string.IsNullOrWhiteSpace(mailMessage.From))
			{
				throw new ArgumentException(nameof(mailMessage.From));
			}

			if (string.IsNullOrWhiteSpace(mailMessage.To))
			{
				throw new ArgumentException(nameof(mailMessage.To));
			}

			if (string.IsNullOrWhiteSpace(mailMessage.Subject))
			{
				throw new ArgumentException(nameof(mailMessage.Subject));
			}

			if (string.IsNullOrWhiteSpace(mailMessage.PlainTextMessage) &&
				string.IsNullOrWhiteSpace(mailMessage.HtmlMessage))
			{
				throw new ArgumentException("No message provided");
			}

			var mimeMessage = new MimeMessage();
			mimeMessage.From.Add(new MailboxAddress(mailMessage.DisplayName, mailMessage.From));

			if (!string.IsNullOrWhiteSpace(mailMessage.To))
			{
				var mailAddresses = mailMessage.To.Split(';');
				foreach (var mailAddress in mailAddresses)
				{
					if (mailAddress.Trim().Length > 0)
					{
						mimeMessage.To.Add(new MailboxAddress("", mailAddress));
					}
				}
			}

			if (!string.IsNullOrWhiteSpace(mailMessage.Cc))
			{
				var mailCcAddresses = mailMessage.Cc.Split(';');
				foreach (var mailAddress in mailCcAddresses)
				{
					if (mailAddress.Trim().Length > 0)
					{
						mimeMessage.Cc.Add(new MailboxAddress("", mailAddress));
					}
				}
			}

			if (!string.IsNullOrWhiteSpace(mailMessage.Bcc))
			{
				var mailBccAddresses = mailMessage.Bcc.Split(';');
				foreach (var mailAddress in mailBccAddresses)
				{
					if (mailAddress.Trim().Length > 0)
					{
						mimeMessage.Bcc.Add(new MailboxAddress("", mailAddress));
					}
				}
			}

			mimeMessage.Subject = mailMessage.Subject;

			var bodyBuilder = new BodyBuilder();
			if (!string.IsNullOrWhiteSpace(mailMessage.PlainTextMessage))
			{
				bodyBuilder.TextBody = mailMessage.PlainTextMessage;
			}

			if (!string.IsNullOrWhiteSpace(mailMessage.HtmlMessage))
			{
				bodyBuilder.HtmlBody = mailMessage.HtmlMessage;
			}

			mimeMessage.Body = bodyBuilder.ToMessageBody();

			return mimeMessage;
		}
	}
}
