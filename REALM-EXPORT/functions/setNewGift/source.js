exports = async function(gift, link){
  const currentUser = context.user;
  var d = new Date();
  
  var conn = context.services.get("mongodb-atlas").db("santa").collection("gifts");
  
  var doc = {ownerId:currentUser.id, email:currentUser.data.email, gift:gift, created:d}
  
  if(link.length > 5) {
    doc.link = link.trim();
  }
  await conn.insertOne(doc);
};