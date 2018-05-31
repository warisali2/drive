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
    }
}
