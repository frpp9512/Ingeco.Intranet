// obtain the badge that will be showed up when the file is uploading
var uploadIndicator = document.getElementById("upload-indicator");

// upload the selected file
$("#profilephoto").ajaxfileupload({
    'action': '/accounts/uploadtempuserphoto',
    'onStart': function () {
        // show the badge that indicates the file upload (removing the 'collapse' class)
        uploadIndicator.classList.remove("collapse");
    },
    'onComplete': function (response) {
        // get the path of the selected file, splitting it to get the filename and setting it up to the input label
        var filepath = $("#profilephoto").val();
        var pieces = filepath.split("\\");
        var filename = pieces[pieces.length - 1];
        $("#profile-pic-filename").html(filename);

        // set the profile picture file id the hidden input
        var fileIdInput = document.getElementById("ProfilePictureId");
        fileIdInput.setAttribute("value", response.fileId);

        // set the filename of the profile picture uploaded to the hidden input
        var fileNameInput = document.getElementById("ProfilePictureFileName");
        fileNameInput.setAttribute("value", filename);

        // Set the uploaded picture to the img
        var img = document.getElementById("profile-pic-preview");
        img.setAttribute("src", response.url);

        // hide the badge that indicates the file upload (adding the 'collapse' class)
        uploadIndicator.classList.add("collapse");
    }
});

// Allow to refresh the img
function RefreshProfilePic(imgElement, url) {
    var timestamp = new Date().getTime();
    var el = document.getElementById(imgElement);
    var queryString = "?t=" + timestamp;
    el.src = url + queryString;
}