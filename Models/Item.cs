using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Models
{
	public class Item
	{
		[Key]
		public int ItemId { get; set; }
		public int GroupId { get; set; }
		public DateTime RecordStamp { get; set; }
		public string Category { get; set; }
		public string ItemName { get; set; }
		public Decimal Money { get; set; }
		public Group Group { get; set; }
	}
}
