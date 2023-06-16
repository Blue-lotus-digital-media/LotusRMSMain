let qrCode = window.qrcode;

const video = document.createElement("video");
const imageForm = document.querySelector("#imageForm");
const fileInp = imageForm.querySelector("input");
const canvasElement = document.getElementById("qr-canvas");
const canvas = canvasElement.getContext("2d");


const btnScanQR = document.getElementById("btn-scan-qr");
let scanning = false;

btnScanQR.addEventListener("click", function () {
    navigator.mediaDevices
        .getUserMedia({ video: { facingMode: "environment" } })
        .then(function (stream) {
            scanning = true;
            btnScanQR.hidden = true;
            canvasElement.hidden = false;
            imageForm.hidden = false;
            video.setAttribute("playsinline", true); // required to tell iOS safari we don't want fullscreen
            video.srcObject = stream;
            video.play();
            tick();
            scan();
        });
    function tick() {
        canvas.drawImage(video, 0, 0, canvasElement.width, canvasElement.height);
        scanning && requestAnimationFrame(tick);
    }
    function scan() {
        try {
            qrCode.decode();
        } catch (e) {
            setTimeout(scan, 300);
        }
    }
    qrCode.callback = res => {
        if (res) {
            RedirectToAction(res);
            scanning = false;
            video.srcObject.getTracks().forEach(track => {
                track.stop();
            });

            canvasElement.hidden = true;
            btnScanQR.hidden = false;
            imageForm.hidden = true;
        }
    };

});

imageForm.addEventListener("click", () => fileInp.click());

fileInp.addEventListener("change", async e => {
    let file = e.target.files[0];
    if (!file) return;
    let formData = new FormData();
    formData.append('file', file);
    fetchRequest(file, formData);
});
function fetchRequest(file, formData) {
    fetch("https://api.qrserver.com/v1/read-qr-code/", {
        method: 'POST', body: formData
    }).then(res => res.json()).then(result => {
        result = result[0].symbol[0].data;
        if (!result) return;
        RedirectToAction(result);
    }).catch((err) => {
        console.log(err);
        console.log("Couldn't scan QR Code catch");
    });
}
function RedirectToAction(url) {
    if (isValidUrl(url)) {
        if (isMyDomain(url)) {
            console.log(url);
            window.location.href=url;
        } else { alert("Not a valid Qr for system 1.") }
    } else {
        alert("Not a valid Qr for system 2.");
    }
}
function isMyDomain(url) {
    let qrDomain = (new URL(url));
    let myDomain = window.location;
    console.log("main domain = " + myDomain.hostname);
    console.log("client domain = " + qrDomain.hostname);
    if (qrDomain.hostname.localeCompare(myDomain.hostname) == 0) {
        console.log("true");
        return true;
    } else {
        console.log("false");
        return false;
    }
}
const isValidUrl = urlString => {
    try {
        return Boolean(new URL(urlString));
    }
    catch (e) {
        return false;
    }
}
