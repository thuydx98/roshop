namespace ROS.Common.Mail
{
	public class MailMessage
	{
		public string DisplayName { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public string Cc { get; set; }
		public string Bcc { get; set; }
		public string Subject { get; set; }
		public string PlainTextMessage { get; set; }
		public string HtmlMessage { get; set; }
		public string ExtensionString { get; set; }
	}
}
