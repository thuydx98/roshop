using Microsoft.EntityFrameworkCore;

namespace ROS.Data.Contexts.Application
{
	public class WriteDbContext : ApplicationContext
	{
		public WriteDbContext(DbContextOptions<ApplicationContext> options) : base(options) { }
	}
}
