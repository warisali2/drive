using Drive.DAL;
using Drive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Drive.API.Controllers
{
    public class FoldersController : ApiController
    {
        public List<Folder> GetAllOfUser(int id)
        {
            FolderDAO folderDAO = new FolderDAO();

            var folders = folderDAO.GetAll();
            var activeAndRootFolders = (from folder in folders
                                        where folder.IsActive == true && folder.ParentFolderId == -1 && folder.CreatedBy == id
                                        select folder).ToList<Folder>();

            return activeAndRootFolders;
        }
    }
}
