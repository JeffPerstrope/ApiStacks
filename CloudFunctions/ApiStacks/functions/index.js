const functions = require('firebase-functions');
const admin = require("firebase-admin");
admin.initializeApp(functions.config().firebase);

const cors = require('cors')({ origin: true });
const express = require("express");
const ftp = require("basic-ftp");
const fs = require("fs");
const { Duplex } = require("stream");



//############################################################
// AUTHORIZE API REQUESTS
//############################################################

const AdminAPIKey = "9f61ed46-1214-4a95-b17f-eec1b0161994";

async function authorizeAPIRequest(request) {
  try {
    var apiKey = request.headers['x-api-key'];

    //If key is not provided
    if (apiKey === null || apiKey === undefined) {
      return {
        "status": "failed",
        "timestamp": Date.now(),
        "reason": "'x-api-key'was not found in header"
      }
    }

    //Authorize Admin Key
    if (apiKey === AdminAPIKey) {
      return {
        "status": "success",
        "max": 10000000,
        "current": 1
      };
    }

    //Get Data from Database
    const apiKeyUsageData = await admin.database().ref('/Usage/' + apiKey).once('value');

    //Check if user can proceed
    if (apiKeyUsageData.exists()) {
      var apiKeyUsageDic = apiKeyUsageData.val();

      //Check if app is enabled
      const userID = apiKeyUsageDic.userID;
      const userEnabledApps = await admin.database().ref('/Users/' + userID + '/apps').once('value');
      var appName_dirty = request.url.substr(request.url.lastIndexOf('/') + 1);
      var appName_clean = appName_dirty.split('?')[0];
      console.log("Clean Name: " + appName_clean);

      if (!userEnabledApps.hasChild(appName_clean)) {
        return {
          "status": "failed",
          "timestamp": Date.now(),
          "reason": appName_clean + " is currently disabled"
        }
      }

      //Check if user has exceeded api limit
      if (apiKeyUsageDic.current < apiKeyUsageDic.max) {
        return {
          "status": "success",
          "timestamp": Date.now(),
          "max": apiKeyUsageDic.max,
          "current": apiKeyUsageDic.current
        }
      } else {
        return {
          "status": "failed",
          "timestamp": Date.now(),
          "reason": "usage limit exceeded"
        }
      }
    }

    //API Key was not found in the database
    else {
      return {
        "status": "failed",
        "timestamp": Date.now(),
        "reason": "invalid authorization key"
      }
    }
  } catch (ex) {
    return {
      "status": "failed",
      "timestamp": Date.now(),
      "reason": "unknown error occured:" + ex
    }
  }
}

async function incrementAPIRequest(request, currentUsage) {
  var apiKey = request.headers['x-api-key'];

  if (apiKey === AdminAPIKey) {
    return;
  }

  var newUsage = currentUsage += 1;
  var payload = {
    "current": newUsage
  };
  await admin.database().ref('/Usage/' + apiKey).update(payload);
}

function loadCache(appname, request)
{
  
}


//#################################################  API ENDPOINTS ##########################################################




//############################################################
// WHOIS
//############################################################

const whois = require("whois");

// send the default array of dreams to the webpage
exports.getwhois = functions.https.onRequest((request, response) => {

  //Authorize Request
  return authorizeAPIRequest(request).then(function (authorizationResult) {
    if (authorizationResult.status === "failed") {
      return response.json(authorizationResult);
    }

    //Clean URL
    var URL = request.query.url.toLowerCase().replace("http://", "").replace("https://", "");
    if (URL === undefined || URL.trim() === "") {
      return response.json({
        "status": "failed",
        "timestamp": Date.now(),
        "reason": "incorrect request format"
      });
    }

    whois.lookup(URL, function (err, data) {
      var array = [];
      var char = '\n';
      var i = j = 0;
      while ((j = data.indexOf(char, i)) !== -1) {
        //console.log(data.substring(i, j));
        array.push(data.substring(i, j).replace(':', '": "').replace("\\", ""));
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

      incrementAPIRequest(request, authorizationResult.current);

      response.json(successData);
    });
    return;
  }).catch(err => {
    console.log(err);
    return;
  });
});



//############################################################
// WEB SCRAPER
//############################################################

var htmlRequest = require("request");

// send the default array of dreams to the webpage
exports.scrapewebsite = functions.https.onRequest((request, response) => {

  //Authorize Request
  return authorizeAPIRequest(request).then(function (authorizationResult) {
    if (authorizationResult.status === "failed") {
      return response.json(authorizationResult);
    }

    //Parse request
    var URL = "http://" + request.query.url.replace("http://", "").replace("https://", "");
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

    incrementAPIRequest(request, authorizationResult.current);

    htmlRequest(options, function (error, res, body) {
      console.error("error:", error); // Print the error if one occurred
      console.log("statusCode:", res && res.statusCode); // Print the response status code if a response was received
      console.log("body:", body); // Print the HTML for the Google homepage.
      response.send(body);
    });

    return;
  }).catch(err => {
    console.log(err);
    return;
  });
});



//############################################################
// LINK SCRAPER
//############################################################

const $ = require("cheerio");
const rp = require("request-promise");

exports.scrapelinks = functions.https.onRequest((request, response) => {

  //Authorize Request
  return authorizeAPIRequest(request).then(function (authorizationResult) {
    if (authorizationResult.status === "failed") {
      return response.json(authorizationResult);
    }
    //Parse request
    var URL = "https://" + request.query.url.replace("http://", "").replace("https://", "");
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

        incrementAPIRequest(request, authorizationResult.current);

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
    return;
  }).catch(err => {
    console.log(err);
    return;
  });
});



//############################################################
// OPEN GRAPH
//############################################################

const extract = require('meta-extractor');


// send the default array of dreams to the webpage
exports.getmeta = functions.https.onRequest((request, response) => {

  //Authorize Request
  return authorizeAPIRequest(request).then(function (authorizationResult) {
    if (authorizationResult.status === "failed") {
      return response.json(authorizationResult);
    }
    //Parse request
    var URL = "http://" + request.query.url.replace("http://", "").replace("https://", "");
    if (URL === undefined || URL.trim() === "") {
      response.sendStatus(404);
      return;
    }


    return extract({ uri: URL }, (err, res) => {
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

      incrementAPIRequest(request, authorizationResult.current);

      const successData = {
        "status": "success",
        "timestamp": Date.now(),
        "url": URL,
        "data": res
      }
      response.json(successData);
      return;
    });
  }).catch(err => {
    console.log(err);
    return;
  });
});





//############################################################
// EMAIL CHECKER
//############################################################


//var emailCheck = require('email-exists');
var emailCheck = require('@reacherhq/api');

exports.checkemail = functions.https.onRequest((request, response) => {

  //Authorize Request
  return authorizeAPIRequest(request).then(function (authorizationResult) {
    if (authorizationResult.status === "failed") {
      return response.json(authorizationResult);
    }


    //Parse request
    var URL = request.query.url;
    if (URL === undefined || URL.trim() === "") {
      response.sendStatus(404);
      return;
    }

    if (!validateEmail(URL)) {
      return response.json({
        "status": "failed",
        "timestamp": Date.now(),
        "email": URL,
        "reason": "email was not valid"
      });
    }

    return emailCheck.checkEmail({ to_email: URL }).then(function (data) {
      const successData = {
        "status": "success",
        "timestamp": Date.now(),
        "input": URL,
        "data": data
      }

      incrementAPIRequest(request, authorizationResult.current);

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
  }).catch(err => {
    console.log(err);
    return;
  });
});

function validateEmail(email) {
  const EMAIL_REGEX = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
  if (typeof email === 'string' && email.length > 5 && email.length < 61 && EMAIL_REGEX.test(email)) {
    return true;
  } else {
    return false;
  }
}






//############################################################
// QR CODE GENERATOR
//############################################################

const QRCode = require("qrcode");

// send the default array of dreams to the webpage
exports.generateqr = functions.https.onRequest((request, response) => {

  //Authorize Request
  return authorizeAPIRequest(request).then(function (authorizationResult) {
    if (authorizationResult.status === "failed") {
      return response.json(authorizationResult);
    }

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
        if (err) { console.log(err); response.sendStatus(404); return; }

        uploadScrape(imageFullPath, imageName).then(function (data) {
          //Delete file from here
          fs.unlinkSync(imageFullPath);

          const successData = {
            "status": "success",
            "timestamp": Date.now(),
            "input": URL,
            "data": destination
          }

          incrementAPIRequest(request, authorizationResult.current);

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
  }).catch(err => {
    console.log(err);
    return;
  });
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


  //Authorize Request
  return authorizeAPIRequest(request).then(function (authorizationResult) {
    if (authorizationResult.status === "failed") {
      return response.json(authorizationResult);
    }


    //Parse request
    var URL = request.query.url;
    if (URL === undefined || URL.trim() === "") {
      response.sendStatus(404);
      return;
    }

    const lngDetector = new LanguageDetect();
    var detectedLanguages = lngDetector.detect(URL, 2);
    if (detectedLanguages === undefined || detectedLanguages.length <= 0) {
      const errorMessage = {
        "status": "failed",
        "timestamp": Date.now(),
        "value": URL,
        "reason": "unable to parse text"
      }

      incrementAPIRequest(request, authorizationResult.current);
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
  }).catch(err => {
    console.log(err);
    return;
  });
});





//############################################################
// EMAIL SCRAPER
//############################################################

const Scraper = require("email-crawler");


// send the default array of dreams to the webpage
exports.scrapeemail = functions.https.onRequest((request, response) => {

  //Authorize Request
  return authorizeAPIRequest(request).then(function (authorizationResult) {
    if (authorizationResult.status === "failed") {
      return response.json(authorizationResult);
    }

    //Parse request
    var URL = request.query.url;
    if (URL === undefined || URL.trim() === "") {
      response.sendStatus(404);
      return;
    }
    var emailscraper = new Scraper(URL);
    // A level is how far removed (in  terms of link clicks) a page is from the root page (only follows same domain routes)
    return emailscraper.getLevels(50).then((emails) => {
      console.log(emails); // Here are the emails crawled from traveling two levels down this domain
      response.json(emails);

      incrementAPIRequest(request, authorizationResult.current);

      return;
    })
      .catch((e) => {
        console.log("error");
      })
  }).catch(err => {
    console.log(err);
    return;
  });
});






//#################################################  STRIPE ##########################################################


const stripe = require('stripe')('sk_test_51HcZiyF6EVrg0l22KUHmhtN6fxfGQvV1yG2vk2My3Dnq6N4zTg3CASy3OKzArWvWij8CL7BwnqGDPY8xke0Hsmq100FLmHAkYc');

exports.stripecheckout = functions.https.onRequest((request, response) => {

  cors(request, response, () => {

    var priceItem = "";
    var plan = request.query.plan.toLowerCase();
    if (plan === "starter")
      priceItem = "price_1HcZm0F6EVrg0l22lVOfn9c8";
    else if (plan === "professional")
      priceItem = "price_1HfvS8F6EVrg0l22KoQlyZp8";
    else if (plan === "ultimate")
      priceItem = "price_1HfwJ3F6EVrg0l22NYrfJDO6";

    return stripe.checkout.sessions.create({
      payment_method_types: ['card'],
      line_items: [
        {
          price: priceItem,
          quantity: 1,
        },
      ],
      mode: 'subscription',
      success_url: 'https://apistacks.com/Plan?subscription=success',
      cancel_url: 'https://apistacks.com/Plan?subscription=cancel'
    }).then(result => {
      // response.writeHead(200, { 'Access-Control-Allow-Origin': '*' });
      // response.setHeader('Access-Control-Allow-Origin', '*');
      return response.status(200).json({ id: result.id });
    });
  });
});