using PetOwner.Data;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Repository.Implementations
{
	public class PetRepository : GenericRepository<Pet>, IPetRepository
	{
		public PetRepository(PetOwnerContext _context) : base(_context)
		{

		}

		public List<Pet> GetGroupPets(int groupid)
		{
			return _context.Pet.Where(x => x.GroupId == groupid).ToList();
		}
	}
}
