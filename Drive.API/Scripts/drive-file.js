var File = {};

File.input = "#new-file-name";

File.uploadFile = function () {
    var data = new FormData();
    var files = $(".prompt #new-file-name").get(0).files;

    if (files.length > 0)
        data.append("UploadedFile", files[0]);

    var settings = {};

    settings.url = "api/files/uploadfile" + "?userId=" + userId + "&parentFolderId=" + currentFolderId;
    settings.contentType = false;
    settings.processData = false;
    settings.type = "POST";
    settings.data = data;
    settings.success = function () { alert("File uploaded"); };
    settings.error = function (e) { alert("File not uploaded"); console.log(e.responseText); console.log(e.statusText);};

    $.ajax(settings);
};