using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.DAL.Tests.IntegrationTests
{
    public class IntegrationTestsBase
    {
        protected String _conStringName = "SQL_SERVER_TEST_DB_CON_STRING";
        DbContext _context = null;

        public IntegrationTestsBase()
        {
            _context = CreateDbContext();
        }

        public IntegrationTestsBase(String key)
        {
            _conStringName = key;
            _context = CreateDbContext();
        }

        private DbContext CreateDbContext()
        {
            return new DbContext(_conStringName);
        }

        [SetUp]
        public void CleanUp()
        {
            var command = _context.CreateCommand();
            command.CommandText = "DELETE dbo.Files; DELETE dbo.Folder; DELETE dbo.Users";

            _context.ExecuteQuery(command);
        }

    }
}
