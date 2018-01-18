
/**
 * Get the current URL.
 *
 * @param {function(string)} callback called when the URL of the current tab
 *   is found.
 */
function getCurrentTabUrl(callback) {
    // Query filter to be passed to chrome.tabs.query - see
    // https://developer.chrome.com/extensions/tabs#method-query
    var queryInfo = {
      active: true,
      currentWindow: true
    };
  
    chrome.tabs.query(queryInfo, (tabs) => {
      // chrome.tabs.query invokes the callback with a list of tabs that match the
      // query. When the popup is opened, there is certainly a window and at least
      // one tab, so we can safely assume that |tabs| is a non-empty array.
      // A window can only have one active tab at a time, so the array consists of
      // exactly one tab.
      var tab = tabs[0];
  
      // A tab is a plain object that provides information about the tab.
      // See https://developer.chrome.com/extensions/tabs#type-Tab
      var url = tab.url;
  
      // tab.url is only available if the "activeTab" permission is declared.
      // If you want to see the URL of other tabs (e.g. after removing active:true
      // from |queryInfo|), then the "tabs" permission is required to see their
      // "url" properties.
      console.assert(typeof url == 'string', 'tab.url should be a string');
  
      callback(url);
    });
}
function getSSID(callback) {
    chrome.cookies.get({"url": "http://127.0.0.1:5000", "name": "sommmmmmm"}, (cookie) => {
        if (cookie == null) {
            
            callback(null);
        }
        callback(cookie.value)
    });
}

document.addEventListener('DOMContentLoaded', () => {
    getSSID((ssid) => {
        if (ssid != null) {
        var record_auto = document.getElementById("record_auto");
        record_auto.addEventListener('click', () => {
            chrome.cookies.set({"url": "http://127.0.0.1:5000", "name": "sommmmmmm", "value": "aaaaaaa"});
        });
        var record_t = document.getElementById("record_t");
        record_t.addEventListener('click', () => {
            chrome.cookies.get({"url": "http://127.0.0.1:5000", "name": "sommmmmmm"}, (cookie) => {
                alert(cookie.value);
            });
        });
        getCurrentTabUrl((url) => {
            var record_now = document.getElementById("record_now");
            record_now.addEventListener('click', () => {
                var xhr = new XMLHttpRequest();
                //xhr.onreadystatechange = handleStateChange; // Implemented elsewhere.
                //ssid(登陆后)/收藏夹ID/URL/
                xhr.open("POST", "http://127.0.0.1:5000/add?a="+url+"&b="+ssid+"&c=3", true);
                xhr.onreadystatechange = function() {
                    if (xhr.readyState == 4) {
                      // 警告! 这样处理有可能被注入恶意脚本!
                      alert(xhr.responseText);
                    }
                  }
                xhr.send();
            });
        })
    } else {
        document.getElementById("logIn").style.display="block";
        //$("#logIn").show();
    }
    });
})
// Copyright (c) 2014 The Chromium Authors. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.



  // Most methods of the Chrome extension APIs are asynchronous. This means that
  // you CANNOT do something like this:
  //
  // var url;
  // chrome.tabs.query(queryInfo, (tabs) => {
  //   url = tabs[0].url;
  // });
  // alert(url); // Shows "undefined", because chrome.tabs.query is async.

/**
 * Change the background color of the current page.
 *
 * @param {string} color The new background color.
 */

/**
 * Gets the saved background color for url.
 *
 * @param {string} url URL whose background color is to be retrieved.
 * @param {function(string)} callback called with the saved background color for
 *     the given url on success, or a falsy value if no color is retrieved.
 */

/**
 * Sets the given background color for url.
 *
 * @param {string} url URL for which background color is to be saved.
 * @param {string} color The background color to be saved.
 */

// This extension loads the saved background color for the current tab if one
// exists. The user can select a new background color from the dropdown for the
// current page, and it will be saved as part of the extension's isolated
// storage. The chrome.storage API is used for this purpose. This is different
// from the window.localStorage API, which is synchronous and stores data bound
// to a document's origin. Also, using chrome.storage.sync instead of
// chrome.storage.local allows the extension data to be synced across multiple
// user devices.
// document.addEventListener('DOMContentLoaded', () => {
//   getCurrentTabUrl((url) => {
//     var dropdown = document.getElementById('dropdown');
//     alert(url);
//     // Load the saved background color for this page and modify the dropdown
//     // value, if needed.
//     getSavedBackgroundColor(url, (savedColor) => {
//       if (savedColor) {
//         changeBackgroundColor(savedColor);
//         dropdown.value = savedColor;
//       }
//     });

//     // Ensure the background color is changed and saved when the dropdown
//     // selection changes.
//     dropdown.addEventListener('change', () => {
//       changeBackgroundColor(dropdown.value);
//       saveBackgroundColor(url, dropdown.value);
//     });
//   });
// });

// function handleStateChange() {

// }
// title ssid FLID domain url


// document.addEventListener('DOMContentLoaded', function() {
//     var checkPageButton = document.getElementById('checkPage');
//     checkPageButton.addEventListener('click', function() {
  
//       chrome.tabs.getSelected(null, function(tab) {
//         d = document;
  
//         var f = d.createElement('form');
//         f.action = 'http://gtmetrix.com/analyze.html?bm';
//         f.method = 'post';
//         var i = d.createElement('input');
//         i.type = 'hidden';
//         i.name = 'url';
//         i.value = tab.url;
//         f.appendChild(i);
//         d.body.appendChild(f);
//         f.submit();
//       });
//     }, false);
//   }, false);