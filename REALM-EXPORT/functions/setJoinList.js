exports = async function(share){
  const currentUser = context.user;
  var d = new Date();
  
  var conn = context.services.get("mongodb-atlas").db("santa").collection("lists");

  await conn.updateOne({share:share}, {$push:{ownerId:currentUser.id}});
};