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
		public DbSet<Achievement> Achievements;
		public DbSet<Activity> Activities;
		public DbSet<Gamification> Gamifications;
		public DbSet<Group> Groups;
		public DbSet<Item> Items;
		public DbSet<Pet> Pets;
		public DbSet<PetActivity> PetActivities;
		public DbSet<Tip> Tips;
		public DbSet<User> Users;
		public DbSet<UserAchievement> UserAchievements;
		public DbSet<Vip> Vips;

		public PetOwnerContext(DbContextOptions<PetOwnerContext> options) : base(options)
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

			});

			base.OnModelCreating(builder);
		}

	}
}
