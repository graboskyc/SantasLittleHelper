exports = function(userid){
  var retval = false;
  if(userid.length !=24) {
    retval = true;
  }
  return retval
};