using System;
using System.Collections.Generic;

namespace CareProviderPortal.Models;

public partial class Achievement
{
    public int Id { get; set; }

    public int ProviderId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly AchievedDate { get; set; }

    public virtual CareProvider Provider { get; set; } = null!;
}
