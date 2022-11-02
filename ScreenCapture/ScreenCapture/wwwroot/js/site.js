// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const videoElem = document.getElementById("video");
const logElem = document.getElementById("log");
const startElem = document.getElementById("start");
const stopElem = document.getElementById("stop");

// Options for getDisplayMedia()

const displayMediaOptions = {
    video: {
        cursor: "always"
    },
    audio: false
};

// Set event listeners for the start and stop buttons
startElem.addEventListener("click", (evt) => {
    startCapture();
}, false);

stopElem.addEventListener("click", (evt) => {
    stopCapture();
}, false);

console.log = (msg) => logElem.innerHTML += `${msg}<br>`;
console.error = (msg) => logElem.innerHTML += `<span class="error">${msg}</span><br>`;
console.warn = (msg) => logElem.innerHTML += `<span class="warn">${msg}<span><br>`;
console.info = (msg) => logElem.innerHTML += `<span class="info">${msg}</span><br>`;

async function startCapture() {
    logElem.innerHTML = "";

    try {
        videoElem.srcObject = await navigator.mediaDevices.getDisplayMedia(displayMediaOptions);
        dumpOptionsInfo();
    } catch (err) {
        console.error(`Error: ${err}`);
    }
}

function stopCapture(evt) {
    let tracks = videoElem.srcObject.getTracks();

    tracks.forEach((track) => track.stop());
    videoElem.srcObject = null;
}

function dumpOptionsInfo() {
    const videoTrack = videoElem.srcObject.getVideoTracks()[0];

    console.info("Track settings:");
    console.info(JSON.stringify(videoTrack.getSettings(), null, 2));
    console.info("Track constraints:");
    console.info(JSON.stringify(videoTrack.getConstraints(), null, 2));
}

// Send the drawn image to the server
$('#sendBtn').live('click', function () {
    var image = graph[0].toDataURL("image/png");
    image = image.replace('data:image/png;base64,', '');

    $.ajax({
        type: 'POST',
        url: '/Default.aspx/UploadImage',
        data: '{ "imageData" : "' + image + '" }',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (msg) {
            alert('Image sent!');
        }
    });
});