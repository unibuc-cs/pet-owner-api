using Microsoft.EntityFrameworkCore;
using PetOwner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Data
{
	public class PetOwnerContext : DbContext
	{
		public DbSet<Achievement> Achievements { get; set; }
		public DbSet<Activity> Activities { get; set; }
		public DbSet<Gamification> Gamifications { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<Item> Items { get; set; }
		public DbSet<Pet> Pet { get; set; }
		public DbSet<PetActivity> PetActivities { get; set; }
		public DbSet<Tip> Tips { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserAchievement> UserAchievements { get; set; }
		public DbSet<Vip> Vip { get; set; }

		public PetOwnerContext(DbContextOptions<PetOwnerContext> options) : base(options)
		{

		}
		public PetOwnerContext()
		{
			
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			
			builder.Entity<UserAchievement>(entity =>
			{
				entity.HasKey(x => new { x.UserId, x.AchievementId });

				entity.HasOne(x => x.User)
				.WithMany(y => y.UserAchievements)
				.HasForeignKey(x => x.UserId);

				entity.HasOne(x => x.Achievement)
				.WithMany(y => y.UserAchievements)
				.HasForeignKey(x => x.AchievementId);

			});

			builder.Entity<PetActivity>(entity =>
			{
				entity.HasKey(x => new { x.PetId, x.ActivityId });

				entity.HasOne(x => x.Pet)
				.WithMany(y => y.PetActivities)
				.HasForeignKey(x => x.PetId);

				entity.HasOne(x => x.Activity)
				.WithMany(y => y.PetActivities)
				.HasForeignKey(x => x.ActivityId);

			});

			builder.Entity<Pet>()
				.HasOne(x => x.Group)
				.WithMany(y => y.Pets)
				.HasForeignKey(x => x.GroupId);

			builder.Entity<Group>()
				.HasMany(x => x.Items)
				.WithOne(y => y.Group)
				.HasForeignKey(y => y.GroupId);

			builder.Entity<User>(entity =>
			{
				entity.HasOne(x => x.Group)
				.WithMany(y => y.Users)
				.HasForeignKey(x => x.GroupId);

				entity.HasOne(x => x.Vip)
				.WithOne(y => y.User)
				.HasForeignKey<User>(x => x.VipId);

				entity.HasOne(x => x.Level)
				.WithOne(y => y.User)
				.HasForeignKey<User>(x => x.LevelId);

				entity.HasKey(x => x.UserId);
				

				//entity.Property(x => x.UserId).HasAnnotation("SqlServer:Identity", "1, 1");

			});

			base.OnModelCreating(builder);
		}


	}
}
