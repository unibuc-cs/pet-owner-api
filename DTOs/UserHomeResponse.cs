using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.DTOs
{
	public class UserHomeResponse
	{
		public string Name { get; set; }
		public string Photo { get; set; }
		public int Level { get; set; }
		public int Tokens { get; set; }
		public DateTime VipEndDate { get; set; }
	}
}
