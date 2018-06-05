var File = {};

File.input = ".prompt #new-file-name";
File.files = null;
File.container = $("#files");

//Upload file to server using ajax
File.uploadFile = function () {
    var data = new FormData();
    var files = $(File.input).get(0).files;

    if (files.length > 0)
        data.append("UploadedFile", files[0]);

    var settings = {};

    settings.url = "api/files/uploadfile" + "?userId=" + userId + "&parentFolderId=" + currentFolderId;
    settings.contentType = false;
    settings.processData = false;
    settings.type = "POST";
    settings.data = data;
    settings.success = function () { File.getFilesFromServer(); };
    settings.error = function (e) { alert("File not uploaded"); console.log(e.responseText); console.log(e.statusText);};

    $.ajax(settings);
    remove();
};

//Makes ajax request to server and places them in folders variable
File.getFilesFromServer = function (parentFolderId) {
    parentFolderId = currentFolderId;

    var settings = {};

    settings.url = "/api/files/getall/";
    settings.data = { userId: userId, parentFolderId: parentFolderId };
    settings.success = function (data) {
        File.files = data;
        File.loadFiles();
    };

    settings.error = function () {
        alert("Error could not load folders!");
    };

    $.ajax(settings);

};

File.loadFiles = function () {

    var container = File.container;

    container.empty();

    if (File.files == null) {
        container.append($("<span>").text("NO Files").css("color", "red"));
    }
    else {
        for (var i = 0; i < File.files.length; i++) {
            var file = File.files[i];

            if (file.ParenFolderId != currentFolderId)
                continue;

            var div = $("<div>").css("border", "2px solid black").css("clear", "both").addClass("file").attr("file-id", file.Id).attr("file-name", file.Name);

            var Id = $("<span>").text("Id:" + file.Id).insertAfter($("<br>"));
            var Name = $("<span>").text("Name:" + file.Name).insertAfter($("<br>"));
            var CreatedOn = $("<span>").text("UploadedOn:" + file.UploadedOn).insertAfter($("<br>"));

            div.append(Id, Name, CreatedOn);
            container.append(div);
        };
    }
};