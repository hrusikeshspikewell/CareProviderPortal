﻿namespace CareProviderPortal.dto
{
    public class AchievementDTO
    {
        public int? Id { get; set; }

        public int ProviderId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateOnly AchievedDate { get; set; }
    }
}
