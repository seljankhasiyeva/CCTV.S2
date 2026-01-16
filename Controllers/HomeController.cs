using System.Diagnostics;
using CCTV.S2.DAL;
using CCTV.S2.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CCTV.S2.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            TeamMemberVM teamMemberVM = new TeamMemberVM()
            {
                TeamMembers = _context.TeamMembers.ToList(),
            };
            return View(teamMemberVM);
        }

    }
}
