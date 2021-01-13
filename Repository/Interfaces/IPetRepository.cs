using PetOwner.Data;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Repository.Interfaces
{
	public interface IPetRepository : IGenericRepository<Pet>
	{
		List<Pet> GetGroupPets(int groupid);
	}
}
