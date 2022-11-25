let preview = document.getElementById("preview");
let capture = document.getElementById("capture");
let canvas = document.getElementById('canvas');
let upload = document.getElementById("upload");

// Request media
startButton.addEventListener("click", function () {
    navigator.mediaDevices.getDisplayMedia().then(stream => {
        // Grab frame from stream
        let track = stream.getVideoTracks()[0];
        let capture = new ImageCapture(track);
        capture.grabFrame().then(bitmap => {
            // Stop sharing
            track.stop();

            // Draw the bitmap to canvas
            canvas.width = bitmap.width;
            canvas.height = bitmap.height;
            canvas.getContext('2d').drawImage(bitmap, 0, 0);

            // Grab blob from canvas
            canvas.toBlob(blob => {
                // Do things with blob here
                console.log('output blob:', blob);

            });
        });
    })
});

upload.addEventListener("click", function () {
    html2canvas(document.body, {
        onrendered: function (canvas) {
            var mergedImage = canvas.toDataURL("image/png");
            mergedImage = mergedImage.replace('data:image/png;base64,', '');
            var param = { imageData: mergedImage };
            $http({
                method: 'POST',
                url: '/Home/UploadImage',
                data: JSON.stringify(param),
                dataType: 'JSON',
                headers: { 'content-type': 'application/json' }
            }).then(function (_response) {
                alert('Your photos successfully uploaded!');
            });
        }
    })
})