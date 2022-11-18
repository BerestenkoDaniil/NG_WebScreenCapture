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

//$("#FileDownloadBtn").click(function () {
//    event.preventDefault();
//    var rootPath = '@Url.Content("~")';
//    $.ajax({
//        type: "post",
//        url: rootpath + "/RequestFormEdit?handler=FileDownload",
//        data: { filename: this.value}
//        success: function (data) {

//        }

//    })
//})
upload.addEventListener("click",function () {
    var fileUpload = $("canvas");
    var files = fileUpload.files;
    var my_canvas = document.getElementById('canvas'),
        context = my_canvas.getContext("2d");
    var img = canvas.toDataURL("image/png");
    console.log(img);
    // Create  a FormData object
    var fileData = new FormData();
    fileData.append(img);
    $.ajax({
        url: '/Home/Upload', //URL to upload files 
        type: "POST", //as we will be posting files and other method POST is used
        data: fileData,
        success: function (result) {
            alert(result);
        },
        error: function (err) {
            alert(err.statusText);
        }
    });
})