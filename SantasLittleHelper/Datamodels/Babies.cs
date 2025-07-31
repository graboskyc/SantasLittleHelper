using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SantasLittleHelper.Datamodels
{
    [BsonIgnoreExtraElements]
    class Babies
    {
        public ObjectId _id { get; set; }
        public string name { get; set; }
        public string babycolor { get; set; }
    }
}