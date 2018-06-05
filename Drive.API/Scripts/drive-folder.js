var Folder = {};

Folder.folders = null;
Folder.container = $("#folders");
Folder.breadcrumbsContainer = $("#breadcrumbs");

//Makes ajax request to server and places them in folders variable
Folder.getFoldersFromServer = function (parentFolderId) {
    parentFolderId = currentFolderId;
    var settings = {};

    settings.url = "/api/folders/getall/";
    settings.data = { userId: userId, parentFolderId: parentFolderId};
    settings.success = function (data) {
        Folder.folders = data;
        Folder.loadFolders();
        File.getFilesFromServer();
    };

    settings.error = function () {
        alert("Error could not load folders!");
    };

    $.ajax(settings);

};

//Places folders from folders variable in the specified container
Folder.loadFolders = function () {
    
    var container = Folder.container;

    container.empty();

    if (Folder.folders == null) {
        container.append($("<span>").text("NO FOLDERS").css("color", "red"));
    }
    else {
        for(var i = 0; i < Folder.folders.length; i++){
            var folder = Folder.folders[i];

            if(folder.ParentFolderId != currentFolderId)
                continue;

            var div = $("<div>").css("border", "2px solid black").css("clear", "both").addClass("folder").attr("folder-id", folder.Id).attr("folder-name", folder.Name);

            var Id = $("<span>").text("Id:" + folder.Id).insertAfter($("<br>"));
            var Name = $("<span>").text("Name:" + folder.Name).insertAfter($("<br>"));
            var CreatedOn = $("<span>").text("CreatedOn:" + folder.CreatedOn).insertAfter($("<br>"));

            div.append(Id, Name, CreatedOn);
            container.append(div);
        };
    }
};

//Saves folder by making appropriate ajax request
Folder.saveFolder = function (name, parentFolderId) {

    var settings = {};

    settings.url = "/api/folders/save";
    settings.type = "GET";
    settings.data = { name: name, userId: userId, parentFolderId: parentFolderId };
    settings.success = function () {
        Folder.getFoldersFromServer();
        return true;
    };

    settings.error = function (e) {
        alert("Error could not save folder!");
        alert(e.responseText);
    };

    $.ajax(settings);
}

Folder.addNewFolder = function () {
    var name = $(".prompt input[id=new-folder-name]").val();

    if (name == null || name == "") return false;

    Folder.saveFolder(name, currentFolderId);
    Folder.getFoldersFromServer();
    remove();
};

Folder.updateBreadcrumbs = function () {
    var container = Folder.breadcrumbsContainer;

    var currentId = currentFolderId;

    while(currentId != -1)
    {
        
    }
}