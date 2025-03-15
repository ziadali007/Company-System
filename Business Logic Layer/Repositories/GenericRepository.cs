using Business_Logic_Layer.Interfaces;
using Data_Access_Layer.Data.Contexts;
using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _context;
        public GenericRepository(CompanyDbContext context) 
        {
          _context = context;
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T? Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public int Add(T department)
        {
            _context.Set<T>().Add(department);
            return _context.SaveChanges();
        }

        public int Update(T department)
        {
            _context.Set<T>().Update(department);
            return _context.SaveChanges();
        }
        public int Delete(T department)
        {
            _context.Set<T>().Remove(department);
            return _context.SaveChanges();
        }
    }
}
