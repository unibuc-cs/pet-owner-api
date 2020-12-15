using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PetOwner.Data;
using PetOwner.DTOs;
using PetOwner.Helpers;
using PetOwner.Mappers;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetOwner.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class VipController : ControllerBase
	{
		private readonly IVipRepository _vipRepository;
		private readonly IUserRepository _userRepository;
		private readonly PetOwnerContext _context;

		public VipController(IVipRepository vipRepository, IUserRepository userRepository, PetOwnerContext context)
		{
			_vipRepository = vipRepository;
			_userRepository = userRepository;
			_context = context;
		}
		// GET: api/<VipController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		[HttpPost]	// add vip to user
		public ActionResult<Vip> Post([FromBody] JObject data)
		{
			if (data["id"].ToString() == null)
			{
				return Ok(new { errorcode = Errors.ErrorCode.Invalid_Json_Object });
			}

			int userid = Int32.Parse(data["id"].ToString());
			var user = _userRepository.Get(userid);

			if (user.VipId != null) return Ok(new {errocode = Errors.ErrorCode.User_Already_Has_Vip });


			var vipCreate = new Vip
			{
				StartDate = DateTime.UtcNow,
				EndDate = DateTime.UtcNow.AddDays(30),
				ExpMultiplier = new decimal(1.5),
			};

			user.Vip = vipCreate;

			_userRepository.Update(user);

			_vipRepository.Insert(vipCreate);

			if(_context.SaveChanges() > 0)
			{
				var result = _vipRepository.Get((int)_userRepository.Get(userid).VipId);
				return Ok(result);
			}

			return Ok(new {errorcode = Errors.ErrorCode.Insert_Vip_Error });
		}


		// GET api/<VipController>
		[HttpGet("{id}")]	// get user level vip objects for profile screen
		public ActionResult<UserProfileDTO> Get(int id)
		{
			User user = _userRepository.GetUserWithLevelVip(id);

			if(user != null)
			{
				return Ok(user.ToProfile());
			}


			return Ok(new {errorcode = Errors.ErrorCode.User_Not_Found });

		}

		// PUT api/<VipController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<VipController>/5
		[HttpDelete("{id}")]	// delete vip from user with vipid
		public ActionResult Delete(int id)
		{
			var user = _context.Users.Where(x => x.VipId == id).FirstOrDefault();

			if(user != null)
			{
				user.VipId = null;
			}
			else
			{
				return Ok(new { errorcode = Errors.ErrorCode.User_Doesnt_Have_Vip });
			}
			

			_context.SaveChanges();

			_vipRepository.Delete(_vipRepository.Get(id));

			if (_vipRepository.Save())
			{
				return Ok();
			}

			return Ok(new {errorcode = Errors.ErrorCode.VipId_Not_Found });
		}
	}
}
