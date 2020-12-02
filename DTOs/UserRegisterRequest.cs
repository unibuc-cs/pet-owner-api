using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.DTOs
{
	public class UserRegisterRequest
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string FCMToken { get; set; }
		public string InviteCode { get; set; }

	}
}
