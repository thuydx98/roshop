using ROS.Common.Constants;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ROS.Common.Loggers
{
	public static class FileLogger<T> where T : class
	{
		private const string ERROR = "ERROR";
		private const string path = "./Logs/";
		private const string fileName = "Log-{0}.txt";

		public static async Task ErrorAsync(Exception ex, params string[] message)
		{
			await WriteLogAsync(ERROR, string.Join("||", message),
				"Message: " + ex.Message,
				"Inner: " + (ex.InnerException != null ? ex.InnerException.Message : "No inner"),
				"StackTrace: " + (ex.StackTrace ?? "No StackTrace"),
				"Source: " + (ex.Source ?? "No Source"));
		}

		private static async Task WriteLogAsync(string logType, params string[] message)
		{
			try
			{
				var datetime = DateTime.Now;
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}

				using (var fs = File.AppendText(string.Format(path + fileName, datetime.ToString(DateFormat.DateFormatStandart))))
				{
					var logContent = logType + "[" + datetime.ToString(DateFormat.DateTimeFormat) + "]:: " + typeof(T).Name + ":: " + string.Join("||", message);
					await fs.WriteLineAsync(logContent);
				}
			}
			catch (Exception ex)
			{
				await ErrorAsync(ex, message);
			}
		}

		public static void Error(params string[] message)
		{
			WriteLog(ERROR, message);
		}

		public static void Error(Exception ex, params string[] message)
		{
			WriteLog(ERROR, string.Join("||", message),
				"Message: " + ex.Message,
				"Inner: " + (ex.InnerException != null ? ex.InnerException.Message : "No inner"),
				"StackTrace: " + (ex.StackTrace ?? "No StackTrace"),
				"Source: " + (ex.Source ?? "No Source"));
		}

		private static void WriteLog(string logType, params string[] message)
		{
			try
			{
				var datetime = DateTime.Now;
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}

				using (var fs = File.AppendText(string.Format(path + fileName, datetime.ToString(DateFormat.DateFormatStandart))))
				{
					var logContent = logType + "[" + datetime.ToString(DateFormat.DateTimeFormat) + "]:: " + typeof(T).Name + ":: " + string.Join("||", message) + "[ENDLOG]";
					fs.WriteLine(logContent);
				}
			}
			catch (Exception ex)
			{
				try
				{
					Thread.Sleep(5000);
					WriteLog(logType, message);
				}
				catch
				{
					Error(ex, message);
				}
			}
		}
	}
}
