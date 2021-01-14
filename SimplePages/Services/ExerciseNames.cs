using System.Collections.Concurrent;
using System.Collections.Generic;

namespace SimplePages.Models.GymStats
{
    public static class ExerciseNames
    {
        private static ConcurrentDictionary<BodyPart, List<string>> _exerciseNames
        = new ConcurrentDictionary<BodyPart, List<string>>()
        {
            [BodyPart.Back] = new List<string>()
            {
                "Pull-ups on the bar",
                "Press in the simulator vertical",
                "Press in the simulator horizontal"
            },
            [BodyPart.Biceps] = new List<string>()
            {
                "Dumbbell curl",
                "Barbell curl"
            },
            [BodyPart.Chest] = new List<string>()
            {
                "Press the bar horizontal",
                "Dumbbell breeding horizontal",
                "Breeding hands in the simulator"
            },
            [BodyPart.Legs] = new List<string>()
            {
                "Squats",
                "Bench press",
                "Rear leg muscles in the simulator",
                "Seated Leg Curl"
            },
            [BodyPart.Press] = new List<string>()
            {
                "Lying press"
            },
            [BodyPart.Shoulders] = new List<string>()
            {
                "Dumbbell breeding",
                "Breeding hands in the simulator"
            },
            [BodyPart.Triceps] = new List<string>()
            {
                "Flexion of the arms on the simulator",
                "Dumbbell Bent Over Bend"
            },
            [BodyPart.General] = new List<string>()
            {
                ""
            }
        }
    }
}