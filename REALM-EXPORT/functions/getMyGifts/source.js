exports = async function(eventcode){
  const currentUser = context.user
  var conn = context.services.get("mongodb-atlas").db("santa").collection("gifts");
  
  var pipeline = [
    {$match:{ownerId:currentUser.id, deleted:{$ne:true}, event:eventcode}},
    {$sort: {_id:1}}
    ];
    
  var docs = await conn.aggregate(pipeline).toArray();

  return docs;
};