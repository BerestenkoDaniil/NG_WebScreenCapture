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


document.getElementById('canvas').onclick = function () {
    $(this).parent().attr('href', document.getElementById('canvas').toDataURL());
    $(this).parent().attr('download', "Screenshot.png");
};


//function dataURItoBlob(dataURI) {
//    const byteString = atob(dataURI.split(',')[1]);
//    const mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0]
//    const ab = new ArrayBuffer(byteString.length);
//    const ia = new Uint8Array(ab);
//    for (let i = 0; i < byteString.length; i++) {
//        ia[i] = byteString.charCodeAt(i);
//    }
//    const blob = new Blob([ab], { type: mimeString });
//    return blob;
//}
function ShanuSaveImage() {
    var m = confirm("Are you sure to Save ");
    if (m) {
        // generate the image data     
        var image_NEW = document.getElementById("canvas").toDataURL("image/png");
        image_NEW = image_NEW.replace('data:image/png;base64,', '');
        console.log(image_NEW);
        $.ajax({
            type: 'POST',
            url: '/Home/UploadImage',
            data: '{ "imageData" : "' + image_NEW + '" }',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                alert('Image saved to your root Folder !');
            }
        });
    }
}   