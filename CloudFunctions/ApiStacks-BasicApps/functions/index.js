const functions = require('firebase-functions');
const admin = require("firebase-admin");
admin.initializeApp();

const ftp = require("basic-ftp");
const fs = require("fs");
const { Duplex } = require("stream");



//############################################################
// AUTHORIZE API REQUESTS
//############################################################

function authorizeAPI(request)
{
  var apiKey = request.headers['x-api-key']; 

  //If key is not provided
  if(apiKey === null || apiKey === undefined)
  {
    return {
      "status": "failed",
      "timestamp": Date.now(),
      "step": "api key authorization",
      "reason": "api key was not found in the request header 'x-api-key'"
    }
  }

  // Get a database reference to our posts
  return admin.database().ref("Main/Usage/" + apiKey).once('value').then(function (userUsage) {
    return {
      "status": "failed",
      "timestamp": Date.now(),
      "step": "testing",
      "reason": userUsage
    };
  });
}



//###########################################################################################################



//############################################################
// WHOIS
//############################################################

const whois = require("whois");

// send the default array of dreams to the webpage
exports.getwhois = functions.https.onRequest((request, response) => {

  //Check for API Key
  var validation = authorizeAPI(request);
  if(validation.status === "failed")
  {
    response.json(validation);
    return;
  }

  //Parse request
  var URL = request.query.url;
  if (URL === undefined || URL.trim() === "") {
    response.sendStatus(404);
    return;
  }

  whois.lookup(URL, function (err, data) {
    var array = [];
    var char = '\n';
    var i = j = 0;
    while ((j = data.indexOf(char, i)) !== -1) {
      //console.log(data.substring(i, j));
      array.push(data.substring(i, j).replace(':', '": "'));
      i = j + 1;
    }

    if (err !== null) {
      response.sendStatus(404);
      return;
    }

    const successData = {
      "status": "success",
      "timestamp": Date.now(),
      "url": URL,
      "data": array
    }

    response.json(successData);
    //response.send(JSON.stringify(data));
  });
});



//############################################################
// WEB SCRAPER
//############################################################

var htmlRequest = require("request");

// send the default array of dreams to the webpage
exports.scrapewebsite = functions.https.onRequest((request, response) => {
  //Parse request
  var URL = request.query.url;
  if (URL === undefined || URL.trim() === "") {
    response.sendStatus(404);
    return;
  }

  //Get HTML
  const options = {
    url: URL,
    gzip: true,
    headers: {
      "User-Agent": "request"
    }
  };

  htmlRequest(options, function (error, res, body) {
    console.error("error:", error); // Print the error if one occurred
    console.log("statusCode:", res && res.statusCode); // Print the response status code if a response was received
    console.log("body:", body); // Print the HTML for the Google homepage.
    response.send(body);
  });
});



//############################################################
// LINK SCRAPER
//############################################################

const $ = require("cheerio");
const rp = require("request-promise");

exports.scrapelinks = functions.https.onRequest((request, response) => {
  //Parse request
  var URL = request.query.url;
  if (URL === undefined || URL.trim() === "") {
    response.sendStatus(404);
    return;
  }

  rp(URL)
    .then(html => {
      const linkObjects = $("a", html);
      // this is a mass object, not an array

      const total = linkObjects.length;
      // The linkObjects has a property named "lenght"

      //If no links are found
      if (total <= 0) {
        const errorMessage = {
          "status": "failed",
          "timestamp": Date.now(),
          "url": URL,
          "reason": "bot was blocked"
        }
        response.json(errorMessage);
        return;
      }
      const links = [];

      for (let i = 0; i < total; i++) {

        links.push({
          href: linkObjects[i].attribs.href.replace("\"/", "\"/" + URL),
          title: linkObjects[i].attribs.title
        });
      }

      const successData = {
        "status": "success",
        "timestamp": Date.now(),
        "url": URL,
        "data": links
      }

      response.json(successData);
      return;
    })
    .catch(err => {
      const errorMessage = {
        "status": "failed",
        "timestamp": Date.now(),
        "url": URL,
        "reason": "bot was blocked"
      }
      console.log(err);
      response.json(errorMessage);
      return;
    });
});



//############################################################
// OPEN GRAPH
//############################################################

const extract = require('meta-extractor');


// send the default array of dreams to the webpage
exports.getmeta = functions.https.onRequest((request, response) => {
  //Parse request
  var URL = request.query.url;
  if (URL === undefined || URL.trim() === "") {
    response.sendStatus(404);
    return;
  }


  extract({ uri: URL }, (err, res) => {
    if (err !== null) {
      const errorMessage = {
        "status": "failed",
        "timestamp": Date.now(),
        "url": URL,
        "reason": "bot was blocked"
      }
      response.json(errorMessage);
      return;
    }

    const successData = {
      "status": "success",
      "timestamp": Date.now(),
      "url": URL,
      "data": res
    }
    response.json(successData);
    return;
  });
});




//############################################################
// EMAIL SCRAPER
//############################################################

const Scraper = require("email-crawler");


// send the default array of dreams to the webpage
exports.scrapeemail = functions.https.onRequest((request, response) => {
  //Parse request
  var URL = request.query.url;
  if (URL === undefined || URL.trim() === "") {
    response.sendStatus(404);
    return;
  }
  var emailscraper = new Scraper(URL);
  // A level is how far removed (in  terms of link clicks) a page is from the root page (only follows same domain routes)
  emailscraper.getLevels(50).then((emails) => {
    console.log(emails); // Here are the emails crawled from traveling two levels down this domain
    response.json(emails);
    return;
  })
    .catch((e) => {
      console.log("error");
    })
});


//############################################################
// EMAIL CHECKER
//############################################################


//var emailCheck = require('email-exists');
var emailCheck = require('@reacherhq/api');

exports.checkemail = functions.https.onRequest((request, response) => {
  //Parse request
  var URL = request.query.url;
  if (URL === undefined || URL.trim() === "") {
    response.sendStatus(404);
    return;
  }

  emailCheck.checkEmail({ to_email: URL }).then(function (data) {
    const successData = {
      "status": "success",
      "timestamp": Date.now(),
      "input": URL,
      "data": data
    }

    response.send(successData);
    return;

  }).catch((e) => {
    const errorMessage = {
      "status": "failed",
      "timestamp": Date.now(),
      "email": URL,
      "reason": "unable to parse email request"
    }

    console.log(e);
    response.send(errorMessage);
    return;
  });
  
  return;
});

// const EMAIL_REGEX = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;

// function validateEmail(email) {
//   if (typeof email === 'string' && email.length > 5 && email.length < 61 && EMAIL_REGEX.test(email)) {
//     return email.toLowerCase();
//   } else {
//     return false;
//   }
// }






//############################################################
// QR CODE GENERATOR
//############################################################

const QRCode = require("qrcode");

// send the default array of dreams to the webpage
exports.generateqr = functions.https.onRequest((request, response) => {
  //Parse request
  var URL = request.query.url;
  if (URL === undefined || URL.trim() === "") {
    response.sendStatus(404);
    return;   
  }

  //URL is valid, generate GUID
  var imageID = getGUID();
  var imageName = imageID + ".png";
  var imageFullPath = __dirname + "/tmp/" + imageName;
  var destination = "https://api.apistacks.com/repo/" + imageName;
  
  QRCode.toFile(
    imageFullPath,
    URL,
    {
      color: {
        dark: "#000000", // Blue dots
        light: "#0000", // Transparent background
        width: 300,
        scale: 8
      }
    },
    function (err) {
      if (err){ console.log(err); response.sendStatus(404); return; }

      uploadScrape(imageFullPath, imageName).then(function (data) {
        //Delete file from here
        fs.unlinkSync(imageFullPath);

        const successData = {
          "status": "success",
          "timestamp": Date.now(),
          "input": URL,
          "data": destination
        }
  
        response.json(successData);
        return;
      }).catch(function (err) {
        const errorMessage = {
          "status": "failed",
          "timestamp": Date.now(),
          "email": URL,
          "reason": "unable to generate qr code"
        }
    
        console.log(err);
        response.send(errorMessage);
        return;
      });
      return;
    });

  return;
  //const myReadableStream = bufferToStream(data);
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
      secure: false
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






//############################################################
// LANGUAGE DETECT
//############################################################

const LanguageDetect = require('languagedetect');


// send the default array of dreams to the webpage
exports.detectlanguage = functions.https.onRequest((request, response) => {
  //Parse request
  var URL = request.query.url;
  if (URL === undefined || URL.trim() === "") {
    response.sendStatus(404);
    return;
  }
  
  const lngDetector = new LanguageDetect();
  var detectedLanguages = lngDetector.detect(URL, 2);
  if(detectedLanguages === undefined || detectedLanguages.length <= 0)
  {
    const errorMessage = {
      "status": "failed",
      "timestamp": Date.now(),
      "value": URL,
      "reason": "unable to parse text"
    }

    console.log(detectedLanguages);
    response.json(errorMessage);
    return;
  }

  const successData = {
    "status": "success",
    "timestamp": Date.now(),
    "value": URL,
    "data": detectedLanguages
  }
  response.json(successData);
  return;
});