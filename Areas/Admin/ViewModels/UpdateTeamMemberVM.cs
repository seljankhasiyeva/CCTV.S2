namespace CCTV.S2.Areas.Admin.ViewModels
{
    public class UpdateTeamMemberVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string? Image { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
