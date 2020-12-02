using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Models
{
	public class UserAchievement
	{
		public int UserId { get; set; }
		public int AchievementId { get; set; }
		public DateTime RecordStamp { get; set; }
		public virtual User User { get; set; }
		public virtual Achievement Achievement { get; set; }
	}
}
