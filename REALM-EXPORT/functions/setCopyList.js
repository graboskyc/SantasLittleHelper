exports = async function(name, share, oldshare){
  const currentUser = context.user;
  var d = new Date();
  
  var conn = context.services.get("mongodb-atlas").db("santa").collection("lists");
  var oldDoc = await conn.findOne({share:oldshare});
  
  var doc = {ownerId:oldDoc.ownerId, email:currentUser.data.email, name:name, share:share, created:d}

  await conn.insertOne(doc);
  
  var conng = context.services.get("mongodb-atlas").db("santa").collection("gifts");
  var pipeline = [
    {$match:{purchasedBy:{$exists:false}, deleted:{$ne:true}, event:oldshare}},
    {$sort: {_id:1}}
    ];
    
  var docs = await conng.aggregate(pipeline).toArray();
  for (var i = 0; i < docs.length; i++) {
    docs[i]["event"] = share;
    delete docs[i]["_id"];
  }
  await conng.insertMany(docs);
  
};
