namespace Oz.Algorithms.Activities
{
    public class Activity<T>
    {
        public int StartTime { get; }
        public int EndTime { get; }
        public T Data { get; }

        public Activity(int startTime, int endTime, T data)
        {
            StartTime = startTime;
            EndTime = endTime;
            Data = data;
        }

        public static Activity<T> EmptyActivity => new Activity<T>(0, 0, default);
    }
}