var Folder = {};

Folder.folders = null;
Folder.container = $("#folders");

//Makes ajax request to server and places them in folders variable
Folder.getFoldersFromServer = function () {

    var settings = {};

    settings.url = "/api/folders/getallofuser/" + userId;

    settings.success = function (data) {
        Folder.folders = data;
    };

    settings.error = function () {
        alert("Error could not load folders!");
    };

    $.ajax(settings);

};

//Places folders from folders variable in the specified container
Folder.loadFolders = function () {

    var container = Folder.container;

    if (Folder.folders == null) {
        container.append($("<span>").text("NO FOLDERS").css("color", "red"));
    }
    else {
        Folder.folders.forEach(function (folder) {

            var div = $("<div>").css("border", "5px solid black").css("clear", "both");

            var Id = $("<span>").text("Id:" + folder.Id).insertAfter($("<br>"));
            var Name = $("<span>").text("Name:" + folder.Name).insertAfter($("<br>"));
            var CreatedOn = $("<span>").text("CreatedOn:" + folder.CreatedOn).insertAfter($("<br>"));

            div.append(Id, Name, CreatedOn);
        });
    }
};