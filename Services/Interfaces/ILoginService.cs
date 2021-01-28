using PetOwner.DTOs;
using PetOwner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Services.Interfaces
{
	public interface ILoginService
	{
		string Authentificate(UserLoginRequest userLogin);
		string GenerateJWTToken(User user);
	}
}
