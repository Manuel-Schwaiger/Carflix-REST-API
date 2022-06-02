using REST_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace REST_API.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserService(
            IOptions<CarflixDatabaseSettings> carflixDatabaseSettings)
        {
            // MongoDB Connection String wurde an Client gegeben
            var mongoClient = new MongoClient(
                carflixDatabaseSettings.Value.ConnectionString);

            // Name der Datenbank wird übergeben
            var mongoDatabase = mongoClient.GetDatabase(
                carflixDatabaseSettings.Value.DatabaseName);

            // Name der Collection wird übergeben
            _userCollection = mongoDatabase.GetCollection<User>(
                carflixDatabaseSettings.Value.CarflixCollectionName);
        }

        public async Task<List<User>> GetAsync() =>
            await _userCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(string id) =>
            await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(User newUser) =>
            await _userCollection.InsertOneAsync(newUser);

        public async Task UpdateAsync(string id, User updatedUser) =>
            await _userCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(string id) =>
            await _userCollection.DeleteOneAsync(x => x.Id == id);
    }
}