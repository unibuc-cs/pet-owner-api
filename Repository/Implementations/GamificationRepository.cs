﻿using PetOwner.Data;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Repository.Implementations
{
	public class GamificationRepository : GenericRepository<Gamification>, IGamificationRepository
	{
		public GamificationRepository(PetOwnerContext _context) : base(_context)
		{

		}
	}
}
