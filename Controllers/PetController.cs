using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PetOwner.Data;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetOwner.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PetController : ControllerBase
	{
		private readonly IPetRepository _petRepository;
		private readonly PetOwnerContext _context;
		private readonly IUserRepository _userRepository;

		public PetController(IPetRepository petRepository, PetOwnerContext context, IUserRepository userRepository)
		{
			_petRepository = petRepository;
			_context = context;
			_userRepository = userRepository;
		}
		// GET: api/<PetController>
		[HttpGet("group/{id}")]
		public ActionResult<List<Pet>> GetPets(int id)
		{
			var pets = _context.Pet.Where(x => x.GroupId == id).ToList();

			if(pets != null)
			{
				return Ok(pets);
			}

			return BadRequest();
			
		}

		// GET api/<PetController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<PetController>
		[HttpPost]
		public ActionResult Post([FromBody] JObject data)
		{
			int userid = Int32.Parse(data["userid"].ToString()) ;

			var value = data["pet"].ToObject<Pet>();

			int groupid = _userRepository.Get(userid).GroupId;

			value.GroupId = _context.Groups
				.Where(x => x.GroupId == groupid)
				.FirstOrDefault().GroupId;
			
			_petRepository.Insert(value);
			

			if (_petRepository.Save())
			{
				return Ok();
			}

			return BadRequest();
		}

		// PUT api/<PetController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<PetController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
