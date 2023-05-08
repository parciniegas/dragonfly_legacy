using System;
using Dragonfly.Core.Test.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Dragonfly.Core.Test.Extensions
{
    [TestClass]
    public class TestReflection
    {
        private User _user;

        [TestInitialize]
        public void Setup()
        {
            _user = new User
            {
                Id = 1,
                FirstName = "Jhon",
                LastName = "Doe",
                Password = "P2ssw0rd",
                Login = "jdoe"
            };
        }

        [TestMethod]
        public void TestEncrypt()
        {
            _user.Encrypt();
            Assert.AreEqual("P2ssw0rd".Encrypt(), _user.Password);
            _user.Decrypt();
            Assert.AreEqual("P2ssw0rd", _user.Password);
        }

        [TestMethod]
        public void TestRijndaelManagedEncryption()
        {
            var key = Guid.NewGuid().ToString();
            var testValue = JsonConvert.SerializeObject(_user);
            var encrypted = testValue.EncryptAes(key);
            var decrypted = encrypted.DecryptAes(key);
            Assert.AreEqual(testValue, decrypted);
        }
    }
}
