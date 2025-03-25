using System;
using System.Collections.Generic;

namespace CareProviderPortal.Models;

public partial class Experience
{
    public int Id { get; set; }

    public int? ProviderId { get; set; }

    public string Organization { get; set; } = null!;

    public string Position { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Description { get; set; }

    public virtual CareProvider? Provider { get; set; }
}
