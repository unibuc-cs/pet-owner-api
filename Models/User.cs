using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Models
{
	public class User
	{
		[Key]
		public int UserId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Photo { get; set; }
		public string GoogleId { get; set; }
		public int GroupId { get; set; }
		public int? VipId { get; set; }
		public int LevelId { get; set; }
		public string FCMToken { get; set; }
		public virtual Vip Vip { get; set; }
		public virtual Gamification Level { get; set; }
		public virtual Group Group { get; set; }
		public ICollection<UserAchievement> UserAchievements { get; set; }

	}
}
