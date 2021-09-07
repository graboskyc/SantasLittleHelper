exports = async function(gift, link, eventcode, cost){
  const currentUser = context.user;
  var d = new Date();
  
  var conn = context.services.get("mongodb-atlas").db("santa").collection("gifts");
  
  var doc = {ownerId:currentUser.id, email:currentUser.data.email, gift:gift, created:d, event:eventcode};
  
  if(cost.length > 0) {
    doc["cost"]  = cost.replace("$","");
  }
  
  if(link.length > 5) {
    doc.link = link.trim();
  }
  await conn.insertOne(doc);
};