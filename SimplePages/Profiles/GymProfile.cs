using AutoMapper;
using SimplePages.Models.GymStats;
using SimplePages.ViewModels;

namespace SimplePages.Profiles
{
    public class GymProfile : Profile
    {
        public GymProfile()
        {
            CreateMap<PhysicalExerciseViewModel, PhysicalExercise>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.BodyPart, opt => opt.MapFrom(src => src.BodyPart))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            
            CreateMap<PhysicalExercise, PhysicalExerciseViewModel>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.BodyPart, opt => opt.MapFrom(src => src.BodyPart))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<TrainingViewModel, Training>()
                .ForMember(dest => dest.Date,
                    opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Exercises,
                    opt => opt.MapFrom(src => src.Exercises));
            
            CreateMap<Training, TrainingViewModel>()
                .ForMember(dest => dest.Date,
                    opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Exercises,
                    opt => opt.MapFrom(src => src.Exercises));

        }
    }
}