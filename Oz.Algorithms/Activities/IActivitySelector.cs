using System.Collections.Generic;

namespace Oz.Algorithms.Activities
{
    public interface IActivitySelector<T>
    {
        HashSet<Activity<T>> SelectActivities(List<Activity<T>> activities);
    }
}