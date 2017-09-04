var fs = require('fs');

const readline = require('readline');



var baseDir = "./parserDataSets";

fs.readdir(baseDir, function (err, items) {
  items.forEach(function (t) {
    var fileName = baseDir + "/" + t;
    const rl = readline.createInterface({ input: require('fs').createReadStream(fileName) });

    rl.on('line', function (line) {
      line = line.replace(/S  \(/g, "S(");
      // var finalLine = (line.replace(line.trim(), convert(line.trim()))).replace(/__/g, "_") + '\n';
      fs.appendFileSync(
        fileName.substring(0, fileName.length - 4) + '.mrg',
        line + '\n');
    });
  })
});


function convert(s) {
  var res="";
  var counter=0;
  for(var i=0;i<s.length;i++){
    if(s.charAt(i)===' '){
      counter++;
      if(counter>1){
        res+="_"
      }else{
        res+=" ";
      }
    }else{
      if(s.charAt(i)==='('||s.charAt(i)===')'){
        counter=0;
      }
      res+=s.charAt(i);
    }
  }return res;
}