using System.Collections.Generic;
using System.Linq;
using Oz.Algorithms.Sort;

namespace Oz.Algorithms.Activities
{
    public class InverseActivitySelector<T> : IActivitySelector<T>
    {
        public HashSet<Activity<T>> SelectActivities(List<Activity<T>> activities)
        {
            if (activities.Count == 0)
            {
                return new HashSet<Activity<T>>();
            }

            var activityArray = new Activity<T>[activities.Count];
            for (var i = 0; i < activities.Count; i++)
            {
                activityArray[i] = activities[i];
            }

            var sorter = new QuickSorter<Activity<T>>(PartitionStrategy.RandomizedPartition);
            sorter.Sort(activityArray, activity => activity.EndTime, Comparisions.StandardComparision);
            var sortedActivities = new List<Activity<T>>(activityArray);

            var result = new HashSet<Activity<T>>(new List<Activity<T>> {sortedActivities.Last()});
            result.UnionWith(SelectActivitiesInner(sortedActivities, sortedActivities.Count - 1));
            return result;
        }

        private IEnumerable<Activity<T>> SelectActivitiesInner(IReadOnlyList<Activity<T>> activities, int startIndex)
        {
            var m = startIndex - 1;
            while (m >= 0 && activities[m].EndTime > activities[startIndex].StartTime)
            {
                m--;
            }

            if (m < 0)
            {
                return new HashSet<Activity<T>>();
            }

            var kMax = m;
            for (var k = m; k >= 0; k--)
            {
                if (activities[k].StartTime > activities[kMax].StartTime &&
                    activities[k].EndTime <= activities[startIndex].StartTime)
                {
                    kMax = k;
                }
            }

            {
                var result = new HashSet<Activity<T>>(new List<Activity<T>> {activities[kMax]});
                result.UnionWith(SelectActivitiesInner(activities, kMax));
                return result;
            }
        }
    }
}