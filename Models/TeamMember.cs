using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CCTV.S2.Models
{
    public class TeamMember:Base.BaseEntity
    {
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Image { get; set; }
    }
}
