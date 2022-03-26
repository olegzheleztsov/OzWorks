using System.Collections.Generic;

namespace Oz.Algorithms.Activities
{
    public class IterativeActivitySelector<T> : BaseActivitySelector<T>
    {
        protected override List<Activity<T>> PrepareActivities(List<Activity<T>> activities) =>
            activities;

        protected override HashSet<Activity<T>> _SelectActivities(IReadOnlyList<Activity<T>> activities, int startIndex, int numberOfActivities)
        {
            var result = new HashSet<Activity<T>>(new[] {activities[0]});
            var k = 0;
            for (var m = 1; m < activities.Count; m++)
            {
                if (activities[m].StartTime < activities[k].EndTime)
                {
                    continue;
                }

                result.UnionWith(new HashSet<Activity<T>>(new[] {activities[m]}));
                k = m;
            }

            return result;
        }
    }
}