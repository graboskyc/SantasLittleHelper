exports = async function(idstr){
  var d = new Date();
  
  var conn = context.services.get("mongodb-atlas").db("santa").collection("gifts");
  
  var findDoc = {_id: BSON.ObjectId(idstr) };
  var setDoc = {deleted:true};
  
  await conn.updateOne(findDoc, {$set: setDoc});
};