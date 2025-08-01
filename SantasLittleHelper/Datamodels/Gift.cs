using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SantasLittleHelper.Datamodels
{
    [BsonIgnoreExtraElements]
    class Gift
    {
        public ObjectId _id { get; set; }
        public string ownerId { get; set; }
        public string email { get; set; }
        public string gift { get; set; }
        public string? cost { get; set; } = null;
        public bool deleted { get; set; } = false;
        public DateTime created { get; set; }
        public string createdBy { get; set; } = "";
        public string? link { get; set; } = null;
        public string? purchasedBy { get; set; } = null;
        [BsonElement("event")]
        public string eventCode { get; set; }
    }
}