using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PetOwner.Data;
using PetOwner.Helpers;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetOwner.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class PetController : ControllerBase
	{
		private readonly IPetRepository _petRepository;
		private readonly PetOwnerContext _context;
		private readonly IUserRepository _userRepository;
		private readonly IPetActivityRepository _petActivityRepository;

		public PetController(IPetRepository petRepository, PetOwnerContext context, IUserRepository userRepository,
			IPetActivityRepository petActivityRepository)
		{
			_petRepository = petRepository;
			_context = context;
			_userRepository = userRepository;
			_petActivityRepository = petActivityRepository;
		}
		// GET: api/<PetController>
		[HttpGet("group/{id}")]  // get group pets for group id
		public ActionResult<List<Pet>> GetPets(int id)
		{
			var pets = _petRepository.GetGroupPets(id);

			if (pets != null)
			{
				return Ok(pets);
			}

			return Ok(new {errorcode = Errors.ErrorCode.Group_Pets_Not_Found });

		}

		// GET api/<PetController>/5
		[HttpGet("{id}")]
		public ActionResult<Pet> Get(int id)
		{
			var pet = _petRepository.Get(id);
			if(pet != null)
			{
				return Ok(pet);
			}

			return Ok("Pet not found");
			
		}



		// POST api/<PetController>	// post pet and add to group
		[HttpPost]
		public ActionResult Post([FromBody] JObject data)
		{
			if (data["userid"].ToString() == null)
			{
				return Ok(new { errorcode = Errors.ErrorCode.Invalid_Json_Object });
			}

			int userid = Int32.Parse(data["userid"].ToString());

			var value = data["pet"].ToObject<Pet>();

			int groupid = _userRepository.Get(userid).GroupId;

			if (groupid == 0) return Ok(new {errorcode = Errors.ErrorCode.Group_Not_Found });

			value.GroupId = _context.Groups
				.Where(x => x.GroupId == groupid)
				.FirstOrDefault().GroupId;

			_petRepository.Insert(value);


			if (_petRepository.Save())
			{
				return Ok("Pet added to group");
			}

			return Ok(new {errorcode = Errors.ErrorCode.Insert_Pet_To_Group_Failed });
		}

		// PUT api/<PetController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		[HttpPatch("{petid}")] // update pet
		public ActionResult Patch(int petid, [FromBody] Pet value)
		{
			var petUpdate = _petRepository.Get(petid);

			if (petUpdate == null) return Ok(new {errorcode = Errors.ErrorCode.Pet_Not_Found });

			if (value.PetName != null) petUpdate.PetName = value.PetName;

			if (value.Age != 0) petUpdate.Age = value.Age;

			if (value.Race != null) petUpdate.Race = value.Race;

			if (value.Species != null) petUpdate.Species = value.Species;

			if (value.Weigth != 0) petUpdate.Weigth = value.Weigth;

			if (value.Photo != null) petUpdate.Photo = value.Photo;

			if (_petRepository.Save()) return Ok("Update successfully");

			return Ok(new {errorcode = Errors.ErrorCode.Pet_Update_Failed });


		}


		[HttpDelete("{petid}")]  // delete pet
		public ActionResult DeletePet(int petid)
		{

			_petRepository.Delete(_petRepository.Get(petid));

			if(_petRepository.Save()) return Ok("Pet deleted");

			return Ok(new { errorcode = Errors.ErrorCode.Pet_Not_Found });
		}
	}
}
