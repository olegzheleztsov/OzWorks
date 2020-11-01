using System;
using System.Threading.Tasks;

namespace Oz.Algorithms.Tasks
{
    public static class TaskUtils
    {
        public static Task[] RunTasks(Action[] actions)
        {
            var tasks = new Task[actions.Length];
            for (var i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(actions[i]);
            }

            return tasks;
        }
    }
}