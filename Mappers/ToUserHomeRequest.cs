using PetOwner.DTOs;
using PetOwner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Mappers
{
	public static class ToUserHomeRequest
	{
		public static UserHomeResponse ToUserHome(this User user)
		{
			return new UserHomeResponse
			{
				Name = user.Name,
				Photo = user.Photo,
				Level = user.Level.Experience,
				Tokens = user.Level.Tokens,
				VipEndDate = user.Vip.EndDate,
			};
		}
	}
}
