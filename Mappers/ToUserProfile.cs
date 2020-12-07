using PetOwner.DTOs;
using PetOwner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Mappers
{
	public static class ToUserProfile
	{
		public static UserProfileDTO ToProfile(this User user)
		{
			return new UserProfileDTO
			{
				Name = user.Name,
				Photo = user.Photo,
				Tokens = user.Level.Tokens,
				Level = user.Level.Experience,
				Vip = user.Vip,
			};
		}

	}
}
