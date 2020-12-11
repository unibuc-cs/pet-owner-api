﻿using PetOwner.Data;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Repository.Implementations
{
	public class TipRepository : GenericRepository<Tip>, ITipRepository
	{
		public TipRepository(PetOwnerContext _context) : base(_context)
		{

		}
	}
}