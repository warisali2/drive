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
        [Test]
        public void GetAll__WithNoRows__ReturnsListOfZero()
        {
            FileDAO dao = new FileDAO();

            var list = dao.GetAll();

            Assert.AreEqual(list.Count, 0);
        }
    }
}
