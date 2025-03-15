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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(CompanyDbContext companyDbContext) : base(companyDbContext)
        {
        }
        
       
    }
}
