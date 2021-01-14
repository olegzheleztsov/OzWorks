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
            = new ConcurrentDictionary<BodyPart, List<ExerciseName>>()
            {
                [BodyPart.Back] = new List<ExerciseName>()
                {
                    new ExerciseName(1, "Pull-ups on the bar"),
                    new ExerciseName(2, "Press in the simulator vertical"),
                    new ExerciseName(3, "Press in the simulator horizontal")
                },
                [BodyPart.Biceps] = new List<ExerciseName>()
                {
                    new ExerciseName(4, "Dumbbell curl"),
                    new ExerciseName(5, "Barbell curl")
                },
                [BodyPart.Chest] = new List<ExerciseName>()
                {
                    new ExerciseName(6, "Press the bar horizontal"),
                    new ExerciseName(7, "Dumbbell breeding horizontal"),
                    new ExerciseName(8, "Breeding hands in the simulator")
                },
                [BodyPart.Legs] = new List<ExerciseName>()
                {
                    new ExerciseName(9, "Squats"),
                    new ExerciseName(10, "Bench press"),
                    new ExerciseName(11, "Rear leg muscles in the simulator"),
                    new ExerciseName(12, "Seated Leg Curl")
                },
                [BodyPart.Press] = new List<ExerciseName>()
                {
                    new ExerciseName(13, "Lying press")
                },
                [BodyPart.Shoulders] = new List<ExerciseName>()
                {
                    new ExerciseName(14, "Dumbbell breeding"),
                    new ExerciseName(15, "Breeding hands in the simulator")
                },
                [BodyPart.Triceps] = new List<ExerciseName>()
                {
                    new ExerciseName(16, "Flexion of the arms on the simulator"),
                    new ExerciseName(17, "Dumbbell Bent Over Bend")
                },
                [BodyPart.General] = new List<ExerciseName>()
                {
                    new ExerciseName(18, "Exercise bike"),
                    new ExerciseName(19, "Pull-ups on the bar")
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

            throw new ArgumentException($"Excercise name for id: {exerciseNameId} not found");
        }
    }
}