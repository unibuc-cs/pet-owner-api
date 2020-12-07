using PetOwner.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Services.Interfaces
{
	public interface IRegisterService
	{
		bool Register(UserRegisterRequest user);
	}
}
