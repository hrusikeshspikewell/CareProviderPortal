namespace CareProviderPortal.dto
{
    public class CareProviderCreateDTO
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public int DepartmentId { get; set; }
        public string Status { get; set; } = "ACTIVE";

    }
}
