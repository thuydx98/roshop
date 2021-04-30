using System;

namespace ROS.Common.Extensions
{
	public static class DateTimeExtensions
	{
		public static DateTime ToStartOfDay(this DateTime date)
		{
			return date.Date;
		}

		public static DateTime ToEndOfDay(this DateTime date)
		{
			return date.Date.AddDays(1).AddTicks(-1);
		}

		public static DateTime ToLastDayOfMonth(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
		}
	}
}
