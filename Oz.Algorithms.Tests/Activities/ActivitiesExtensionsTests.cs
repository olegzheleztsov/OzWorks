using System.Collections.Generic;
using System.Linq;
using Oz.Algorithms.Activities;
using Xunit;

namespace Oz.Algorithms.Tests.Activities
{
    public class ActivitiesExtensionsTests
    {
        [Fact]
        public void Should_Recursive_Selection_Work_Correctly()
        {
            var a1 = new Activity<int>(1, 4, 1);
            var a2 = new Activity<int>(3, 5, 2);
            var a3 = new Activity<int>(0, 6, 3);
            var a4 = new Activity<int>(5, 7, 4);
            var a5 = new Activity<int>(3, 9, 5);
            var a6 = new Activity<int>(5, 9, 6);
            var a7 = new Activity<int>(6, 10, 7);
            var a8 = new Activity<int>(8, 11, 8);
            var a9 = new Activity<int>(8, 12, 9);
            var a10 = new Activity<int>(2, 14, 10);
            var a11 = new Activity<int>(12, 16, 11);
            var activities = new List<Activity<int>>
            {
                a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11
            };

            var result = activities.SelectActivities().ToList().OrderBy(a => a.EndTime).ToList();
            var expected = new List<Activity<int>> {a1, a4, a8, a11};
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Should_Iterative_Selection_Work_Correctly()
        {
            var a1 = new Activity<int>(1, 4, 1);
            var a2 = new Activity<int>(3, 5, 2);
            var a3 = new Activity<int>(0, 6, 3);
            var a4 = new Activity<int>(5, 7, 4);
            var a5 = new Activity<int>(3, 9, 5);
            var a6 = new Activity<int>(5, 9, 6);
            var a7 = new Activity<int>(6, 10, 7);
            var a8 = new Activity<int>(8, 11, 8);
            var a9 = new Activity<int>(8, 12, 9);
            var a10 = new Activity<int>(2, 14, 10);
            var a11 = new Activity<int>(12, 16, 11);
            var activities = new List<Activity<int>>
            {
                a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11
            };

            var result = activities.SelectActivities(AlgorithmKind.Iterative).ToList().OrderBy(a => a.EndTime).ToList();
            var expected = new List<Activity<int>> {a1, a4, a8, a11};
            Assert.Equal(expected, result);
        }
    }
}