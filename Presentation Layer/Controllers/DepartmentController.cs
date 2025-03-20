using AutoMapper;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Repositories;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation_Layer.Dtos;

namespace Presentation_Layer.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitRepository , IMapper mapper)
        {
            _unitOfWork = unitRepository;
            _mapper = mapper;
        }


        public IActionResult Index(string? Search)
        {
            IEnumerable<Department> departments;
            if (Search is not null)
            {
                departments = _unitOfWork.departmentRepository.GetByName(Search);
                return View(departments);
            }
            else
            {
                departments = _unitOfWork.departmentRepository.GetAll();
                return View(departments);
            }
            //var departments = _departmentRepository.GetAll();
            //return View(departments);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(CreateDepartmentDto departmentDto)
        {
            if(ModelState.IsValid)
            {
                //var department = new Department()
                //{
                //    Code = departmentDto.Code,
                //    Name = departmentDto.Name,
                //    CreateAt = departmentDto.CreateAt
                //};
                var department = _mapper.Map<Department>(departmentDto);
                _unitOfWork.departmentRepository.Add(department);
                var Count = _unitOfWork.Complete();
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(departmentDto);
        }
        [HttpGet]
        public IActionResult Details(int? id, string ViewName="Details")
        {
            if (id is null) return BadRequest("Invalid Id");

            var department = _unitOfWork.departmentRepository.Get(id.Value);

            if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id {id} Is Not Found" });

            return View(ViewName,department);

        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest("Invalid Id");

            var department = _unitOfWork.departmentRepository.Get(id.Value);

            if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id {id} Is Not Found" });

            //var createDepartmentDto = new CreateDepartmentDto()
            //{
            //    Code = department.Code,
            //    Name = department.Name,
            //    CreateAt = department.CreateAt
            //};
            var createDepartmentDto = _mapper.Map<CreateDepartmentDto>(department);
            return View(createDepartmentDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.departmentRepository.Update(department);
                var Count = _unitOfWork.Complete();
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(department);
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
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.departmentRepository.Delete(department);
                var Count = _unitOfWork.Complete();
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(department);
        }
    }
}
