﻿using PetOwner.DTOs;
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
			var userHome = new UserHomeResponse
			{
				Name = user.Name,
				Photo = user.Photo,
				Level = user.Level.Experience,
				Tokens = user.Level.Tokens,

			};

			if(user.Vip != null)
			{
				userHome.VipEndDate = user.Vip.EndDate;
			}

			return userHome;
		}
	}
}
