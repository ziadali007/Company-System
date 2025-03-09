using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }


        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }
    }
}
