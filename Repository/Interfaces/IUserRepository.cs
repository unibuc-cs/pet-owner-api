using PetOwner.Data;
using PetOwner.DTOs;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Repository.Interfaces
{
	public interface IUserRepository : IGenericRepository<User>
	{
		User Login(UserLoginRequest userLogin);
		User GetByEmailAndPassword(string username, string password);
		User GetUserWithLevelVip(int id);

		User GetUserByEmail(string email);
	}
}
