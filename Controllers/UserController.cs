using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetOwner.DTOs;
using PetOwner.Mappers;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using PetOwner.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetOwner.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		private readonly IRegisterService _registerService;
		//private readonly GroupRepository _groupRepository;
		public UserController(IUserRepository userRepository, IRegisterService registerService)
		{
			_userRepository = userRepository;
			_registerService = registerService;
		}

		// GET: api/<UserController>
		[HttpPost("login")]
		public ActionResult<int> Login(UserLoginRequest userLogin)
		{
			var user = _userRepository.Login(userLogin);
			return Ok(user.UserId);
		}

		[HttpPost("register")]
		public ActionResult<int> Register(UserRegisterRequest userRegister)
		{
			bool res = _registerService.Register(userRegister);
			if (res)
			{
				var userResponse = _userRepository.GetByEmailAndPassword(userRegister.Email, userRegister.Password);
				return Ok(userResponse.UserId);
			}

			return BadRequest();
		}

		// GET api/<UserController>/5
		[HttpGet("{id}")]
		public ActionResult<User> Get(int id)
		{
			return Ok(_userRepository.Get(id));
		}

		[HttpGet("home/{id}")]
		public ActionResult<UserHomeResponse> GetHome(int id)
		{
			User user = _userRepository.GetUserWithLevelVip(id);

			UserHomeResponse response = user.ToUserHome();

			return Ok(response);
		}

		// PUT api/<UserController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<UserController>/5
		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			_userRepository.Delete(_userRepository.Get(id));
			return Ok(_userRepository.Save());
		}
	}
}
