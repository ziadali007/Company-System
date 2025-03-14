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
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly CompanyDbContext _companyDbContext;

        public EmployeeRepository(CompanyDbContext companyDbContext) {
            _companyDbContext = companyDbContext;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _companyDbContext.Employees.ToList();
        }

        public Employee? Get(int id)
        {
           return _companyDbContext.Employees.Find(id);
        }

        public int Add(Employee department)
        {
            _companyDbContext.Employees.Add(department);
            return _companyDbContext.SaveChanges();
        }

        public int Update(Employee department)
        {
            _companyDbContext.Employees.Update(department);
            return _companyDbContext.SaveChanges();
        }
        public int Delete(Employee department)
        {
            _companyDbContext.Employees.Remove(department);
            return _companyDbContext.SaveChanges();
        }
       
    }
}
