using PetOwner.Data;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Repository.Interfaces
{
	public interface IItemRepository : IGenericRepository<Item>
	{
		List<Item> GetItemsByDate(int groupid, DateTime start, DateTime end);
	}
}
