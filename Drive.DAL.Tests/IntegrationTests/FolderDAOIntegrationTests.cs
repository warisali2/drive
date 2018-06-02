using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drive.Entities;
using NUnit.Framework;

namespace Drive.DAL.Tests.IntegrationTests
{
    [TestFixture]
    [Category("IntegrationTests")]
    public class FolderDAOIntegrationTests : IntegrationTestsBase
    {
        FolderDAO dao = null;

        public FolderDAOIntegrationTests() : base()
        {
            dao = new FolderDAO(_conStringName);
        }

        public FolderDAOIntegrationTests(String key) : base(key)
        {
            dao = new FolderDAO(key);
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
            Folder folder = new Folder();
            folder.Name = "Test Folder";
            folder.ParentFolderId = -1;
            folder.IsActive = true;
            folder.CreatedBy = 1;
            folder.CreatedOn = DateTime.Now;

            folder.Id = dao.Insert(folder);

            var list = dao.GetAll();

            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void Insert__WithData__InsertsOneRow()
        {
            Folder folder = new Folder();
            folder.Name = "Test Folder";
            folder.ParentFolderId = -1;
            folder.IsActive = true;
            folder.CreatedBy = 1;
            folder.CreatedOn = DateTime.Now;

            folder.Id = dao.Insert(folder);

            var list = dao.GetAll();

            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void Insert__WithData__InsertsCorrectData()
        {
            Folder folder = new Folder();
            folder.Name = "Test Folder";
            folder.ParentFolderId = -1;
            folder.IsActive = true;
            folder.CreatedBy = 1;
            folder.CreatedOn = DateTime.Now.Truncate(TimeSpan.FromSeconds(1));

            folder.Id = dao.Insert(folder);

            var list = dao.GetAll();
            var dbFolder = list[0];

            Assert.AreEqual(dbFolder.Id, folder.Id);
            Assert.AreEqual(dbFolder.Name, folder.Name);
            Assert.AreEqual(dbFolder.ParentFolderId, folder.ParentFolderId);
            Assert.AreEqual(dbFolder.IsActive, folder.IsActive);
            Assert.AreEqual(dbFolder.CreatedBy, folder.CreatedBy);
            Assert.AreEqual(dbFolder.CreatedOn, folder.CreatedOn);
        }

        [Test]
        public void Insert__WithIncorrectData__ReturnsNegOne()
        {
            Folder folder = new Folder();
            folder.ParentFolderId = -1;
            folder.IsActive = true;
            folder.CreatedBy = 1;
            folder.CreatedOn = DateTime.Now;

            folder.Id = dao.Insert(folder);

            Assert.AreEqual(-1, folder.Id);
        }

        [Test]
        public void Insert__WithCorrectData__ReturnsReturnsIdOfInsertedRecord()
        {
            Folder folder = new Folder();
            folder.Name = "Text Folder";
            folder.ParentFolderId = -1;
            folder.IsActive = true;
            folder.CreatedBy = 1;
            folder.CreatedOn = DateTime.Now.Truncate(TimeSpan.FromSeconds(1));

            folder.Id = dao.Insert(folder);

            var dbFolder = dao.GetById(folder.Id);

            Assert.AreEqual(dbFolder.Id, folder.Id);
            Assert.AreEqual(dbFolder.Name, folder.Name);
            Assert.AreEqual(dbFolder.ParentFolderId, folder.ParentFolderId);
            Assert.AreEqual(dbFolder.IsActive, folder.IsActive);
            Assert.AreEqual(dbFolder.CreatedBy, folder.CreatedBy);
            Assert.AreEqual(dbFolder.CreatedOn, folder.CreatedOn);
        }



        [Test]
        public void GetById__WithId__ReturnsResult()
        {
            Folder folder = new Folder();
            folder.Name = "Test Folder";
            folder.ParentFolderId = -1;
            folder.IsActive = true;
            folder.CreatedBy = 1;
            folder.CreatedOn = DateTime.Now.Truncate(TimeSpan.FromSeconds(1));

            folder.Id = dao.Insert(folder);

            var dbFolder = dao.GetById(folder.Id);

            Assert.AreEqual(dbFolder.Id, folder.Id);
            Assert.AreEqual(dbFolder.Name, folder.Name);
            Assert.AreEqual(dbFolder.ParentFolderId, folder.ParentFolderId);
            Assert.AreEqual(dbFolder.IsActive, folder.IsActive);
            Assert.AreEqual(dbFolder.CreatedBy, folder.CreatedBy);
            Assert.AreEqual(dbFolder.CreatedOn, folder.CreatedOn);
        }

        [Test]
        public void GetById__WithIncorrectId__ReturnsNull()
        {
            var folder = dao.GetById(2);

            Assert.IsNull(folder);
        }

        [Test]
        public void Update__WithCorrectData__UpdatesRecord()
        {
            Folder folder = new Folder();
            folder.Name = "Test Folder";
            folder.ParentFolderId = -1;
            folder.IsActive = true;
            folder.CreatedBy = 1;
            folder.CreatedOn = DateTime.Now.Truncate(TimeSpan.FromSeconds(1));

            folder.Id = dao.Insert(folder);

            var dbFolder = dao.GetById(folder.Id);
            dbFolder.IsActive = false;

            var count = dao.Update(dbFolder);
            var updatedFolder = dao.GetById(dbFolder.Id);

            Assert.AreEqual(1, count);
            Assert.AreEqual(false, updatedFolder.IsActive);
        }

        [Test]
        public void Update__WithIncorrectData__ReturnsNegOne()
        {
            Folder folder = new Folder();
            folder.Name = "Test Folder";
            folder.ParentFolderId = -1;
            folder.IsActive = true;
            folder.CreatedBy = 1;
            folder.CreatedOn = DateTime.Now.Truncate(TimeSpan.FromSeconds(1));

            folder.Id = dao.Insert(folder);

            var dbFolder = dao.GetById(folder.Id);
            dbFolder.Name = null;

            var count = dao.Update(dbFolder);

            Assert.AreEqual(-1, count);
        }

        [Test]
        public void Delete__WithCorrectId__DeactivatesRecord()
        {
            Folder folder = new Folder();
            folder.Name = "Test Folder";
            folder.ParentFolderId = -1;
            folder.IsActive = true;
            folder.CreatedBy = 1;
            folder.CreatedOn = DateTime.Now.Truncate(TimeSpan.FromSeconds(1));

            folder.Id = dao.Insert(folder);


            var count = dao.Delete(folder.Id);
            var updatedFolder = dao.GetById(folder.Id);

            Assert.AreEqual(1, count);
            Assert.AreEqual(false, updatedFolder.IsActive);
        }

        [Test]
        public void Delete__WithIncorrectId__ReturnsZero()
        {
            var count = dao.Delete(-123);

            Assert.AreEqual(0, count);
        }
    }
}
