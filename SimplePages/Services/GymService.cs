using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SimplePages.Config;
using SimplePages.Models.GymStats;
using SimplePages.Services.Interfaces;

namespace SimplePages.Services
{
    public class GymService : IGymService
    {
        private readonly IMongoCollection<Training> _trainings;

        public GymService(IGymSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.GymDatabase);
            _trainings = database.GetCollection<Training>(settings.TrainingCollectionName);
        }

        public async Task<List<Training>> GetAsync()
        {
            var cursor = await _trainings.FindAsync(training => true).ConfigureAwait(false);
            return await cursor.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Training> GetAsync(string id)
        {
            var cursor = await _trainings.FindAsync(training => training.Id == id).ConfigureAwait(false);
            var list = await cursor.ToListAsync().ConfigureAwait(false);
            return list.FirstOrDefault();
        }

        public async Task<Training> InsertAsync(Training training)
        {
            await _trainings.InsertOneAsync(training).ConfigureAwait(false);
            return training;
        }

        public async Task<bool> UpdateAsync(string id, Training trainingIn)
        {
            var result = await _trainings.ReplaceOneAsync(training => training.Id == id, trainingIn).ConfigureAwait(false);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var result = await _trainings.DeleteOneAsync(training => training.Id == id).ConfigureAwait(false);
            return result.DeletedCount > 0;
        }
    }
}