﻿using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IEnumerable<Employee> GetAll();

        Employee? Get(int id);

        int Add(Employee department);


        int Update(Employee department);


        int Delete(Employee department);
    }
}
