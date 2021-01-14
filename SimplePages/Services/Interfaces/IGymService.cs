using System.Collections.Generic;
using System.Threading.Tasks;
using SimplePages.Models.GymStats;

namespace SimplePages.Services.Interfaces
{
    public interface IGymService
    {
        Task<List<Training>> GetAsync();
        Task<Training> GetAsync(string id);
        Task<Training> InsertAsync(Training training);
        Task<bool> UpdateAsync(string id, Training trainingIn);
        Task<bool> RemoveAsync(string id);
    }
}