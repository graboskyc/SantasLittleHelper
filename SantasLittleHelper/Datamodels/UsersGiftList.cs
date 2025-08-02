using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SantasLittleHelper.Datamodels
{
    [BsonIgnoreExtraElements]
    class UsersGiftList
    {
        [BsonElement("_id")]
        public string ForWho { get; set; }
        [BsonElement("giftlist")]
        public List<Gift> Gifts { get; set; } = new List<Gift>();
    }
}