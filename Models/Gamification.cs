using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Models
{
	public class Gamification
	{
		[Key]
		public int LevelId { get; set; }
		public int Experience { get; set; }
		public int WeeklyExp { get; set; }
		public string LevelName { get; set; }
		public int Tokens { get; set; }
		//public int UserId { get; set; }
		public virtual User User { get; set; }
	}
}
