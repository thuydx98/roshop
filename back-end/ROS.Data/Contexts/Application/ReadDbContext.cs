using Microsoft.EntityFrameworkCore;

namespace ROS.Data.Contexts.Application
{
	public class ReadDbContext : ApplicationContext
	{
		public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options) { }
	}
}
