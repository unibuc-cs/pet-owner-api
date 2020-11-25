﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Models
{
	public class Tip
	{
		[Key]
		public int TipId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

	}
}
