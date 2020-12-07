using PetOwner.DTOs;
using PetOwner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Mappers
{
	public static class ToUserLeaderBoards
	{
		public static UserLeaderboardsDTO ToLeaderboards(this User user)
		{
			return new UserLeaderboardsDTO
			{
				Name = user.Name,
				UserId = user.UserId,
				Photo = user.Photo,
				Level = user.Level.Experience,
			};
		}
	}
}
