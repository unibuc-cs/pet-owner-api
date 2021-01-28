using PetOwner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.DTOs
{
	public class UserProfileDTO
	{
		public string Name;
		public string Photo;
		public Vip Vip;
		public int Level;
		public int Tokens;
	}
}
