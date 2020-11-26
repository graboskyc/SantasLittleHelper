exports = async function(name, share){
  const currentUser = context.user;
  var d = new Date();
  
  var conn = context.services.get("mongodb-atlas").db("santa").collection("lists");
  
  var doc = {ownerId:[currentUser.id], email:currentUser.data.email, name:name, share:share, created:d}

  await conn.insertOne(doc);
};