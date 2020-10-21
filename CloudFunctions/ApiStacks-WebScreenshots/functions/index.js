const functions = require("firebase-functions");
const captureWebsite = require("capture-website");
const ftp = require("basic-ftp");
const fs = require("fs");
const { Duplex } = require("stream");

//app.get("/getscreenshot", (request, response) => {
exports.getscreenshot = functions.https.onRequest((request, response) => {
  //Parse request
  var URL = request.query.url;
  if (URL === undefined || URL.trim() === "") {
    response.sendStatus(404);
    return;
  }

  //URL is valid, generate GUID
  var imageID = getGUID();
  var imageName = imageID + ".jpeg";
  //var imageFullPath = __dirname + "/public/capture/" + imageName;
  var destination = "https://api.apistacks.com/repo/" + imageName;

  //Grab Image
  //https://www.npmjs.com/package/capture-website
  captureWebsite
    .buffer(URL, {
      type: "jpeg",
      quality: 0.2,
      scaleFactor: 2,
      timeout: 30,
      launchOptions: {
        args: ["--no-sandbox"]
      },
    })
    .then(function (data) {
      const myReadableStream = bufferToStream(data);

      uploadScrape(myReadableStream, imageName)
        .then(function (data) {
          //Delete file from here
          //fs.unlinkSync(imageFullPath);
          response.json(destination);
          return false;
        })
        .catch((e) => {
          console.log(e);
          response.sendStatus(500);
        });
        return false;
    })
    .catch((e) => {
      console.log(e);
      response.sendStatus(500);
    });

    return false;
  //response.json(imageID + dreams);
});

//Upload file to drive
async function uploadScrape(imageSource, imageName) {
  const client = new ftp.Client();
  client.ftp.verbose = true;
  try {
    await client.access({
      host: "demo.adblock.evlar.net",
      user: "apistacks",
      password: "PassWord!",
      secure: false,
    });
    console.log(await client.list());
    await client.uploadFrom(imageSource, "/" + imageName);
    //await client.downloadTo("README_COPY.md", "README_FTP.md")
  } catch (err) {
    console.log(err);
  }
  client.close();
}

function getGUID() {
  return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function (c) {
    var r = (Math.random() * 16) | 0,
      v = c === "x" ? r : (r & 0x3) | 0x8;
    return v.toString(16);
  });
}

function bufferToStream(myBuuffer) {
  let tmp = new Duplex();
  tmp.push(myBuuffer);
  tmp.push(null);
  return tmp;
}


// // listen for requests
// const listener = app.listen(process.env.PORT, () => {
//   console.log("Your app is listening on port " + listener.address().port);
// });

// make all the files in 'public' available
//app.use(express.static("public"));

// https://expressjs.com/en/starter/basic-routing.html
// app.get("/", (request, response) => {
//   response.sendStatus(404);
//   //response.sendFile(__dirname + "/views/index.html");
// });