using Microsoft.EntityFrameworkCore;
using PetOwner.Data;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Repository.Implementations
{
	public class PetActivityRepository : GenericRepository<PetActivity>, IPetActivityRepository
	{
		public PetActivityRepository(PetOwnerContext _context) : base(_context)
		{

		}

		public List<PetActivity> GetPetActivities(int petid)
		{
			return _context.PetActivity.Where(x => x.PetId == petid)
				.Include(x => x.Activity).ToList();
		}

		public PetActivity GetPetActivity(int petid, int activityid)
		{
			return _context.PetActivity.Where(x => x.PetId == petid && x.ActivityId == activityid).FirstOrDefault();
		}
	}
}
