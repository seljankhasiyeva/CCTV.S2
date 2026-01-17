using Microsoft.EntityFrameworkCore;

namespace CCTV.S2.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Models.TeamMember> TeamMembers { get; set; }
    }
}
