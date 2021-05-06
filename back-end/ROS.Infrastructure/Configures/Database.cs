using ROS.Contracts.EntityFramework;
using ROS.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using ROS.Data.Contexts.Application;

namespace ROS.Infrastructure.Configures
{
	public static class Database
	{
		public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
		{
			var readConnectionString = Environment.GetEnvironmentVariable("READ_DATABASE_CONNECTION_STRING");
			var writeConnectionString = Environment.GetEnvironmentVariable("WRITE_DATABASE_CONNECTION_STRING");

			services.AddDbContext<ReadDbContext>(opt => opt.UseNpgsql(readConnectionString));
			services.AddScoped<IUnitOfWork<ReadDbContext>, UnitOfWork<ReadDbContext>>();

			services.AddDbContext<WriteDbContext>(opt => opt.UseNpgsql(writeConnectionString));
			services.AddScoped<IUnitOfWork<WriteDbContext>, UnitOfWork<WriteDbContext>>();

			return services;
		}
	}
}
