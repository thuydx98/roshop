using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ROS.Common.Enums;
using ROS.Data.Entities;
using ROS.EntityFramework.Contracts;

namespace ROS.Data.Contexts.Application
{
	public abstract class ApplicationContext : IdentityDbContext<UserEntity, RoleEntity, int>, IApplicationDbContext
	{
		public ApplicationContext(DbContextOptions options) : base(options) { }

		#region Tables
		public virtual DbSet<BrandEntity> Brands { get; set; }
		public virtual DbSet<CategoryEntity> Categories { get; set; }
		public virtual DbSet<CartItemEntity> CartItems { get; set; }
		public virtual DbSet<OrderEntity> Orders { get; set; }
		public virtual DbSet<OrderDetailEntity> OrderDetails { get; set; }
		public virtual DbSet<ProductCategoryEntity> ProductCategories { get; set; }
		public virtual DbSet<ProductEntity> Products { get; set; }
		public virtual DbSet<ProductPhotoEntity> ProductPhotos { get; set; }
		public virtual DbSet<ProductVoteEntity> ProductVotes { get; set; }
		public virtual DbSet<ProductVotePhotoEntity> ProductVotePhotos { get; set; }
		#endregion

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			foreach (var entity in builder.Model.GetEntityTypes())
			{
				entity.SetTableName(entity.GetTableName().Replace("AspNet", string.Empty));
			}

			builder.HasPostgresExtension("unaccent");

			builder.Entity<BrandEntity>(entity =>
			{
			});

			builder.Entity<CategoryEntity>(entity =>
			{
			});

			builder.Entity<CartItemEntity>(entity =>
			{
				entity.HasKey(e => new { e.UserId, e.ProductId });

				entity.HasOne(n => n.Product)
					.WithMany(n => n.CartItems)
					.HasForeignKey(n => n.ProductId)
					.OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(n => n.User)
					.WithMany(n => n.CartItems)
					.HasForeignKey(n => n.UserId)
					.OnDelete(DeleteBehavior.Cascade);
			});

			builder.Entity<OrderEntity>(entity =>
			{
				entity.Property(p => p.Status).HasConversion(new EnumToStringConverter<OrderStatus>());

				entity.HasOne(n => n.User)
					.WithMany(n => n.Orders)
					.HasForeignKey(n => n.UserId)
					.OnDelete(DeleteBehavior.Cascade);
			});

			builder.Entity<OrderDetailEntity>(entity =>
			{
				entity.HasKey(e => new { e.OrderId, e.ProductId });

				entity.HasOne(n => n.Product)
					   .WithMany(n => n.OrderDetails)
					   .HasForeignKey(n => n.ProductId)
					   .OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(n => n.Order)
					   .WithMany(n => n.Details)
					   .HasForeignKey(n => n.OrderId)
					   .OnDelete(DeleteBehavior.Cascade);
			});

			builder.Entity<ProductCategoryEntity>(entity =>
			{
				entity.HasKey(e => new { e.CategoryId, e.ProductId });

				entity.HasOne(n => n.Product)
					   .WithMany(n => n.ProductCategories)
					   .HasForeignKey(n => n.ProductId)
					   .OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(n => n.Category)
					   .WithMany(n => n.ProductCategories)
					   .HasForeignKey(n => n.CategoryId)
					   .OnDelete(DeleteBehavior.Cascade);
			});

			builder.Entity<ProductEntity>(entity =>
			{
				entity.HasKey(entity => entity.Id);
				entity.Property(p => p.Type).HasConversion(new EnumToStringConverter<ProductTypes>());

				entity.HasOne(n => n.Brand)
					.WithMany(n => n.Products)
					.HasForeignKey(n => n.BrandId)
					.OnDelete(DeleteBehavior.Cascade);
			});

			builder.Entity<ProductPhotoEntity>(entity =>
			{
				entity.HasOne(n => n.Product)
					.WithMany(n => n.Photos)
					.HasForeignKey(n => n.ProductId)
					.OnDelete(DeleteBehavior.Cascade);
			});

			builder.Entity<ProductVoteEntity>(entity =>
			{
				entity.HasOne(n => n.Product)
					.WithMany(n => n.Votes)
					.HasForeignKey(n => n.ProductId)
					.OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(n => n.Author)
					.WithMany(n => n.Votes)
					.HasForeignKey(n => n.AuthorId)
					.OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(n => n.Order)
					.WithMany(n => n.Votes)
					.HasForeignKey(n => n.OrderId)
					.OnDelete(DeleteBehavior.Cascade);
			});

			builder.Entity<ProductVotePhotoEntity>(entity =>
			{
				entity.HasOne(n => n.Vote)
					.WithMany(n => n.Photos)
					.HasForeignKey(n => n.VoteId)
					.OnDelete(DeleteBehavior.Cascade);
			});

			builder.Entity<RoleEntity>(users =>
			{
			});

			builder.Entity<UserEntity>(users =>
			{
			});
		}
	}
}
