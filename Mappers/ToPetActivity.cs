using PetOwner.DTOs;
using PetOwner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Mappers
{
	public static class ToPetActivity
	{
		public static PetActivityDTO MergeObjects(PetActivity pa, Activity a)
		{
			var padto = new PetActivityDTO
			{
				PetId = pa.PetId,
				ActivityId = pa.ActivityId,
				Data = pa.Data,
				Recurring = pa.Recurring,
				RecurringInterval = pa.RecurringInterval,
				Description = a.Description,
				ExpPoints = a.ExpPoints,
				Title = a.Title,
			};

			return padto;
		}

		public static PetActivity DividePetActivity(PetActivityDTO pa)
		{
			var petactivity = new PetActivity
			{
				PetId = pa.PetId,
				Data = pa.Data,
				Recurring = pa.Recurring,
				RecurringInterval = pa.RecurringInterval,
			};

			if(pa.ActivityId != 0)
			{
				petactivity.ActivityId = pa.ActivityId;
			}

			return petactivity;
		}

		public static Activity DiviteActivity(PetActivityDTO a)
		{
			var activity = new Activity
			{
				Description = a.Description,
				ExpPoints = a.ExpPoints,
				Title = a.Title,
			};

			return activity;
		}

	}
}
