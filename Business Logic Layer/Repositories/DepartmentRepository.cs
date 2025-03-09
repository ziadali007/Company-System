﻿using Business_Logic_Layer.Interfaces;
using Data_Access_Layer.Data.Contexts;
using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly CompanyDbContext _context;

        public DepartmentRepository(CompanyDbContext companyDbContext)
        {
            _context = companyDbContext;
        }
        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department? Get(int id)
        {
            return _context.Departments.Find(id);
        }

        public int Add(Department department)
        {
            _context.Departments.Add(department);
            return _context.SaveChanges();
        }

        public int Update(Department department)
        {
            _context.Departments.Update(department);
            return _context.SaveChanges();
        }
        public int Delete(Department department)
        {
            _context.Departments.Remove(department);
            return _context.SaveChanges();
        }
      
    }
}
