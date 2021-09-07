exports = async function(idstr){
  const currentUser = context.user;
  var d = new Date();
  
  var conn = context.services.get("mongodb-atlas").db("santa").collection("gifts");
  
  var findDoc = {_id: BSON.ObjectId(idstr) };
  var setDoc = {purchasedBy:currentUser.data.email};
  
  await conn.updateOne(findDoc, {$set: setDoc});
};