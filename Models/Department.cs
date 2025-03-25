using System;
using System.Collections.Generic;

namespace CareProviderPortal.Models;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<CareProvider> CareProviders { get; set; } = new List<CareProvider>();
}
