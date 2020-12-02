using PetOwner.DTOs;
using PetOwner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Mappers
{
	public static class ToUserMapper
	{
		public static User ToUser(this UserRegisterRequest userRegister)
		{
			return new User
			{
				Email = userRegister.Email,
				Password = userRegister.Password,
				FCMToken = userRegister.FCMToken,
			};
		}
	}
}
