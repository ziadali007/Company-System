using Business_Logic_Layer.Interfaces;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation_Layer.Dtos;

namespace Presentation_Layer.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IDepartmentRepository DepartmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            DepartmentRepository = departmentRepository;
        }


        public IActionResult Index(string? Search)
        {
            IEnumerable<Employee> employees;
            if (Search is not null)
            {
                employees = _employeeRepository.GetByName(Search);
                return View(employees);
            }
            else
            {
                employees = _employeeRepository.GetAll();
                return View(employees);
            }
           
            //ViewData["Message"]= "Welcome To Employee Page";

            //ViewBag.message = "Welcome To Employee Page";
        }

        [HttpGet]

        public IActionResult Create()
        {
            var departments = DepartmentRepository.GetAll();
            ViewData["departments"] = departments;
            return View();
        }


        [HttpPost]
        public IActionResult Create(CreateEmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Name = employeeDto.Name,
                    Age = employeeDto.Age,
                    Address = employeeDto.Address,
                    Email = employeeDto.Email,
                    Phone = employeeDto.Phone,
                    Salary = employeeDto.Salary,
                    IsActive = employeeDto.IsActive,
                    IsDeleted = employeeDto.IsDeleted,
                    HiringDate = employeeDto.HiringDate,
                    CreateAt = employeeDto.CreateAt,
                    DepartmentId =employeeDto.DepartmentId
                };
                var Count = _employeeRepository.Add(employee);
                if (Count > 0)
                {
                    TempData["Message"] = "Employee Added Successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(employeeDto);
        }
        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");

            var employee = _employeeRepository.Get(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"Employee With Id {id} Is Not Found" });

            return View(ViewName, employee);

        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var departments = DepartmentRepository.GetAll();
            ViewData["departments"] = departments;
            if (id is null) return BadRequest("Invalid Id");

            var employee = _employeeRepository.Get(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id {id} Is Not Found" });

            var employeeDto = new CreateEmployeeDto()
            {
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                IsDeleted = employee.IsDeleted,
                HiringDate = employee.HiringDate,
                CreateAt = employee.CreateAt,
                DepartmentId = employee.DepartmentId
            };
            return View(employeeDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                var Count = _employeeRepository.Update(employee);
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(employee);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit([FromRoute] int id, CreateDepartmentDto department)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var departmentModel = new Department()
        //        {
        //            Id = id,
        //            Code = department.Code,
        //            Name = department.Name,
        //            CreateAt = department.CreateAt
        //        };
        //        var Count = _departmentRepository.Update(departmentModel);
        //        if (Count > 0)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return View(department);
        //}

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id");

            //var department = _departmentRepository.Get(id.Value);

            //if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id {id} Is Not Found" });

            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                var Count = _employeeRepository.Delete(employee);
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(employee);
        }
    }
}
