using System;

namespace ROS.Common.Extensions
{
	public static class DateTime
	{
		public static System.DateTime ToStartOfDay(this System.DateTime date)
		{
			return date.Date;
		}

		public static System.DateTime ToEndOfDay(this System.DateTime date)
		{
			return date.Date.AddDays(1).AddTicks(-1);
		}

		public static System.DateTime ToLastDayOfMonth(this System.DateTime date)
		{
			return new System.DateTime(date.Year, date.Month, System.DateTime.DaysInMonth(date.Year, date.Month));
		}
	}
}
