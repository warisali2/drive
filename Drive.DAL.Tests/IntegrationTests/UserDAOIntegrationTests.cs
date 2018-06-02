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
    public class UserDAOIntegrationTests : IntegrationTestsBase
    {
        UserDAO dao = null;

        public UserDAOIntegrationTests() : base()
        {
            dao = new UserDAO(_conStringName);
        }

        public UserDAOIntegrationTests(String key) : base(key)
        {
            dao = new UserDAO(key);
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
            User user = new User();
            user.Name = "Test User";
            user.Login = "user";
            user.Password = "123";
            user.Email = "user@user.com";

            user.Id = dao.Insert(user);

            var list = dao.GetAll();

            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void Insert__WithData__InsertsOneRow()
        {
            User user = new User();
            user.Name = "Test User";
            user.Login = "user";
            user.Password = "123";
            user.Email = "user@user.com";

            user.Id = dao.Insert(user);

            var list = dao.GetAll();

            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void Insert__WithData__InsertsCorrectData()
        {
            User user = new User();
            user.Name = "Test User";
            user.Login = "user";
            user.Password = "123";
            user.Email = "user@user.com";

            user.Id = dao.Insert(user);

            var list = dao.GetAll();
            var dbUser = list[0];

            Assert.AreEqual(dbUser.Id, user.Id);
            Assert.AreEqual(dbUser.Name, user.Name);
            Assert.AreEqual(dbUser.Login, user.Login);
            Assert.AreEqual(dbUser.Password, user.Password);
            Assert.AreEqual(dbUser.Email, user.Email);

        }

        [Test]
        public void Insert__WithIncorrectData__ReturnsNegOne()
        {
            User user = new User();
            user.Login = "user";
            user.Password = "123";
            user.Email = "user@user.com";


            user.Id = dao.Insert(user);

            Assert.AreEqual(-1, user.Id);
        }

        [Test]
        public void Insert__WithCorrectData__ReturnsReturnsIdOfInsertedRecord()
        {
            User user = new User();
            user.Name = "Text User";
            user.Login = "user";
            user.Password = "123";
            user.Email = "user@user.com";


            user.Id = dao.Insert(user);

            var dbUser = dao.GetById(user.Id);

            Assert.AreEqual(dbUser.Id, user.Id);
            Assert.AreEqual(dbUser.Name, user.Name);
            Assert.AreEqual(dbUser.Login, user.Login);
            Assert.AreEqual(dbUser.Password, user.Password);
            Assert.AreEqual(dbUser.Email, user.Email);

        }

        [Test]
        public void GetById__WithId__ReturnsResult()
        {
            User user = new User();
            user.Name = "Test User";
            user.Login = "user";
            user.Password = "123";
            user.Email = "user@user.com";


            user.Id = dao.Insert(user);

            var dbUser = dao.GetById(user.Id);

            Assert.AreEqual(dbUser.Id, user.Id);
            Assert.AreEqual(dbUser.Name, user.Name);
            Assert.AreEqual(dbUser.Login, user.Login);
            Assert.AreEqual(dbUser.Password, user.Password);
            Assert.AreEqual(dbUser.Email, user.Email);

        }

        [Test]
        public void GetById__WithIncorrectId__ReturnsNull()
        {
            var user = dao.GetById(2);

            Assert.IsNull(user);
        }

        [Test]
        public void Update__WithCorrectData__UpdatesRecord()
        {
            User user = new User();
            user.Name = "Test User";
            user.Login = "user";
            user.Password = "123";
            user.Email = "user@user.com";


            user.Id = dao.Insert(user);

            var dbUser = dao.GetById(user.Id);
            dbUser.Password = "123";

            var count = dao.Update(dbUser);
            var updatedUser = dao.GetById(dbUser.Id);

            Assert.AreEqual(1, count);
            Assert.AreEqual("123", updatedUser.Password);
        }

        [Test]
        public void Update__WithIncorrectData__ReturnsNegOne()
        {
            User user = new User();
            user.Name = "Test User";
            user.Login = "user";
            user.Password = "123";
            user.Email = "user@user.com";

            user.Id = dao.Insert(user);

            var dbUser = dao.GetById(user.Id);
            dbUser.Name = null;

            var count = dao.Update(dbUser);

            Assert.AreEqual(-1, count);
        }

        [Test]
        public void Delete__WithCorrectId__DeletesRecord()
        {
            User user = new User();
            user.Name = "Test User";
            user.Login = "user";
            user.Password = "123";
            user.Email = "user@user.com";

            user.Id = dao.Insert(user);

            var count = dao.Delete(user.Id);

            Assert.AreEqual(1, count);
        }

        [Test]
        public void Delete__WithIncorrectId__ReturnsZero()
        {
            var count = dao.Delete(-123);

            Assert.AreEqual(0, count);
        }
    }
}
