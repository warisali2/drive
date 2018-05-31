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

        

    }
}
