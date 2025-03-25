namespace CareProviderPortal.dto
{
    public class ExperienceCreateDTO
    {
        public int ProviderId { get; set; }
        public string Organization { get; set; } = null!;
        public string Position { get; set; } = null!;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; }
    }
}