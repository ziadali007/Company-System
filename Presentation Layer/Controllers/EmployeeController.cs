using AutoMapper;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Repositories;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation_Layer.Dtos;
using Presentation_Layer.Helpers;

namespace Presentation_Layer.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
             _mapper = mapper;
        }


        public IActionResult Index(string? Search)
        {
            IEnumerable<Employee> employees;
            if (Search is not null)
            {
                employees = _unitOfWork.employeeRepository.GetByName(Search);
                return View(employees);
            }
            else
            {
                employees = _unitOfWork.employeeRepository.GetAll();
                return View(employees);
            }
           
            //ViewData["Message"]= "Welcome To Employee Page";

            //ViewBag.message = "Welcome To Employee Page";
        }

        [HttpGet]

        public IActionResult Create()
        {
            var departments = _unitOfWork.departmentRepository.GetAll();
            ViewData["departments"] = departments;
            return View();
        }


        [HttpPost]
        public IActionResult Create(CreateEmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                if(employeeDto.Image is not null)
                {
                    employeeDto.ImageName = DocumentSetting.UploadFile(employeeDto.Image, "images");
                }
                
                var employee = _mapper.Map<Employee>(employeeDto);
                _unitOfWork.employeeRepository.Add(employee);
                var Count = _unitOfWork.Complete();
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

            var employee = _unitOfWork.employeeRepository.Get(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"Employee With Id {id} Is Not Found" });

            return View(ViewName, employee);

        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var departments = _unitOfWork.departmentRepository.GetAll();
            ViewData["departments"] = departments;
            if (id is null) return BadRequest("Invalid Id");

            var employee = _unitOfWork.employeeRepository.Get(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id {id} Is Not Found" });

            //var employeeDto = new CreateEmployeeDto()
            //{
            //    Name = employee.Name,
            //    Age = employee.Age,
            //    Address = employee.Address,
            //    Email = employee.Email,
            //    Phone = employee.Phone,
            //    Salary = employee.Salary,
            //    IsActive = employee.IsActive,
            //    IsDeleted = employee.IsDeleted,
            //    HiringDate = employee.HiringDate,
            //    CreateAt = employee.CreateAt,
            //    DepartmentId = employee.DepartmentId
            //};
            var employeeDto = _mapper.Map<CreateEmployeeDto>(employee);
            return View(employeeDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateEmployeeDto employeeDto)
        {
           
            if (ModelState.IsValid)
            {
                if(employeeDto.ImageName is not null && employeeDto.Image is not null)
                {
                    DocumentSetting.DeleteFile("images", employeeDto.ImageName);
                }
                if (employeeDto.Image is not null)
                {
                    employeeDto.ImageName = DocumentSetting.UploadFile(employeeDto.Image, "images");
                }
                var employee = _mapper.Map<Employee>(employeeDto);
                employee.Id = id;
                _unitOfWork.employeeRepository.Update(employee);
                var Count = _unitOfWork.Complete();
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(employeeDto);
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
        public IActionResult Delete([FromRoute] int id, CreateEmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                
                var employee = _mapper.Map<Employee>(employeeDto);
                employee.Id = id;
                _unitOfWork.employeeRepository.Delete(employee);
                var Count = _unitOfWork.Complete();
                if (Count > 0)
                {
                    if (employeeDto.ImageName is not null)
                    {
                        DocumentSetting.DeleteFile("images", employeeDto.ImageName);
                    }
                    return RedirectToAction("Index");
                }
            }
            return View(employeeDto);
        }
    }
}
