using PetOwner.Data;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Repository.Implementations
{
	public class ItemRepository : GenericRepository<Item>, IItemRepository
	{
		public ItemRepository(PetOwnerContext _context) : base(_context)
		{

		}

		public List<Item> GetItemsByDate(int groupid, DateTime start, DateTime end)
		{

			return _context.Item.Where(x => x.GroupId == groupid && x.RecordStamp >= start && x.RecordStamp <= end).ToList();
		}
	}
}
