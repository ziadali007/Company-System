using Business_Logic_Layer.Interfaces;
using Data_Access_Layer.Data.Contexts;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext _context;
        public EmployeeRepository(CompanyDbContext companyDbContext) : base(companyDbContext)
        {
            _context= companyDbContext;
        }

        public IEnumerable<Employee> GetByName(string name)
        {
            return _context.Employees.Include(E=>E.Department).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}
