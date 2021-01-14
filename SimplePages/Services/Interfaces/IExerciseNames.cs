using System.Collections.Generic;
using SimplePages.Models.GymStats;

namespace SimplePages.Services.Interfaces
{
    public interface IExerciseNames
    {
        List<ExerciseName> GetExerciseNames(BodyPart bodyPart);
        BodyPart GetBodyPartForExerciseName(int exerciseNameId);

        ExerciseName GetExerciseName(int exerciseNameId);
    }
}