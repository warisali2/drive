var Folder = {};

Folder.folders = null;
Folder.container = $("#folders");
Folder.breadcrumbsContainer = $("#breadcrumbs");

//Makes ajax request to server and places them in folders variable
Folder.getFoldersFromServer = function (parentFolderId) {
    parentFolderId = global.currentFolderId;
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

            if(folder.ParentFolderId != global.currentFolderId)
                continue;

            var template = $("#folder-card").html();
            var templateScript = Handlebars.compile(template);

            folder.CreatedOn = folder.CreatedOn.substring(0, 10);
            var div = templateScript(folder);

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
    var name = $("input[id=new-folder-name]").val();

    if (name == null || name == "") return false;

    Folder.saveFolder(name, global.currentFolderId);
    Folder.getFoldersFromServer();
};

Folder.deleteFolder = function (id) {
    var settings = {};

    settings.url = "/api/folders/delete";
    settings.data = { id: id };
    settings.success = function () {
        Folder.getFoldersFromServer();
    };

    settings.error = function () {
        alert("Error could not delete folder!");
    };

    $.ajax(settings);
};

Folder.navigateTo = function (folder) {
    $("#folder-actions").hide();
    $("#file-actions").hide();

    global.currentFolderId = $(folder).attr("folder-id");
    var name = $(folder).attr("folder-name");

    var bc = $("#breadcrumbs");

    bc.append($("<span>").text(" > " + name).addClass("breadcrumb-item mdl-typography--headline").attr("folder-id", global.currentFolderId));

    Folder.getFoldersFromServer();
};

Folder.toggleActionsFor = function (folder) {
    $(".selected").not(folder).removeClass("selected");

    $("#file-actions").hide();
    $("#folder-actions").show();
    $(folder).toggleClass("selected");

    if (!$(folder).hasClass("selected"))
        $("#folder-actions").hide();

    var id = $(folder).attr("folder-id");
    $("#folder-actions .delete").attr("obj-id", id).attr("obj-type", "folder");
};

Folder.downloadMeta = function (id) {
    var url = "/api/folders/downloadmeta?id=" + id + "&userId=" + userId;
    window.open(url);
};