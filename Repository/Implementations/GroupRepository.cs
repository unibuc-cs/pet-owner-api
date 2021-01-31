using Microsoft.EntityFrameworkCore;
using PetOwner.Data;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using PetOwner.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Repository.Implementations
{
	public class GroupRepository : GenericRepository<Group>, IGroupRepository
	{
		private readonly IGroupService _groupService;
		public GroupRepository(PetOwnerContext _context, IGroupService groupService) : base(_context)
		{
			_groupService = groupService;

		}

		public Group GetByInviteCode(string code)
		{
			return _context.Groups.Where(x => x.InviteCode == code).FirstOrDefault();
		}

		public Group GetGroupWithMembersAndPets(int groupid)
		{
			var group = _context.Groups.Where(x => x.GroupId == groupid)
				.Include(x => x.Pets)
				.Include(x => x.Users).FirstOrDefault();

			return group;
		}

		public void InsertGroup(Group group)
		{
			group.InviteCode = _groupService.GenerateInviteCode(5);

			_context.Add(group);
		}
	}
}
