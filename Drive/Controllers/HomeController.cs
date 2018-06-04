using Drive.DAL;
using Drive.Entities;
using Drive.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drive.Controllers
{
    public class HomeController : Controller
    {
        [AuthorizedOnly]
        public ActionResult Index()
        {
            FolderDAO folderDAO = new FolderDAO();

            var folders = folderDAO.GetAll();
            var activeAndRootFolders = (from folder in folders
                                        where folder.IsActive == true && folder.ParentFolderId == -1 && folder.CreatedBy == SessionManager.User.Id
                                        select folder) as ICollection<Folder>;

            FileDAO fileDAO = new FileDAO();

            var files = fileDAO.GetAll();
            var activeAndRootFiles = (from file in files
                                      where file.IsActive == true && file.ParenFolderId == -1 && file.CreatedBy == SessionManager.User.Id
                                      select file) as ICollection<File>;

            ViewBag.files = activeAndRootFiles;
            ViewBag.folder = activeAndRootFolders;

            return View();
        }
    }
}