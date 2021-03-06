exports = async function(eventcode){
  const currentUser = context.user
  var conn = context.services.get("mongodb-atlas").db("santa").collection("gifts");
  
  var pipeline = [{$match: {
    ownerId:{$ne:currentUser.id},
    event:eventcode
    }}, {$group: {
      _id: "$email",
      "giftlist": {$push:"$$ROOT"}
    }},
    {$sort: {_id:1}}];
    
  var docs = await conn.aggregate(pipeline).toArray();

  return docs;
};