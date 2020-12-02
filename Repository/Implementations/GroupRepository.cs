using PetOwner.Data;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Repository.Implementations
{
	public class GroupRepository : GenericRepository<Group>, IGroupRepository
	{
		public GroupRepository(PetOwnerContext _context) : base(_context)
		{

		}
	}
}
