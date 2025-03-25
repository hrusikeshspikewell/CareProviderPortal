namespace CareProviderPortal.dto
{
    public class CareProviderDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public int DepartmentId { get; set; }
        public string Status { get; set; } = null!;
        public List<ExperienceDTO> Experiences { get; set; } = new List<ExperienceDTO>();
        public List<AchievementDTO> Achievements { get; set; } = new List<AchievementDTO>();
    }
}
