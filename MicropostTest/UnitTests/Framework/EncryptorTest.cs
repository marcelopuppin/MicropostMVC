using MicropostMVC.Framework.Common;
using NUnit.Framework;

namespace MicropostTest.UnitTests.Framework
{
    [TestFixture]
    public class EncryptorTest
    {
        [Test]
        public void ComparingIfHashedPasswordsAreIdentical()
        {
            string password = "abcedfghijklmnopqrstuvwxyz0123456789";
            string salt = Encryptor.CreateSalt(4);
            string hashedPassword = Encryptor.CreateHashedPassword(password, salt);
            string hashingPasswordAgain = Encryptor.CreateHashedPassword(password, salt);
            Assert.That(hashingPasswordAgain, Is.EqualTo(hashedPassword));
        }

        [Test]
        public void ComparingIfHashedPasswordsWithAnotherSaltAreDifferent()
        {
            string password = "abcedfghijklmnopqrstuvwxyz0123456789";

            string salt1 = Encryptor.CreateSalt(4);
            string hashedPassword = Encryptor.CreateHashedPassword(password, salt1);

            string salt2 = Encryptor.CreateSalt(4);
            string hashingPasswordAgain = Encryptor.CreateHashedPassword(password, salt2);
            
            Assert.That(hashingPasswordAgain, Is.Not.EqualTo(hashedPassword));
        }
    }
}
