using Drive.DAL;
using Drive.DAL.Extensions;
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
        public List<Folder> GetAll(int userId, int parentFolderId = -1)
        {
            FolderDAO folderDAO = new FolderDAO();

            var folders = folderDAO.GetAll();
            var activeAndRootFolders = (from folder in folders
                                        where folder.IsActive == true && folder.ParentFolderId == parentFolderId && folder.CreatedBy == userId
                                        select folder).ToList<Folder>();

            return activeAndRootFolders;
        }

        [HttpGet]
        public void Save(string name, int userId, int parentFolderId = -1)
        {
            Folder folder = new Folder();
            folder.Name = name;
            folder.CreatedBy = userId;
            folder.ParentFolderId = parentFolderId;
            folder.IsActive = true;
            folder.CreatedOn = DateTime.Now.Truncate(TimeSpan.FromSeconds(1));

            folder.Save();
        }

        [HttpGet]
        public void Delete(int id)
        {
            FolderDAO dao = new FolderDAO();

            var folder = dao.GetById(id);
            folder.IsActive = false;
            folder.Update();

            FileDAO fileDAO = new FileDAO();
            var allfiles = fileDAO.GetAll();

            var files = (from file in allfiles
                         where file.ParenFolderId == id
                         select file).ToList<Entities.File>();

            foreach(var file in files)
            {
                file.IsActive = false;
                file.Update();
            }
        }

    }
}
