using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using SimplePages.Models.GymStats;
using SimplePages.Services.Interfaces;

namespace SimplePages.Services
{
    public class ExerciseNames : IExerciseNames
    {
        private readonly ConcurrentDictionary<BodyPart, List<ExerciseName>> _exerciseNames
            = new ConcurrentDictionary<BodyPart, List<ExerciseName>>
            {
                [BodyPart.Back] = new List<ExerciseName>
                {
                    new ExerciseName(1, "Pull-ups on the bar", BodyPart.Back),
                    new ExerciseName(2, "Press in the simulator vertical", BodyPart.Back),
                    new ExerciseName(3, "Press in the simulator horizontal", BodyPart.Back)
                },
                [BodyPart.Biceps] = new List<ExerciseName>
                {
                    new ExerciseName(4, "Dumbbell curl", BodyPart.Biceps),
                    new ExerciseName(5, "Barbell curl", BodyPart.Biceps)
                },
                [BodyPart.Chest] = new List<ExerciseName>
                {
                    new ExerciseName(6, "Press the bar horizontal", BodyPart.Chest),
                    new ExerciseName(7, "Dumbbell breeding horizontal", BodyPart.Chest),
                    new ExerciseName(8, "Breeding hands in the simulator", BodyPart.Chest)
                },
                [BodyPart.Legs] = new List<ExerciseName>
                {
                    new ExerciseName(9, "Squats", BodyPart.Legs),
                    new ExerciseName(10, "Bench press", BodyPart.Legs),
                    new ExerciseName(11, "Rear leg muscles in the simulator", BodyPart.Legs),
                    new ExerciseName(12, "Seated Leg Curl", BodyPart.Legs)
                },
                [BodyPart.Press] = new List<ExerciseName>
                {
                    new ExerciseName(13, "Lying press", BodyPart.Press)
                },
                [BodyPart.Shoulders] = new List<ExerciseName>
                {
                    new ExerciseName(14, "Dumbbell breeding", BodyPart.Shoulders),
                    new ExerciseName(15, "Breeding hands in the simulator", BodyPart.Shoulders)
                },
                [BodyPart.Triceps] = new List<ExerciseName>
                {
                    new ExerciseName(16, "Flexion of the arms on the simulator", BodyPart.Triceps),
                    new ExerciseName(17, "Dumbbell Bent Over Bend", BodyPart.Triceps)
                },
                [BodyPart.General] = new List<ExerciseName>
                {
                    new ExerciseName(18, "Exercise bike", BodyPart.General),
                    new ExerciseName(19, "Pull-ups on the bar", BodyPart.General)
                }
            };

        public List<ExerciseName> GetExerciseNames(BodyPart bodyPart)
        {
            if (_exerciseNames.TryGetValue(bodyPart, out var names))
            {
                return names;
            }

            throw new ArgumentException($"Unknown body part: {bodyPart}");
        }

        public BodyPart GetBodyPartForExerciseName(int exerciseNameId)
        {
            foreach (var kvp in _exerciseNames)
            {
                var targetExerciseName = kvp.Value.FirstOrDefault(exerciseName => exerciseName.Id == exerciseNameId);
                if (targetExerciseName != null)
                {
                    return kvp.Key;
                }
            }

            throw new ArgumentException($"Body part for exercise name id: {exerciseNameId} not found");
        }

        public ExerciseName GetExerciseName(int exerciseNameId)
        {
            foreach (var kvp in _exerciseNames)
            {
                var targetExerciseName = kvp.Value.FirstOrDefault(exerciseName => exerciseName.Id == exerciseNameId);
                if (targetExerciseName != null)
                {
                    return targetExerciseName;
                }
            }

            throw new ArgumentException($"Exercise name for id: {exerciseNameId} not found");
        }

        public ExerciseName GetExerciseName(BodyPart bodyPart, string name)
        {
            return _exerciseNames.TryGetValue(bodyPart, out var names) ? names.FirstOrDefault(eName => eName.Name == name) : null;
        }
    }
}