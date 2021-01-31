using PetOwner.Data;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Repository.Interfaces
{
	public interface IGroupRepository : IGenericRepository<Group>
	{
		void InsertGroup(Group group);
		Group GetByInviteCode(string code);
		Group GetGroupWithMembersAndPets(int groupid);
	}
}
