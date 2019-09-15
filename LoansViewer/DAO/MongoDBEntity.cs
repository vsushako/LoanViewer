using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LoansViewer.DAO
{
    public abstract class MongoDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    }
}