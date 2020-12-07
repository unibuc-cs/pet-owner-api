using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.DTOs
{
	public class UserLeaderboardsDTO
	{
		public int UserId { get; set; }
		public string Name { get; set; }
		public string Photo { get; set; }
		public int Level { get; set; }

	}
}
