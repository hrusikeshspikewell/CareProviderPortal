using AutoMapper;
using CareProviderPortal.dto;
using CareProviderPortal.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CareProvider, CareProviderDTO>()
            .ForMember(dest => dest.TotalExperienceYears,
                opt => opt.MapFrom(src =>
                    Math.Floor(src.Experiences.Sum(exp =>
                        ((exp.EndDate.HasValue ? exp.EndDate.Value : DateTime.Now) - exp.StartDate)
                        .TotalDays) / 365.25)));

        CreateMap<CareProviderCreateDTO, CareProvider>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<DepartmentCreateDTO, Department>();

        CreateMap<Department, DepartmentDTO>();

        CreateMap<Experience, ExperienceDTO>();

        CreateMap<ExperienceCreateDTO, Experience>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Achievement, AchievementDTO>();

        CreateMap<AchievementCreateDTO, Achievement>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
