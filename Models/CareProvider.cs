using System;
using System.Collections.Generic;

namespace CareProviderPortal.Models;

public partial class CareProvider
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Specialization { get; set; } = null!;

    public int DepartmentId { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Experience> Experiences { get; set; } = new List<Experience>();
}
