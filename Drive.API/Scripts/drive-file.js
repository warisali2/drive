var File = {};

File.input = "#new-file";
File.files = null;
File.container = $("#files");

//Upload file to server using ajax
File.uploadFile = function () {
    var data = new FormData();
    var files = $(File.input).get(0).files;

    if (files.length > 0)
        data.append("UploadedFile", files[0]);

    var settings = {};

    settings.url = "api/files/uploadfile" + "?userId=" + userId + "&parentFolderId=" + global.currentFolderId;
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
    parentFolderId = global.currentFolderId;

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

            if (file.ParenFolderId != global.currentFolderId)
                continue;

            var template = $("#file-card").html();
            var templateScript = Handlebars.compile(template);

            file.UploadedOn = file.UploadedOn.substring(0, 10);
            file.fileName = file.Name.substring(5) + file.FileExt;
            var div = templateScript(file);

            container.append(div);
        };
    }
};

File.getDisplayName = function (name) {
    return name.substring(name.indexOf("-") + 1);
};

File.deleteFile = function (id) {
    var settings = {};

    settings.url = "/api/files/delete";
    settings.data = { id: id };
    settings.success = function () {
        File.getFilesFromServer();
    };

    settings.error = function () {
        alert("Error could not delete file!");
    };

    $.ajax(settings);
};

File.downloadFile = function (id) {
    var url = "/api/files/downloadfile?id=" + id;
    window.open(url);
};