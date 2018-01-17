var record_now = document.getElementById("record_now");
record_now.onclick=function(){
    var xhr = new XMLHttpRequest();
    url = location.href;
    //xhr.onreadystatechange = handleStateChange; // Implemented elsewhere.
    xhr.open("POST", "http://127.0.0.1:5000/add?a="+url+"&b=2&c=3", true);
    xhr.send();
}
function handleStateChange() {

}
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