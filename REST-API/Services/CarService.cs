using REST_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace REST_API.Services
{
    public class CarService
    {
        private readonly IMongoCollection<Car> _carCollection;

        public CarService(
            IOptions<CarflixDatabaseSettings> carflixDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                carflixDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                carflixDatabaseSettings.Value.DatabaseName);

            _carCollection = mongoDatabase.GetCollection<Car>(
                carflixDatabaseSettings.Value.CarflixCollectionName2);
        }

        public async Task<List<Car>> GetAsync() =>
            await _carCollection.Find(_ => true).ToListAsync();

        public async Task<Car?> GetAsync(string id) =>
            await _carCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Car newCar) =>
            await _carCollection.InsertOneAsync(newCar);

        public async Task UpdateAsync(string id, Car updatedCar) =>
            await _carCollection.ReplaceOneAsync(x => x.Id == id, updatedCar);

        public async Task RemoveAsync(string id) =>
            await _carCollection.DeleteOneAsync(x => x.Id == id);
    }
}
