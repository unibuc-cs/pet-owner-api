using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetOwner.DTOs;
using PetOwner.Helpers;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using PetOwner.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PetOwner.Services.Implementations
{
	public class LoginService : ILoginService
	{
		private readonly AppSettings _appSettings;
		private readonly IUserRepository _userRepository;
		public LoginService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
		{
			_appSettings = appSettings.Value;
			_userRepository = userRepository;
		}

		public string Authentificate(UserLoginRequest userLogin)
		{
			var user = _userRepository.Login(userLogin);

			if (user == null) return null;

			var token = GenerateJWTToken(user);

			return token;
		}

		public string GenerateJWTToken(User user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();

			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);

		}
	}
}
