using AutoMapper;
using CareProviderPortal.dto;
using CareProviderPortal.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // For output (GET): map entity to DTO including Id.
        CreateMap<CareProvider, CareProviderDTO>();

        // For input (POST/PUT): map CreateDTO to entity; ignore Id.
        CreateMap<CareProviderCreateDTO, CareProvider>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        // Other mappings for Department and Achievement remain as needed...
        CreateMap<Department, DepartmentDTO>();
        CreateMap<DepartmentDTO, Department>().ForMember(dest => dest.Id, opt => opt.Ignore());

        // For GET responses (include Id)
        CreateMap<Experience, ExperienceDTO>();

        // For POST/PUT requests (ignore Id so it isn't overwritten)
        CreateMap<ExperienceCreateDTO, Experience>().ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Achievement, AchievementDTO>();

        CreateMap<AchievementCreateDTO, Achievement>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

    }
}

