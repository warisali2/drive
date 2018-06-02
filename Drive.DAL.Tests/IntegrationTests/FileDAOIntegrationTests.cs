using Drive.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.DAL.Tests.IntegrationTests
{
    [TestFixture]
    [Category("IntegrationTests")]
    public class FileDAOIntegrationTests : IntegrationTestsBase
    {
        FileDAO dao = null;

        public FileDAOIntegrationTests() : base()
        {
            dao = new FileDAO(_conStringName);
        }

        public FileDAOIntegrationTests(String key) : base(key)
        {
            dao = new FileDAO(key);
        }


        [Test]
        public void GetAll__WithNoRows__ReturnsListOfZero()
        {
            var list = dao.GetAll();

            Assert.AreEqual(list.Count, 0);
        }

        [Test]
        public void GetAll__InsertOneRow__ReturnsListOfOne()
        {
            File file = new File();
            file.Name = "Test File";
            file.ParenFolderId = -1;
            file.IsActive = true;
            file.FileExt = ".txt";
            file.FileSizeInKB = 100;
            file.CreatedBy = 1;
            file.UploadedOn = DateTime.Now;

            file.Id = dao.Insert(file);

            var list = dao.GetAll();

            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void Insert__WithData__InsertsOneRow()
        {
            File file = new File();
            file.Name = "Test File";
            file.ParenFolderId = -1;
            file.IsActive = true;
            file.FileExt = ".txt";
            file.FileSizeInKB = 100;
            file.CreatedBy = 1;
            file.UploadedOn = DateTime.Now;

            file.Id = dao.Insert(file);

            var list = dao.GetAll();

            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void Insert__WithData__InsertsCorrectData()
        {
            File file = new File();
            file.Name = "Test File";
            file.ParenFolderId = -1;
            file.IsActive = true;
            file.FileExt = ".txt";
            file.FileSizeInKB = 100;
            file.CreatedBy = 1;
            file.UploadedOn = DateTime.Now.Truncate(TimeSpan.FromSeconds(1));

            file.Id = dao.Insert(file);

            var list = dao.GetAll();
            var dbFile = list[0];

            Assert.AreEqual(dbFile.Id, file.Id);
            Assert.AreEqual(dbFile.Name, file.Name);
            Assert.AreEqual(dbFile.ParenFolderId, file.ParenFolderId);
            Assert.AreEqual(dbFile.IsActive, file.IsActive);
            Assert.AreEqual(dbFile.FileSizeInKB, file.FileSizeInKB);
            Assert.AreEqual(dbFile.FileExt, file.FileExt);
            Assert.AreEqual(dbFile.CreatedBy, file.CreatedBy);
            Assert.AreEqual(dbFile.UploadedOn, file.UploadedOn);
        }

        [Test]
        public void Insert__WithIncorrectData__ReturnsNegOne()
        {
            File file = new File();
            file.ParenFolderId = -1;
            file.IsActive = true;
            file.FileExt = ".txt";
            file.FileSizeInKB = 100;
            file.CreatedBy = 1;
            file.UploadedOn = DateTime.Now;

            file.Id = dao.Insert(file);

            Assert.AreEqual(-1, file.Id);
        }

        [Test]
        public void Insert__WithCorrectData__ReturnsReturnsIdOfInsertedRecord()
        {
            File file = new File();
            file.Name = "Text File";
            file.ParenFolderId = -1;
            file.IsActive = true;
            file.FileExt = ".txt";
            file.FileSizeInKB = 100;
            file.CreatedBy = 1;
            file.UploadedOn = DateTime.Now.Truncate(TimeSpan.FromSeconds(1));

            file.Id = dao.Insert(file);

            var dbFile = dao.GetById(file.Id);

            Assert.AreEqual(dbFile.Id, file.Id);
            Assert.AreEqual(dbFile.Name, file.Name);
            Assert.AreEqual(dbFile.ParenFolderId, file.ParenFolderId);
            Assert.AreEqual(dbFile.IsActive, file.IsActive);
            Assert.AreEqual(dbFile.FileSizeInKB, file.FileSizeInKB);
            Assert.AreEqual(dbFile.FileExt, file.FileExt);
            Assert.AreEqual(dbFile.CreatedBy, file.CreatedBy);
            Assert.AreEqual(dbFile.UploadedOn, file.UploadedOn);
        }



        [Test]
        public void GetById__WithId__ReturnsResult()
        {
            File file = new File();
            file.Name = "Test File";
            file.ParenFolderId = -1;
            file.IsActive = true;
            file.FileExt = ".txt";
            file.FileSizeInKB = 100;
            file.CreatedBy = 1;
            file.UploadedOn = DateTime.Now.Truncate(TimeSpan.FromSeconds(1));

            file.Id = dao.Insert(file);

            var dbFile = dao.GetById(file.Id);

            Assert.AreEqual(dbFile.Id, file.Id);
            Assert.AreEqual(dbFile.Name, file.Name);
            Assert.AreEqual(dbFile.ParenFolderId, file.ParenFolderId);
            Assert.AreEqual(dbFile.IsActive, file.IsActive);
            Assert.AreEqual(dbFile.FileSizeInKB, file.FileSizeInKB);
            Assert.AreEqual(dbFile.FileExt, file.FileExt);
            Assert.AreEqual(dbFile.CreatedBy, file.CreatedBy);
            Assert.AreEqual(dbFile.UploadedOn, file.UploadedOn);
        }

        [Test]
        public void GetById__WithIncorrectId__ReturnsNull()
        {
            var file = dao.GetById(2);

            Assert.IsNull(file);
        }

        [Test]
        public void Update__WithCorrectData__UpdatesRecord()
        {
            File file = new File();
            file.Name = "Test File";
            file.ParenFolderId = -1;
            file.IsActive = true;
            file.FileExt = ".txt";
            file.FileSizeInKB = 100;
            file.CreatedBy = 1;
            file.UploadedOn = DateTime.Now.Truncate(TimeSpan.FromSeconds(1));

            file.Id = dao.Insert(file);

            var dbFile = dao.GetById(file.Id);
            dbFile.IsActive = false;

            var count = dao.Update(dbFile);
            var updatedFile = dao.GetById(dbFile.Id);

            Assert.AreEqual(1, count);
            Assert.AreEqual(false, updatedFile.IsActive);
        }

        [Test]
        public void Update__WithIncorrectData__ReturnsNegOne()
        {
            File file = new File();
            file.Name = "Test File";
            file.ParenFolderId = -1;
            file.IsActive = true;
            file.FileExt = ".txt";
            file.FileSizeInKB = 100;
            file.CreatedBy = 1;
            file.UploadedOn = DateTime.Now.Truncate(TimeSpan.FromSeconds(1));

            file.Id = dao.Insert(file);

            var dbFile = dao.GetById(file.Id);
            dbFile.Name = null;

            var count = dao.Update(dbFile);

            Assert.AreEqual(-1, count);
        }

        [Test]
        public void Delete__WithCorrectId__DeactivatesRecord()
        {
            File file = new File();
            file.Name = "Test File";
            file.ParenFolderId = -1;
            file.IsActive = true;
            file.FileExt = ".txt";
            file.FileSizeInKB = 100;
            file.CreatedBy = 1;
            file.UploadedOn = DateTime.Now.Truncate(TimeSpan.FromSeconds(1));

            file.Id = dao.Insert(file);


            var count = dao.Delete(file.Id);
            var updatedFile = dao.GetById(file.Id);

            Assert.AreEqual(1, count);
            Assert.AreEqual(false, updatedFile.IsActive);
        }

        [Test]
        public void Delete__WithIncorrectId__ReturnsNegOne()
        {
            var count = dao.Delete(123);

            Assert.AreEqual(-1, count);
        }
    }
}
