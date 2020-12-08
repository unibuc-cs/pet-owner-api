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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetOwner.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GroupController : ControllerBase
	{
		private readonly IGroupRepository _groupRepository;
		private readonly IUserRepository _userRepository;
		private readonly IPetRepository _petRepository;
		private readonly PetOwnerContext _context;

		public GroupController(IGroupRepository groupRepository, IUserRepository userRepository, PetOwnerContext context, IPetRepository petRepository)
		{
			_groupRepository = groupRepository;
			_userRepository = userRepository;
			_petRepository = petRepository;
			_context = context;
		}
		// GET: api/<GroupController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<GroupController>/5  
		[HttpGet("{id}")]   // get group by group id
		public ActionResult<Group> Get(int id)
		{
			var group = _groupRepository.Get(id);

			if (group != null)
			{
				return Ok(group);
			}
			return BadRequest();
		}

		[HttpGet("{id}/users")] // get group users by group id
		public ActionResult<List<UserProfileDTO>> GetUsers(int id)
		{
			var users = _context.Users.Where(x => x.GroupId == id).ToList();

			if (users == null)
			{
				return BadRequest();
			}

			List<UserProfileDTO> result = new List<UserProfileDTO>();

			foreach (User user in users)
			{
				var userLevelVip = _userRepository.GetUserWithLevelVip(user.UserId);

				result.Add(userLevelVip.ToProfile());
			}

			return Ok(result);
		}

		// POST api/<GroupController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PATCH api/<GroupController>/5 
		[HttpPatch("{groupid}/user")]    //	add user to group
		public ActionResult Patch(int groupid, [FromBody] JObject data)
		{
			int userid = Int32.Parse(data["userid"].ToString());

			var user = _userRepository.Get(userid);

			if (user == null) return BadRequest();

			int oldGroup = user.GroupId;

			user.GroupId = groupid;

			Delete(oldGroup);

			return Ok();

		}

		[HttpPatch("{groupid}/name")]
		public ActionResult UpdateName(int groupid, [FromBody] JObject data)
		{
			string groupName = data["groupname"].ToString();

			var group = _groupRepository.Get(groupid);

			group.GroupName = groupName;

			if (_groupRepository.Save()) return Ok();

			return BadRequest();

		}


		// PUT api/<GroupController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<GroupController>/5  
		[HttpDelete("{id}")]    //	delete group & and default group to user
		public ActionResult Delete(int id)
		{

			var user = _context.Users.Where(x => x.GroupId == id).FirstOrDefault();

			Group groupNew = new Group
			{
				GroupName = "default",
			};

			_groupRepository.Insert(groupNew);

			user.Group = groupNew;

			_context.SaveChanges();

			_groupRepository.Delete(_groupRepository.Get(id));

			if (_groupRepository.Save()) return Ok();
			
			return BadRequest();
		}


		[HttpDelete("user/{userid}")]
		public ActionResult RemoveFromGroup(int userid)
		{
			var user = _userRepository.Get(userid);

			Group groupNew = new Group
			{
				GroupName = "default",
			};

			_groupRepository.Insert(groupNew);

			user.Group = groupNew;

			if (_groupRepository.Save()) return Ok();

			return BadRequest();
		}


	}
}
