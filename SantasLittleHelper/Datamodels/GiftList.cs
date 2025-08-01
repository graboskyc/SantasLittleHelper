using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SantasLittleHelper.Datamodels
{
    [BsonIgnoreExtraElements]
    class GiftList
    {
        public ObjectId _id { get; set; }
        public string name { get; set; }
        public string share { get; set; }
        public List<string> ownerId { get; set; } = new List<string>();
    }
}