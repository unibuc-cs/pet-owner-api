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
	}
}
