using Microsoft.EntityFrameworkCore;

namespace ROS.Data.Contexts.Application
{
	public class ReadDbContext : ApplicationContext
	{
		public ReadDbContext(DbContextOptions<ApplicationContext> options) : base(options) { }
	}
}
