using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ROS.Contracts.User
{
	public class Permisison
	{

		public static List<string> ListPermission => typeof(Permisison)
			.GetFields(BindingFlags.Public | BindingFlags.Static)
			.Where(f => f.FieldType == typeof(string))
			.Select(f => (string)f.GetValue(null))
			.ToList();
	}
}
