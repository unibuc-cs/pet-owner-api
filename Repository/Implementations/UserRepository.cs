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
		public UserRepository(PetOwnerContext _context) : base(_context)
		{

		}

		public User GetByEmailAndPassword(string email, string password)
		{
			return _table.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
		}

		public User Login(UserLoginRequest userLogin)
		{
			var user = this.GetByEmailAndPassword(userLogin.Email, userLogin.Password);

			return user;
		}
	}
}
