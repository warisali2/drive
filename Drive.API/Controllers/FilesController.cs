﻿using Drive.DAL;
using Drive.DAL.Extensions;
using Drive.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Drive.API.Controllers
{
    public class FilesController : ApiController
    {
        [HttpPost]
        public Object UploadFile(int userId, int parentFolderId)
        {
            if(HttpContext.Current.Request.Files.Count > 0)
            {
                try
                {
                    foreach(var filename in HttpContext.Current.Request.Files.AllKeys)
                    {
                        HttpPostedFile postedFile = HttpContext.Current.Request.Files[filename];

                        if(postedFile != null)
                        {
                            var uniqueName = Guid.NewGuid().ToString().Substring(0, 4);

                            Entities.File file = new Entities.File();

                            file.Name = uniqueName + "-" + postedFile.FileName;
                            file.FileExt = Path.GetExtension(postedFile.FileName);
                            file.IsActive = true;
                            file.FileSizeInKB = (postedFile.ContentLength) / 1024;
                            file.UploadedOn = DateTime.Now;
                            file.CreatedBy = userId;
                            file.ParenFolderId = parentFolderId;

                            var rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");
                            var fileSavePath = Path.Combine(rootPath, file.Name);

                            postedFile.SaveAs(fileSavePath);
                            file.Save();
                        }
                    }
                    
                }
                catch(Exception)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError); ;
                }
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public List<Entities.File> GetAll(int userId, int parentFolderId = -1)
        {
            FileDAO fileDAO = new FileDAO();

            var files = fileDAO.GetAll();
            var activeAndRootFiles = (from file in files
                                        where file.IsActive == true && file.ParenFolderId == parentFolderId && file.CreatedBy == userId
                                        select file).ToList<Entities.File>();

            return activeAndRootFiles;
        }
    }
}