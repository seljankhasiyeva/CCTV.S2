using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCTV.S2.Models
{
    public class TeamMember:Base.BaseEntity
    {
        [MaxLength(20,ErrorMessage ="Name cannot contain more than 20 symbols")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "Name cannot contain more than 50 symbols")]
        public string Designation { get; set; }
        public string Image { get; set; }
    }
}
