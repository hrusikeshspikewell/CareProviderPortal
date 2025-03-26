using AutoMapper;
using CareProviderPortal.dto;
using CareProviderPortal.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Map CareProvider to CareProviderDTO including custom mapping for TotalExperienceYears.
        CreateMap<CareProvider, CareProviderDTO>()
            .ForMember(dest => dest.TotalExperienceYears,
                opt => opt.MapFrom(src =>
                    Math.Floor(src.Experiences.Sum(exp =>
                        ((exp.EndDate.HasValue ? exp.EndDate.Value : DateTime.Now) - exp.StartDate)
                        .TotalDays) / 365.25)));

        // For input (POST/PUT): map CareProviderCreateDTO to CareProvider; ignore Id.
        CreateMap<CareProviderCreateDTO, CareProvider>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        // Map DepartmentCreateDTO to Department.
        CreateMap<DepartmentCreateDTO, Department>();

        // Map Department to DepartmentDTO (if needed).
        CreateMap<Department, DepartmentDTO>();

        // For GET responses: map Experience to ExperienceDTO.
        CreateMap<Experience, ExperienceDTO>();

        // For POST/PUT requests: map ExperienceCreateDTO to Experience; ignore Id.
        CreateMap<ExperienceCreateDTO, Experience>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        // Map Achievement to AchievementDTO.
        CreateMap<Achievement, AchievementDTO>();

        // Map AchievementCreateDTO to Achievement; ignore Id.
        CreateMap<AchievementCreateDTO, Achievement>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
