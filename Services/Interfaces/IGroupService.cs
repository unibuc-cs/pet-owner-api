using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Services.Interfaces
{
	public interface IGroupService
	{
		string GenerateInviteCode(int size);
	}
}
