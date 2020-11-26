using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Repository.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		List<T> GetAll();
		T Get(int id);
		void Insert(T entity);
		void InsertRange(List<T> entities);
		void Update(T value);
		void Delete(T entity);
		bool Save();

	}
}
