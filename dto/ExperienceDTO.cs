namespace CareProviderPortal.dto
{
    public class ExperienceDTO
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string Organization { get; set; } = null!;
        public string Position { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
    }
}
