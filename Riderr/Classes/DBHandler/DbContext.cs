using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Riderr.Classes.DBHandler
{
    public class DbContext
    {
        //todo - context
        //          users|  pairs|  messages
        //create    +       +       +
        //read      +       +       +
        //update    +       +       -
        //delete    +       +       -


        private readonly IGridFSBucket _gridFS;
        private static readonly string _connectionString = "mongodb://localhost";
        private MongoClient _client { get; set; }
        private IMongoDatabase _database { get; set; }

        public DbContext()
        {
            _client = new MongoClient(_connectionString);
            _database = _client.GetDatabase("riderr");
            _gridFS = new GridFSBucket(_database);
        }
        
        //Collections
        public IMongoCollection<User> Users
        {
            get => _database.GetCollection<User>("users");
        }

        //public IMongoCollection<Message> Messages
        //{
        //    get => _database.GetCollection<Message>("messages");
        //}

        
        
        //Users
        private async Task<User> GetUser(string _id)
        {
            return await Users.Find(new BsonDocument("_id", new ObjectId(_id))).FirstOrDefaultAsync();
        }
        
        public async Task<IEnumerable<User>> GetUsers(BsonDocument filter)
        {
            if(filter is null)
            {
                filter = new BsonDocument();
            }
            return await Users.Find(filter).ToListAsync();
        }

        public async Task AddUser(User user)
        {
            await Users.InsertOneAsync(user);
        }

        //UpdateUser
        //RemoveUser

        //Images
        public async Task StoreImage(string _id, Stream imageStream, string imageName)
        {
            User user = await GetUser(_id);
            if (user.HasImage())
            {
                await _gridFS.DeleteAsync(new ObjectId(user.ImageId));
            }
            var imageId = await _gridFS.UploadFromStreamAsync(imageName, imageStream);
            user.ImageId = imageId.ToString();

            var filter = Builders<User>.Filter.Eq("_id", new ObjectId(user.UserId));
            var update = Builders<User>.Update.Set("ImageId", user.ImageId);
            await Users.UpdateOneAsync(filter, update);
        }

        public async Task<byte[]> GetUserImage(string _id)
        {
            return await _gridFS.DownloadAsBytesAsync(new ObjectId(_id));
        }
    }
}
