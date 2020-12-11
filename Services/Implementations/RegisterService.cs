using Microsoft.EntityFrameworkCore;
using PetOwner.Data;
using PetOwner.DTOs;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using PetOwner.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Services.Implementations
{
	public class RegisterService : IRegisterService
	{
		private readonly IUserRepository _userRepository;
		private readonly IGamificationRepository _gamificationRepository;
		private readonly IGroupRepository _groupRepository;
		private readonly PetOwnerContext _context;
		public RegisterService(IUserRepository userRepository, IGamificationRepository gamificationRepository, IGroupRepository groupRepository, PetOwnerContext context)
		{
			_userRepository = userRepository;
			_gamificationRepository = gamificationRepository;
			_groupRepository = groupRepository;
			_context = context;

		}
		public bool Register(UserRegisterRequest user)
		{

			if (_userRepository.GetUserByEmail(user.Email) != null)
				return false;

		


			var gamification = new Gamification
			{
				LevelName = "Nivel 1",
				Experience = 0,
				WeeklyExp = 0,
				Tokens = 0,
			};


			Group defaultGroup = new Group
			{
				GroupName = "MyGroup",
			};


			User userCreate = new User
			{
				Email = user.Email,
				Password = user.Password,
				FCMToken = user.FCMToken,
				Level = gamification,

			};

			if (user.InviteCode != null)
			{
				var groupGet = _groupRepository.GetByInviteCode(user.InviteCode);

				if (groupGet != null)
				{
					userCreate.Group = groupGet;
					userCreate.GroupId = groupGet.GroupId;
				}
				else { return false; }
			}
			else
			{
				userCreate.Group = defaultGroup;
				_groupRepository.InsertGroup(defaultGroup);
			}

			_userRepository.Insert(userCreate);
			_gamificationRepository.Insert(gamification);


			if (_context.SaveChanges() > 0)
				return true;

			return false;

		}
	}
}
