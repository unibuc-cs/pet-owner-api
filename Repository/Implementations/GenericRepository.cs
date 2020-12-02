using Microsoft.EntityFrameworkCore;
using PetOwner.Data;
using PetOwner.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Repository.Implementations
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		protected readonly PetOwnerContext _context;
		protected readonly DbSet<T> _table;

		public GenericRepository(PetOwnerContext context)
		{
			_context = context;
			_table = context.Set<T>();
		}

		public void Delete(T entity)
		{
			_table.Remove(entity);
		}

		public T Get(int id)
		{
			return _table.Find(id);
		}

		public List<T> GetAll()
		{
			return _table.ToList();
		}

		public void Insert(T entity)
		{
			_table.Add(entity);
		}

		public void InsertRange(List<T> entities)
		{
			_table.AddRange(entities);
		}

		public bool Save()
		{
			return (_context.SaveChanges() > 0);
		}

		public void Update(T entity)
		{
			_table.Update(entity);
		}
	}
}
