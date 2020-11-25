using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Models
{
	public class Group
	{
		[Key]
		public int GroupId { get; set; }
		public string InviteCode { get; set; }
		public string GroupName { get; set; }
		public virtual Cost Cost { get; set; }
		public ICollection<User> Users { get; set; }
		public ICollection<Pet> Pets { get; set; }
	}
}
