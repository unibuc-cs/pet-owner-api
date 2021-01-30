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

		public List<Activity> GetByTitle(string title)
		{
			return _context.Activity.Where(x => x.Title.Contains(title)).ToList();
		}

		public Activity GetByTitleAndDescription(string title, string description)
		{
			return _context.Activity.Where(x => x.Title.Contains(title) && x.Description.Contains(description)).FirstOrDefault();
		}
	}
}
