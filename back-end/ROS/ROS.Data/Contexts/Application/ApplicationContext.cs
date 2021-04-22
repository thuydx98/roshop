using Microsoft.EntityFrameworkCore;
using ROS.EntityFramework.Contracts;

namespace ROS.Data.Contexts.Application
{
	public class ApplicationContext : DbContext, IApplicationDbContext
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

		#region Tables
		//public virtual DbSet<ProgramEntity> Programs { get; set; }
		#endregion

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasPostgresExtension("unaccent");

			//modelBuilder.Entity<ProgramEntity>(entity =>
			//{
			//	entity.ToTable("Programs");
			//	entity.HasKey(entity => entity.Id);
			//	entity.HasIndex(p => p.Title).IsUnique();
			//});

			//modelBuilder.Entity<FormEntity>(entity =>
			//{
			//	entity.ToTable("Forms");
			//	entity.HasKey(entity => entity.Id);
			//	entity.HasIndex(e => new { e.FormTitle, e.StartTime });
			//	entity.HasIndex(e => e.FormTitle).IsUnique();

			//	entity.HasOne(n => n.Program)
			//	.WithMany(n => n.Forms)
			//	.HasForeignKey(n => n.ProgramId)
			//	.OnDelete(DeleteBehavior.Cascade);
			//});

			//modelBuilder.Entity<QuestionEntity>(entity =>
			//{
			//	entity.ToTable("Questions");
			//	entity.HasKey(entity => entity.Id);
			//	entity.HasIndex(e => new { e.FormId, e.Type });
			//});


			//modelBuilder.Entity<EmailTemplateEntity>(entity =>
			//{
			//	entity.ToTable("EmailTemplates");
			//	entity.HasKey(entity => entity.Id);
			//	entity.HasIndex(e => new { e.ProgramId, e.IsActive });

			//	entity.HasOne(n => n.Program)
			//		.WithMany(n => n.EmailTemplates)
			//		.HasForeignKey(n => n.ProgramId)
			//		.OnDelete(DeleteBehavior.Cascade);
			//});
		}
	}
}
