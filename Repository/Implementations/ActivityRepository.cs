using PetOwner.Data;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Repository.Implementations
{
	public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
	{
		public ActivityRepository(PetOwnerContext _context) : base(_context)
		{

		}

		public Activity GetByTitle(string title)
		{
			return _context.Activity.Where(x => x.Title.Contains(title)).FirstOrDefault();
		}
	}
}
