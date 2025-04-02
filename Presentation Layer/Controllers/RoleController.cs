using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation_Layer.Dtos;
using Presentation_Layer.Helpers;

namespace Presentation_Layer.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Index(string? Search)
        {
            IEnumerable<RoleToReturnDto> roles;
            if (Search is not null)
            {
                roles = _roleManager.Roles.Select(R => new RoleToReturnDto()
                {
                    Id = R.Id,
                    Name=R.Name

                }).Where(R => R.Name.ToLower().Contains(Search.ToLower()));

            }
            else
            {
                roles = _roleManager.Roles.Select(R => new RoleToReturnDto()
                {
                    Id = R.Id,
                    Name = R.Name

                });

            }
            return View(roles);
        }

        [HttpGet]

        public async Task<IActionResult> Create()
        {           
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(RoleToReturnDto roleDto)
        {
            if (ModelState.IsValid)
            {
               var role = await _roleManager.FindByNameAsync(roleDto.Name);
                if (role is null)
                {
                    var newRole = new IdentityRole()
                    {
                        Name = roleDto.Name
                    };
                    var result = await _roleManager.CreateAsync(newRole);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError("Name", "Role Name Already Exists");
            }
            return View(roleDto);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");

            var roles = await _roleManager.FindByIdAsync(id);

            if (roles is null) return NotFound(new { StatusCode = 404, Message = $"Role With Id {id} Is Not Found" });

            var dto = new RoleToReturnDto()
            {
                Id = roles.Id,
                Name = roles.Name

            };
            return View(ViewName, dto);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {

            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleToReturnDto roleDto)
        {
            if (ModelState.IsValid)
            {
                if (id != roleDto.Id) return BadRequest("Invalid Operation");
                var role = await _roleManager.FindByIdAsync(id);

                if (role is null) return BadRequest("Invalid Operation");
                var roleResult=await _roleManager.FindByNameAsync(roleDto.Name);

                if (roleResult is null)
                {
                    role.Name = roleDto.Name;

                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }

                ModelState.AddModelError("Name", "Role Name Already Exists");


            }
            return View(roleDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {

            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, RoleToReturnDto roleDto)
        {
            if (ModelState.IsValid)
            {

                if (id != roleDto.Id) return BadRequest("Invalid Operation");
                var role = await _roleManager.FindByIdAsync(id);

                if (role is null) return BadRequest("Invalid Operation");
                var roleResult = await _roleManager.FindByNameAsync(roleDto.Name);

                
                role.Name = roleDto.Name;

                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                     return RedirectToAction("Index");
                }

                ModelState.AddModelError("Name", "Invalid Operation");


            }
            return View(roleDto);
        }
    }
}
