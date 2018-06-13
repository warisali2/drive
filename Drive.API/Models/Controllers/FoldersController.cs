using Drive.DAL;
using Drive.DAL.Extensions;
using Drive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PdfSharp;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System.Web;
using System.IO;
using MimeTypes;

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

        [HttpGet]
        public Object DownloadMeta(int id, int userid)
        {
            FolderDAO folderDAO = new FolderDAO();
            FileDAO fileDAO = new FileDAO();

            var allfolders = folderDAO.GetAll();
            var folders = (from folder in allfolders
                           where folder.CreatedBy == userid
                           select folder).ToList();

            var allfiles = fileDAO.GetAll();
            var files = (from file in allfiles
                         where file.CreatedBy == userid
                         select file).ToList();


            Folder currentFolder = null;

            if (id == -1)
            {
                currentFolder = new Folder();
                currentFolder.Id = -1;
            }
            else
            {
                currentFolder = (from folder in folders
                                 where folder.Id == id
                                 select folder).FirstOrDefault();

                if (currentFolder == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound); ;
                }
            }

            Document document = new Document();
            Section section = document.AddSection();

            TraverseFolders(currentFolder, ref folders, ref files, ref section);

            PdfDocumentRenderer pdf = new PdfDocumentRenderer(false, PdfSharp.Pdf.PdfFontEmbedding.Always);
            pdf.Document = document;

            pdf.RenderDocument();

            var rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");
            var uid = Guid.NewGuid() + ".pdf";
            var fileSavePath = Path.Combine(rootPath, uid);

            pdf.PdfDocument.Save(fileSavePath);

            HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.OK);

            byte[] fileStream = System.IO.File.ReadAllBytes(fileSavePath);

            System.IO.File.Delete(fileSavePath);

            System.IO.MemoryStream ms = new MemoryStream(fileStream);

            resp.Content = new ByteArrayContent(fileStream);
            resp.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");

            var type = MimeTypeMap.GetMimeType(".pdf");
            resp.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(type);
            resp.Content.Headers.ContentDisposition.FileName = "meta-data.pdf";
            return resp;
        }

        private void TraverseFolders(Folder currentFolder, ref List<Folder> folders, ref List<Entities.File> files, ref Section section)
        {
            //Get childs of current folder
            var childFoldersOfCurrentFolder = (from folder in folders
                                               where folder.ParentFolderId == currentFolder.Id
                                               select folder).ToList();
            var childFilesOfCurrentFolder = (from file in files
                                             where file.ParenFolderId == currentFolder.Id
                                             select file).ToList();

            //Insert meta info to section
            if(currentFolder.Id != -1)
                InsertMetaOf(currentFolder, folders, ref section);

            foreach (var file in childFilesOfCurrentFolder)
            {
                InsertMetaOf(file, folders, ref section);
            }

            //Traverse child folders
            foreach(var folder in childFoldersOfCurrentFolder)
            {
                TraverseFolders(folder, ref folders, ref files, ref section);
            }
        }

        private void InsertMetaOf(Folder inputFolder, List<Folder> folders, ref Section section)
        {
            var parentFolderName = (from folder in folders
                                    where folder.Id == inputFolder.ParentFolderId
                                    select folder.Name).FirstOrDefault() ?? "ROOT";

            section.AddParagraph("Name: " + inputFolder.Name);
            section.AddParagraph("Type: FOLDER");
            section.AddParagraph("Size: NONE");
            section.AddParagraph("Parent: " + parentFolderName);
            section.AddParagraph();
        }

        private void InsertMetaOf(Entities.File inputFile, List<Folder> folders, ref Section section)
        {
            var parentFolderName = (from folder in folders
                                    where folder.Id == inputFile.ParenFolderId
                                    select folder.Name).FirstOrDefault() ?? "ROOT";

            section.AddParagraph("Name: " + inputFile.Name + inputFile.FileExt);
            section.AddParagraph("Type: FILE");
            section.AddParagraph("Size: " + inputFile.FileSizeInKB + " KB");
            section.AddParagraph("Parent: " + parentFolderName);
            section.AddParagraph();
        }
    }
}
