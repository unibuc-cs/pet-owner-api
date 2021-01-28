using PetOwner.Data;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Repository.Interfaces
{
	public interface IPetActivityRepository : IGenericRepository<PetActivity>
	{
		List<PetActivity> GetPetActivities(int petid);
		PetActivity GetPetActivity(int petid, int activityid);
	}
}
