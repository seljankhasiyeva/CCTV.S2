using CCTV.S2.DAL;
using Microsoft.AspNetCore.Mvc;
using CCTV.S2.Models;
using Microsoft.EntityFrameworkCore;

namespace CCTV.S2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMemberController : Controller
    {
        private readonly AppDbContext _context;
        public TeamMemberController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<TeamMember> teamMembers = await _context.TeamMembers.ToListAsync();
            return View(teamMembers);
        }
    }
}
