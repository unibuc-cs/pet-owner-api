using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PetOwner.Data;
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
		private readonly PetOwnerContext _context;
		public UserController(IUserRepository userRepository, IRegisterService registerService, PetOwnerContext context)
		{
			_userRepository = userRepository;
			_registerService = registerService;
			_context = context;
		}

		// GET: api/<UserController>
		[HttpPost("login")]
		public ActionResult<int> Login(UserLoginRequest userLogin)
		{
			var user = _userRepository.Login(userLogin);
			if(user != null)
				return Ok(user.UserId);

			return BadRequest();
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

		[HttpGet("leaderboards")]
		public ActionResult<List<User>> GetLeaderboards([FromBody] JObject data)
		{
			int size = Int32.Parse(data["size"].ToString());
			bool isVip = Boolean.Parse(data["isvip"].ToString());

			List<User> users;

			if (isVip)
			{
				users = _context.Users.Where(x => x.VipId != null)
					.Include(x => x.Level)
					.OrderByDescending(x => x.Level.WeeklyExp)
					.Take(size)
					.ToList();
			}
			else
			{
				users = _context.Users.Where(x => x.VipId == null)
					.Include(x => x.Level)
					.OrderByDescending(x => x.Level.WeeklyExp)
					.Take(size)
					.ToList();
			}

			return Ok(users);
		}

		// PATCH api/<UserController>/5
		[HttpPatch("{id}")]
		public ActionResult Patch(int id, [FromBody] JObject data)
		{
			var name = data["name"].ToString();
			var photo = data["photo"].ToString();

			var userPatch = _userRepository.Get(id);

			if(name != null)
			{
				userPatch.Name = name;
			}

			if(photo != null)
			{
				userPatch.Photo = photo;
			}

			_userRepository.Update(userPatch);

			return Ok(_userRepository.Save());
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
