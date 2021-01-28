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
using PetOwner.Helpers;

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
		private readonly ILoginService _loginService;
		public UserController(IUserRepository userRepository,IRegisterService registerService,
			PetOwnerContext context, ILoginService loginService)
		{
			_userRepository = userRepository;
			_registerService = registerService;
			_context = context;
			_loginService = loginService;
		}

		// GET: api/<UserController>
		[HttpPost("login")]
		public ActionResult Login(UserLoginRequest userLogin)
		{
			var user = _userRepository.Login(userLogin);
			//if(user != null)
				//return Ok(user.UserId);

			var token = _loginService.Authentificate(userLogin);

			if (token != null)
				return Ok(new {usertoken =  token, userid = user.UserId });

			return Ok(new {errorcode = Errors.ErrorCode.Email_Or_Password_Invalid });
		}

		[HttpPost("register")]
		public ActionResult Register(UserRegisterRequest userRegister)
		{
			bool res = _registerService.Register(userRegister);
			if (res)
			{
				
				var userResponse = _userRepository.GetByEmailAndPassword(userRegister.Email, userRegister.Password);
				var token = _loginService.Authentificate(new UserLoginRequest {
					Email = userResponse.Email, 
					Password = userResponse.Password
				});
				return Ok(new { userid = userResponse.UserId, usertoken = token });
			}

			return Ok(new {errorcode = Errors.ErrorCode.Email_Already_Used });
		}

		
		// GET api/<UserController>/5
		[HttpGet("{id}")]  // get user by user id
		[Authorize]
		public ActionResult<User> Get(int id)
		{
			var user = _userRepository.Get(id);

			if (user != null) return Ok(user);


			return Ok(new {errorcode = Errors.ErrorCode.User_Not_Found });
		}

		[HttpGet("home/{id}")]  // get user with level and vip objects for home screen by user id
		[Authorize]
		public ActionResult<UserHomeResponse> GetHome(int id)
		{
			User user = _userRepository.GetUserWithLevelVip(id);

			if (user == null) return Ok(new {errorcode = Errors.ErrorCode.User_Not_Found });

			UserHomeResponse response = user.ToUserHome();

			return Ok(response);
		}

		[HttpGet("leaderboards")]   // get  top {size} users  vip/notvip for leaderboards
		[Authorize]
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

			if(users == null)
			{
				return Ok(new {errorcode = Errors.ErrorCode.User_Not_Found});
			}

			List<UserLeaderboardsDTO> leaderboards = new List<UserLeaderboardsDTO>();
			foreach(User user in users)
			{
				leaderboards.Add(user.ToLeaderboards());
			}

			return Ok(leaderboards);
		}

		// PATCH api/<UserController>/5	// update photo, name for user by user id
		[HttpPatch("{id}")]
		[Authorize]
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
		[Authorize]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<UserController>/5
		[HttpDelete("{id}")]    // delete user
		[Authorize]
		public ActionResult Delete(int id)
		{
			_userRepository.Delete(_userRepository.Get(id));

			if(_userRepository.Save()) return Ok();

			return Ok(new { errorcode = Errors.ErrorCode.User_Not_Found });

		}
	}
}
