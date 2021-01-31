using PetOwner.Data;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Repository.Interfaces
{
	public interface IActivityRepository : IGenericRepository<Activity>
	{
		List<Activity> GetByTitle(string title);
		Activity GetByTitleAndDescription(string title, string description, int exp);
	}
}
