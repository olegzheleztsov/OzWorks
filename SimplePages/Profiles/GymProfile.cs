using AutoMapper;
using SimplePages.Models.GymStats;
using SimplePages.Services.Interfaces;
using SimplePages.ViewModels;

namespace SimplePages.Profiles
{
    public class GymProfile : Profile
    {
        private readonly IExerciseNames _exerciseNames;

        public override string ProfileName => nameof(GymProfile);

        public GymProfile(IExerciseNames exerciseNames)
        {
            _exerciseNames = exerciseNames;
            
            CreateMap<PhysicalExerciseViewModel, PhysicalExercise>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => MapExerciseName(src)))
                .ForMember(dest => dest.BodyPart, opt => opt.MapFrom(src => MapBodyPart(src)));

            CreateMap<PhysicalExercise, PhysicalExerciseViewModel>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.ExerciseId, opt => opt.MapFrom(src => MapExerciseId(src)));

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
        

        private string MapExerciseName(PhysicalExerciseViewModel viewModel)
        {
            var exerciseName = _exerciseNames.GetExerciseName(viewModel.ExerciseId);
            return exerciseName.Name;
        }

        private BodyPart MapBodyPart(PhysicalExerciseViewModel viewModel)
        {
            var exerciseName = _exerciseNames.GetExerciseName(viewModel.ExerciseId);
            return exerciseName.BodyPart;
        }

        private int MapExerciseId(PhysicalExercise exercise)
        {
            var exerciseName = _exerciseNames.GetExerciseName(exercise.BodyPart, exercise.Name);
            return exerciseName.Id;
        }
    }
}