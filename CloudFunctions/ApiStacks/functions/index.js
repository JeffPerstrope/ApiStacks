const functions = require('firebase-functions');
const admin = require("firebase-admin");
admin.initializeApp(functions.config().firebase);

const cors = require('cors')({ origin: true });
const express = require("express");
const ftp = require("basic-ftp");
const fs = require("fs");
const https = require('https');

const { Duplex } = require("stream");



//############################################################
// AUTHORIZE API REQUESTS
//############################################################

const AdminAPIKey = "9f61ed46-1214-4a95-b17f-eec1b0161994";

async function authorizeAPIRequest(request) {
  try {

    var apiKey = request.headers['x-api-key'];
    var appName_dirty = request.url.toLowerCase().substr(request.url.lastIndexOf('/') + 1);
    var appName_clean = appName_dirty.split('?')[0];
    var appName_query = appName_dirty.split('?')[1];

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
        "current": 1,
        "cache": await loadCache(appName_clean, appName_query)
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

      if (!userEnabledApps.hasChild(appName_clean)) {
        return {
          "status": "failed",
          "timestamp": Date.now(),
          "reason": appName_clean + " is currently disabled"
        }
      }

      //Load Cache Data
      var cacheData = await loadCache(appName_clean, appName_query);

      //Check if user has exceeded api limit
      if (apiKeyUsageDic.current < apiKeyUsageDic.max) {
        return {
          "status": "success",
          "timestamp": Date.now(),
          "max": apiKeyUsageDic.max,
          "current": apiKeyUsageDic.current,
          "cache": cacheData
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

async function saveCache(request, data) {
  var appName_dirty = request.url.toLowerCase().substr(request.url.lastIndexOf('/') + 1);
  var appName_clean = appName_dirty.split('?')[0];
  var appName_query = appName_dirty.split('?')[1];

  var encodedKey = encode(appName_query);
  var payload = {
    "data": data.data,
    "timestamp": Date.now()
  }

  await admin.database().ref('Cache/' + appName_clean + '/' + encodedKey).set(payload);
}

async function loadCache(appname, query) {
  var encodedKey = encode(query);
  const cacheData = await admin.database().ref('Cache/' + appname + '/' + encodedKey).once('value');
  if (cacheData.exists()) {
    var cacheDataVal = cacheData.val();
    const cacheReturn = {
      "status": "success",
      "timestamp": Date.now(),
      "data": cacheDataVal.data
    }
    return cacheReturn;
  }
  else {
    return null;
  }
}

function encode(string, base) {
  string = string.replace("http://", "").replace("https://", "").replace("www.", "").replace("/", "");
  var number = "0x";
  var length = string.length;
  for (var i = 0; i < length; i++)
    number += string.charCodeAt(i).toString(36);

  var trimmedKey = number; //.substring(0, 40);
  if (trimmedKey !== undefined && trimmedKey !== "") {
    return "0x1" + trimmedKey;
  } else {
    return number;
  }
}


//#################################################  API ENDPOINTS ##########################################################



//############################################################
// WEB SCREENSHOTS
//############################################################

const captureWebsite = require("capture-website");

exports.getscreenshot = functions.https.onRequest((request, response) => {

  //Parse request
  var URL = request.query.url.replace("http://", "").replace("https://", "").replace("www.", "");
  if (URL === undefined || URL.trim() === "") {
    return response.json({
      "status": "failed",
      "timestamp": Date.now(),
      "reason": "incorrect request format"
    });
  }
  URL = "http://" + URL;

  //Authorize Request
  return authorizeAPIRequest(request).then(function (authorizationResult) {
    if (authorizationResult.status === "failed") {
      return response.json(authorizationResult);
    }

    //Load Cache if present
    if (authorizationResult.cache !== null && authorizationResult.cache !== undefined && authorizationResult.cache !== "") {
      incrementAPIRequest(request, authorizationResult.current);
      return response.json(authorizationResult.cache);
    }

    //URL is valid, generate GUID
    var imageID = getGUID();
    var imageName = imageID + ".jpeg";
    var destination = "https://apistacks.com/cdn/repo/screenshots/" + imageName;

    //Grab Image
    return captureWebsite
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

        uploadftp(myReadableStream, imageName, "apistk_ftp_shot", "6p$1sx9C")
          .then(function (data) {
            const successData = {
              "status": "success",
              "timestamp": Date.now(),
              "data": destination
            }

            //Save new data to the cache
            saveCache(request, successData);
            incrementAPIRequest(request, authorizationResult.current);
            return response.json(successData);
          })
          .catch((e) => {
            console.log(e);
            const errorMessage = {
              "status": "failed",
              "timestamp": Date.now(),
              "reason": "unable to capture screenshot 1, possible bot blocked"
            }
            response.json(errorMessage);
          });
        return false;
      })
      .catch((e) => {
        console.log(e);
        const errorMessage = {
          "status": "failed",
          "timestamp": Date.now(),
          "reason": "unable to capture screenshot 2, possible bot blocked"
        }
        response.json(errorMessage);
      });
  });
});


//############################################################
// WEB PDF
//############################################################

const puppeteer = require("puppeteer");
const RenderPDF = require('chrome-headless-render-pdf');

exports.getpdf = functions.https.onRequest((request, response) => {

  //Parse request
  var URL = request.query.url.replace("http://", "").replace("https://", "").replace("www.", "");
  if (URL === undefined || URL.trim() === "") {
    return response.json({
      "status": "failed",
      "timestamp": Date.now(),
      "reason": "incorrect request format"
    });
  }
  URL = "http://" + URL;

  //Authorize Request
  return authorizeAPIRequest(request).then(function (authorizationResult) {
    if (authorizationResult.status === "failed") {
      return response.json(authorizationResult);
    }

    //Return cache if exists
    if (authorizationResult.cache !== null && authorizationResult.cache !== undefined && authorizationResult.cache !== "") {
      incrementAPIRequest(request, authorizationResult.current);
      return response.json(authorizationResult.cache);
    }

    //URL is valid, generate GUID
    var imageID = getGUID();
    var imageName = imageID + ".pdf";
    //var imageFullPath = __dirname + "/public/capture/" + imageName;
    var destination = "https://apistacks.com/cdn/repo/pdf/" + imageName;

    RenderPDF.generatePdfBuffer(URL, { "chromeBinary": puppeteer.executablePath(), "includeBackground": true, "chromeOptions": ["--ignore-certificate-errors", "--no-sandbox", "--ignore-certificate-errors", "--disable-setuid-sandbox"] })
      .then((pdfBuffer) => {
        const myReadableStream = bufferToStream(pdfBuffer);

        uploadftp(myReadableStream, imageName, "apistk_ftp_pdf", "%Yt53c5r")
          .then(function (data) {
            //Delete file from here
            //fs.unlinkSync(imageFullPath);
            const successData = {
              "status": "success",
              "timestamp": Date.now(),
              "value": URL,
              "data": destination
            }

            //Save new data to the cache
            saveCache(request, successData);
            incrementAPIRequest(request, authorizationResult.current);
            response.json(successData);
            return false;
          })
          .catch((e) => {
            console.log(e);
            const errorMessage = {
              "status": "failed",
              "timestamp": Date.now(),
              "reason": "unable to generate pdf, possible bot blocked"
            }
            response.json(errorMessage);
          });
        return false;
      })
      .catch((e) => {
        console.log(e);
        const errorMessage = {
          "status": "failed",
          "timestamp": Date.now(),
          "reason": "unable to generate pdf, possible bot blocked"
        }
        response.json(errorMessage);
      });

    return false;
    //response.json(imageID + dreams);
  });
});


//############################################################
// WHOIS
//############################################################

const whois = require("whois");

// send the default array of dreams to the webpage
exports.getwhois = functions.https.onRequest((request, response) => {

  //Parse request
  var URL = request.query.url.replace("http://", "").replace("https://", "").replace("www.", "");
  if (URL === undefined || URL.trim() === "") {
    return response.json({
      "status": "failed",
      "timestamp": Date.now(),
      "reason": "incorrect request format"
    });
  }

  //Authorize Request
  return authorizeAPIRequest(request).then(function (authorizationResult) {
    if (authorizationResult.status === "failed") {
      return response.json(authorizationResult);
    }

    //Return cache if exists
    if (authorizationResult.cache !== null && authorizationResult.cache !== undefined && authorizationResult.cache !== "") {
      incrementAPIRequest(request, authorizationResult.current);
      return response.json(authorizationResult.cache);
    }

    whois.lookup(URL, function (err, data) {

      data = data.replace("\\", "");
      if (err !== null) {
        return response.json({
          "status": "failed",
          "timestamp": Date.now(),
          "reason": "whois request was blocked"
        });
      }

      var array = [];
      var char = '\n';
      var i = j = 0;
      while ((j = data.indexOf(char, i)) !== -1) {
        array.push(data.substring(i, j).replace(':', '": "'));
        i = j + 1;
      }

      const successData = {
        "status": "success",
        "timestamp": Date.now(),
        "url": URL,
        "data": array
      }

      //Save new data to the cache
      saveCache(request, successData);
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

  //Parse request
  var URL = request.query.url.replace("http://", "").replace("https://", "").replace("www.", "");
  if (URL === undefined || URL.trim() === "") {
    return response.json({
      "status": "failed",
      "timestamp": Date.now(),
      "reason": "incorrect request format"
    });
  }
  URL = "http://" + URL;

  //Authorize Request
  return authorizeAPIRequest(request).then(function (authorizationResult) {
    if (authorizationResult.status === "failed") {
      return response.json(authorizationResult);
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

  //Parse request
  var URL = request.query.url.replace("http://", "").replace("https://", "").replace("www.", "");
  if (URL === undefined || URL.trim() === "") {
    return response.json({
      "status": "failed",
      "timestamp": Date.now(),
      "reason": "incorrect request format"
    });
  }
  URL = "http://" + URL;

  //Authorize Request
  return authorizeAPIRequest(request).then(function (authorizationResult) {
    if (authorizationResult.status === "failed") {
      return response.json(authorizationResult);
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

  //Parse request
  var URL = request.query.url.replace("http://", "").replace("https://", "").replace("www.", "");
  if (URL === undefined || URL.trim() === "") {
    return response.json({
      "status": "failed",
      "timestamp": Date.now(),
      "reason": "incorrect request format"
    });
  }
  URL = "http://" + URL;

  //Authorize Request
  return authorizeAPIRequest(request).then(function (authorizationResult) {
    if (authorizationResult.status === "failed") {
      return response.json(authorizationResult);
    }

    //Return cache if exists
    if (authorizationResult.cache !== null && authorizationResult.cache !== undefined && authorizationResult.cache !== "") {
      incrementAPIRequest(request, authorizationResult.current);
      return response.json(authorizationResult.cache);
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

  //Parse request
  var URL = request.query.url;
  if (URL === undefined || URL.trim() === "") {
    return response.json({
      "status": "failed",
      "timestamp": Date.now(),
      "reason": "incorrect request format"
    });
  }

  //Parse Email
  if (!validateEmail(URL)) {
    return response.json({
      "status": "failed",
      "timestamp": Date.now(),
      "email": URL,
      "reason": "email was not valid"
    });
  }


  //Authorize Request
  return authorizeAPIRequest(request).then(function (authorizationResult) {
    if (authorizationResult.status === "failed") {
      return response.json(authorizationResult);
    }

    //Return cache if exists
    if (authorizationResult.cache !== null && authorizationResult.cache !== undefined && authorizationResult.cache !== "") {
      incrementAPIRequest(request, authorizationResult.current);
      return response.json(authorizationResult.cache);
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

  //Parse request
  var URL = request.query.url;
  if (URL === undefined || URL.trim() === "") {
    return response.json({
      "status": "failed",
      "timestamp": Date.now(),
      "reason": "incorrect request format"
    });
  }

  //Authorize Request
  return authorizeAPIRequest(request).then(function (authorizationResult) {
    if (authorizationResult.status === "failed") {
      return response.json(authorizationResult);
    }
    //Return cache if exists
    if (authorizationResult.cache !== null && authorizationResult.cache !== undefined && authorizationResult.cache !== "") {
      incrementAPIRequest(request, authorizationResult.current);
      return response.json(authorizationResult.cache);
    }

    //URL is valid, generate GUID
    var imageID = getGUID();
    var imageName = imageID + ".png";
    var imageFullPath = "/tmp/" + imageName;
    var destination = "https://apistacks.com/cdn/repo/qr/" + imageName;
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
        if (err) { 
          console.log(err); 
          const errorMessage = {
            "status": "failed",
            "timestamp": Date.now(),
            "reason": err
          }
          
          return response.json(errorMessage);
        }
        uploadftp(imageFullPath, imageName, "apistk_ftp_qr", "Gjh2*8v9").then(function (data) {
          //Delete file from here
          fs.unlinkSync(imageFullPath);

          const successData = {
            "status": "success",
            "timestamp": Date.now(),
            "data": destination
          }

          incrementAPIRequest(request, authorizationResult.current);

          response.json(successData);
          return;
        }).catch(function (err) {
          const errorMessage = {
            "status": "failed",
            "timestamp": Date.now(),
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
    const errorMessage = {
      "status": "failed",
      "timestamp": Date.now(),
      "reason": "unable to generate qr code"
    }
    
    return response.json(errorMessage);
  });
});







//############################################################
// LANGUAGE DETECT
//############################################################

const LanguageDetect = require('languagedetect');


// send the default array of dreams to the webpage
exports.detectlanguage = functions.https.onRequest((request, response) => {

  //Parse request
  var URL = request.query.url;
  if (URL === undefined || URL.trim() === "") {
    return response.json({
      "status": "failed",
      "timestamp": Date.now(),
      "reason": "incorrect request format"
    });
  }


  //Authorize Request
  return authorizeAPIRequest(request).then(function (authorizationResult) {
    if (authorizationResult.status === "failed") {
      return response.json(authorizationResult);
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

  //Parse request
  var URL = request.query.url;
  if (URL === undefined || URL.trim() === "") {
    return response.json({
      "status": "failed",
      "timestamp": Date.now(),
      "reason": "incorrect request format"
    });
  }

  //Authorize Request
  return authorizeAPIRequest(request).then(function (authorizationResult) {
    if (authorizationResult.status === "failed") {
      return response.json(authorizationResult);
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

  var userID = request.query.userid;
  if (userID === undefined || userID.trim() === "") {
    return response.status(404);
  }

  cors(request, response, () => {

    //Get Customer ID from Database
    return admin.database().ref('/Users/' + userID).once('value').then((userSnapshot) => {
      if (userSnapshot.exists()) {
        var stripeID = userSnapshot.stripeID;

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
          customer: stripeID,
          success_url: 'https://apistacks.com/Plan?subscription=success',
          cancel_url: 'https://apistacks.com/Plan?subscription=cancel'
        }).then(result => {
          // response.writeHead(200, { 'Access-Control-Allow-Origin': '*' });
          // response.setHeader('Access-Control-Allow-Origin', '*');
          return response.status(200).json({ id: result.id });
        });
      }
      else {
        return response.status(404);
      }
    })
  });
});





//#################################################  HELPERS ##########################################################

//Upload file to drive
async function uploadftp(imageSource, imageName, user, password) {
  const client = new ftp.Client();
  client.ftp.verbose = true;
  try {
    await client.access({
      host: "apistacks.com",
      user: user,
      password: password,
      secure: false,
    });
    await client.list();
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