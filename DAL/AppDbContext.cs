using Microsoft.EntityFrameworkCore;
using CCTV.S2.Models;
namespace CCTV.S2.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<TeamMember> TeamMembers { get; set; }    
    }
}
