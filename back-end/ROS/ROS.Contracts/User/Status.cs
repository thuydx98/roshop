using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ROS.Contracts.User
{
	public class Status
	{
		public const string ACTIVE = "ACTIVE";
		public const string INACTIVE = "INACTIVE";
		public const string LOCKED = "LOCKED";
		public const string SUSPENDED = "SUSPENDED";

		public static List<string> ListStatus => typeof(Status)
			.GetFields(BindingFlags.Public | BindingFlags.Static)
			.Where(f => f.FieldType == typeof(string))
			.Select(f => (string)f.GetValue(null))
			.ToList();
	}
}
