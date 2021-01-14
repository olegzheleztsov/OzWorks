using System.Collections.Generic;
using Oz.Algorithms.Sort;

namespace Oz.Algorithms.Activities
{
    public abstract class BaseActivitySelector<T> : IActivitySelector<T>
    {
        public HashSet<Activity<T>> SelectActivities(List<Activity<T>> activities)
        {
            var activityArray = new Activity<T>[activities.Count];
            for (var i = 0; i < activities.Count; i++)
            {
                activityArray[i] = activities[i];
            }

            var sorter = new QuickSorter<Activity<T>>(PartitionStrategy.RandomizedPartition);
            sorter.Sort(activityArray, activity => activity.EndTime, Comparisions.StandardComparision);
            var sortedActivities = new List<Activity<T>>(activityArray);
            sortedActivities = PrepareActivities(sortedActivities);
            return _SelectActivities(sortedActivities, 0, activities.Count);
        }

        protected abstract List<Activity<T>> PrepareActivities(List<Activity<T>> activities);

        protected abstract HashSet<Activity<T>> _SelectActivities(IReadOnlyList<Activity<T>> activities, int startIndex,
            int numberOfActivities);
        
    }
}