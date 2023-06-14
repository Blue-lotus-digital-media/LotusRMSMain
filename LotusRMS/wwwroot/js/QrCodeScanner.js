
const searchBtn = document.querySelector("#btn-scan-qr");


searchBtn.addEventListener("click", () => {
    console.log("avv");
    html5QrcodeScanner.render(onScanSuccess);
});

const isValidUrl = urlString => {
    try {
        return Boolean(new URL(urlString));
    }
    catch (e) {
        return false;
    }
}
function onScanSuccess(decodedText, decodedResult) {
    // Handle on success condition with the decoded text or result.
    console.log(`Scan result: ${decodedText}`, decodedResult);
  

    // ...
    html5QrcodeScanner.clear();
    // ^ this will stop the scanner (video feed) and clear the scan area.
   
    if (isValidUrl(decodedText)) {
        window.location=decodedText;
    }
}
function onScanError(errorMessage) {
    // handle on error condition, with error message
}

var html5QrcodeScanner = new Html5QrcodeScanner(
    "reader", { fps: 10, qrbox: 500 });
//html5QrcodeScanner.render(onScanSuccess)