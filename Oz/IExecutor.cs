using System.Threading.Tasks;

namespace Oz
{
    public interface IExecutor
    {
        Task RunAsync(object arg);
    }
}