using CCTV.S2.Areas.Admin.ViewModels;
using CCTV.S2.DAL;
using CCTV.S2.Models;
using CCTV.S2.Utilities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CCTV.S2.Utilities.Extensions;

namespace CCTV.S2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMemberController : Controller
    {
        private readonly AppDbContext _context;
        public readonly IWebHostEnvironment _env;
        public TeamMemberController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<TeamMember> teamMembers = await _context.TeamMembers.ToListAsync();
            return View(teamMembers);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamMemberVM createTeamMemberVM)
        {
            if (!ModelState.IsValid)
            {
                return View(createTeamMemberVM);
            }
            if (!createTeamMemberVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "File type must be image");
                return View(createTeamMemberVM);
            }
            if (!createTeamMemberVM.Photo.ValidateSize(FileSize.MB, 20))
            {
                ModelState.AddModelError("Photo", "File size is incorrect");
                return View(createTeamMemberVM);
            }
            TeamMember teamMember = new TeamMember()
            {
                Name = createTeamMemberVM.Name,
                Designation = createTeamMemberVM.Designation,
                Image = await createTeamMemberVM.Photo.CreateFile(_env.WebRootPath, "img")
            };
            await _context.TeamMembers.AddAsync(teamMember);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            TeamMember teamMember = await _context.TeamMembers.FirstOrDefaultAsync(t => t.Id == id);
            if (teamMember == null)
            {
                return NotFound();
            }
            UpdateTeamMemberVM updateTeamMemberVM = new UpdateTeamMemberVM()
            {
                Id = teamMember.Id,
                Name = teamMember.Name,
                Designation = teamMember.Designation,
                Image = teamMember.Image
            };
            return View(updateTeamMemberVM);
        }

        [HttpPost]

        public async Task<IActionResult> Update(int? id, UpdateTeamMemberVM updateTeamMemberVM)
        {
            if (!ModelState.IsValid)
            {
                return View(updateTeamMemberVM);
            }
            TeamMember teamMember = await _context.TeamMembers.FirstOrDefaultAsync(t => t.Id == id);
            if (updateTeamMemberVM.Photo != null)
            {
                if (!updateTeamMemberVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(updateTeamMemberVM.Photo), "File type must be image");
                    return View(updateTeamMemberVM);
                }
                if (!updateTeamMemberVM.Photo.ValidateSize(FileSize.MB, 20))
                {
                    ModelState.AddModelError(nameof(updateTeamMemberVM.Photo), "File size is incorrect");
                    return View(updateTeamMemberVM);
                }
                string fileName = await updateTeamMemberVM.Photo.CreateFile(_env.WebRootPath, "img");
                teamMember.Image.DeleteFile(_env.WebRootPath, "img");
                teamMember.Image = fileName;
            }
            teamMember.Name = updateTeamMemberVM.Name;
            teamMember.Designation = updateTeamMemberVM.Designation;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            TeamMember teamMember = await _context.TeamMembers.FirstOrDefaultAsync(t => t.Id == id);
            if (teamMember == null)
            {
                return NotFound();
            }
            teamMember.Image.DeleteFile(_env.WebRootPath, "img");
            _context.TeamMembers.Remove(teamMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            TeamMember teamMember = await _context.TeamMembers.FirstOrDefaultAsync(t => t.Id == id);
            if (teamMember == null)
            {
                return NotFound();
            }

            DetailTeamMemberVM detailTeamMemberVM = new DetailTeamMemberVM()
            {
                Name = teamMember.Name,
                Designation = teamMember.Designation,
                Image = teamMember.Image
            };
            return View(detailTeamMemberVM);
        }
    }
}
