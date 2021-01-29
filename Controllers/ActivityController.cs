using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PetOwner.DTOs;
using PetOwner.Mappers;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetOwner.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ActivityController : ControllerBase
	{
		private readonly IPetActivityRepository _petActivityRepository;
		private readonly IActivityRepository _activityRepository;

		public ActivityController(IPetActivityRepository petActivityRepository,
			IActivityRepository activityRepository)
		{
			_petActivityRepository = petActivityRepository;
			_activityRepository = activityRepository;
		}

		// GET: api/<ActivityController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		[HttpGet("{id}/activities")]
		public ActionResult<Pet> GetPetActivities(int id)
		{
			List<PetActivity> activities = _petActivityRepository.GetPetActivities(id);

			var petactivities = new List<PetActivityDTO>();

			foreach(PetActivity act in activities)
			{
				PetActivityDTO aux = ToPetActivity.MergeObjects(act, act.Activity);
				petactivities.Add(aux);
			}

			if (petactivities != null)
			{
				return Ok(petactivities);
			}

			return Ok("eroare");
		}

		// GET api/<ActivityController>/5
		[HttpGet("activity/{id}")]
		public ActionResult<Activity> Get(int id)
		{
			var activity = _activityRepository.Get(id);

			if (activity != null)
				return Ok(activity);

			return Ok("eroare");
		}

		// POST api/<ActivityController>
		[HttpPost("petactivity")]
		public ActionResult Post([FromBody] PetActivityDTO value)
		{
			var activity = ToPetActivity.DiviteActivity(value);
			var petactivity = ToPetActivity.DividePetActivity(value);
			petactivity.PetId = value.PetId;
			_activityRepository.Insert(activity);
			_activityRepository.Save();

			int activityId = _activityRepository.GetByTitleAndDescription(value.Title, value.Description).ActivityId;

			petactivity.ActivityId = activityId;
			_petActivityRepository.Insert(petactivity);

			if (_petActivityRepository.Save())
			{
				return Ok(new {petid = value.PetId, activityid = activityId});
			}

			return Ok("eroare");

		}

		[HttpPost("activity")]
		public ActionResult PostActivity([FromBody] Activity value)
		{
			_activityRepository.Insert(value);
			_activityRepository.Save();

			var insertedActivity = _activityRepository.GetByTitleAndDescription(value.Title, value.Description);

			if (insertedActivity != null)
			{
				return Ok(new {activityid = insertedActivity.ActivityId });
			}

			return Ok("eroare");

		}

		[HttpPost("attach")]
		public ActionResult PostAttach([FromBody] PetActivityDTO value)
		{
			PetActivity petactivity = ToPetActivity.DividePetActivity(value);
			_petActivityRepository.Insert(petactivity);

			if (_petActivityRepository.Save())
			{
				return Ok(new {petid = value.PetId, activityid = value.ActivityId });
			}

			return Ok("eroare");

		}


		// PUT api/<ActivityController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		[HttpPatch("activity/{activityid}")]
		public ActionResult PatchActivity(int activityid, [FromBody] Activity value)
		{
			var activity = _activityRepository.Get(activityid);

			if (value.Description != null) activity.Description = value.Description;

			if (value.ExpPoints != 0) activity.ExpPoints = value.ExpPoints;

			if (value.Title != null) activity.Title = value.Title;

			if (_activityRepository.Save())
			{
				return Ok(new {activityid = activityid });
			}

			return Ok("eroare");

		}

		[HttpPatch("petactivity/{petid}/{activityid}")]
		public ActionResult PatchPetActivity(int petid, int activityid, [FromBody] PetActivity value)
		{
			var petactivity = _petActivityRepository.GetPetActivity(petid, activityid);

			if (value.Data != default(DateTime)) petactivity.Data = value.Data;

			if (value.Recurring == true || value.Recurring == false) petactivity.Recurring = value.Recurring;

			if (value.RecurringInterval != 0) petactivity.RecurringInterval = value.RecurringInterval;

			if (_petActivityRepository.Save())
			{
				return Ok(new {petid = petid, activityid = activityid });
			}

			return Ok("eroare");

		}



		// DELETE api/<ActivityController>/5
		[HttpDelete("activity/{id}")]
		public ActionResult Delete(int id)
		{
			_activityRepository.Delete(_activityRepository.Get(id));

			if (_activityRepository.Save())
			{
				return Ok(new {message =  "Successfuly deleted" });
			}

			return Ok("eroare");
		}

		[HttpDelete("petactivity/{petid}/{activityid}")]
		public ActionResult DeletePetActivity(int petid, int activityid)
		{
			_petActivityRepository.Delete(_petActivityRepository.GetPetActivity(petid, activityid));
			if (_petActivityRepository.Save())
			{
				return Ok(new { message = "Successfuly deleted" });
			}

			return Ok("eroare");

		}

	}
}
