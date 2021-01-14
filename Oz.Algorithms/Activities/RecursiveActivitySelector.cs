using System.Collections.Generic;

namespace Oz.Algorithms.Activities
{
    public class RecursiveActivitySelector<T> : BaseActivitySelector<T>
    {
        protected override List<Activity<T>> PrepareActivities(List<Activity<T>> activities)
        {
            activities.Insert(0, Activity<T>.EmptyActivity);
            return activities;
        }

        protected override HashSet<Activity<T>> _SelectActivities(IReadOnlyList<Activity<T>> activities, int startIndex, int numberOfActivities)
        {
            var m = startIndex + 1;
            while (m <= numberOfActivities && activities[m].StartTime < activities[startIndex].EndTime)
            {
                m++;
            }

            if (m <= numberOfActivities)
            {
                var result = new HashSet<Activity<T>>(new[] {activities[m]});
                result.UnionWith(_SelectActivities(activities, m, numberOfActivities));
                return result;
            }

            return new HashSet<Activity<T>>();
        }
    }
}