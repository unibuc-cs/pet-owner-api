
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Models
{
	public class Achievement
	{
		[Key]
		public int AchievementId { get; set; }
		public string Description { get; set; }
		public int ExpPoints { get; set; }
		public ICollection<UserAchievement> UserAchievements { get; set; }

	}
}
