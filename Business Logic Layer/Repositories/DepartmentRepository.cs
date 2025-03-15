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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {       
        public DepartmentRepository(CompanyDbContext companyDbContext) : base(companyDbContext)
        {
        }
       
    }
}
