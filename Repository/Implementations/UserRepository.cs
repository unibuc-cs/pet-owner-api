﻿using Microsoft.EntityFrameworkCore;
using PetOwner.Data;
using PetOwner.DTOs;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Repository.Implementations
{
	public class UserRepository : GenericRepository<User>, IUserRepository
	{
		private readonly IVipRepository _vipRepository;
		public UserRepository(PetOwnerContext _context, IVipRepository vipRepository) : base(_context)
		{
			_vipRepository = vipRepository;
		}

		public User GetByEmailAndPassword(string email, string password)
		{
			return _table.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
		}

		public User GetUserByEmail(string email)
		{
			//return _table.Where(x => x.Email == email).FirstOrDefault();
			return _context.Users.Where(x => x.Email == email).FirstOrDefault();
		}

		public User GetUserWithLevelVip(int id)
		{
			User user = _context.Users
				.Include(x => x.Level)
				.Where(x => x.UserId == id)
				.FirstOrDefault();

			if(user != null && user.VipId != null)
			{
				user.Vip = _vipRepository.Get((int)user.VipId);
			}

			return user;
		}

		public User Login(UserLoginRequest userLogin)
		{
			var user = this.GetByEmailAndPassword(userLogin.Email, userLogin.Password);

			return user;
		}

	}
}
