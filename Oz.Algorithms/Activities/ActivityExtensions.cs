using System.Collections.Generic;
using static Oz.Algorithms.Activities.ActivitySelectorFactory;

namespace Oz.Algorithms.Activities
{
    public static class ActivityExtensions
    {
        public static HashSet<Activity<T>> SelectActivities<T>(
            this List<Activity<T>> activities,
            AlgorithmKind algorithmKind = AlgorithmKind.Recursive)
        {
            var selector = Create<T>(algorithmKind);
            return selector.SelectActivities(activities);
        }
    }
}