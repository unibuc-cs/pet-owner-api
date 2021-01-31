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
		public static int ExpForLevel = 200;
		public static UserProfileDTO ToProfile(this User user)
		{
			 var profile = new UserProfileDTO
			{
				Name = user.Name,
				Photo = user.Photo,
				Tokens = user.Level.Tokens,
				Level = user.Level.Experience/ExpForLevel + 1,
				GroupId = user.GroupId,
			};

			if(user.Vip != null)
			{
				profile.Vip = user.Vip;
			}

			return profile;
		}

	}
}
