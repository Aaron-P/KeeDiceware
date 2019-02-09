using KeeDiceware;
using KeePassLib.Cryptography;
using KeePassLib.Cryptography.PasswordGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeeDicewareTest
{
    [TestClass]
    public class KeeDicewareUnitTests
    {
        //[TestMethod]
        //public void TestMethod1()
        //{
        //    var gen = new KeeDicewareGen(null);
        //    var pwp = new PwProfile();
        //    var crs = new CryptoRandomStream(CrsAlgorithm.Salsa20, new byte[] { 0x00 });
        //    var password = gen.Generate(pwp, crs);
        //}

        //[TestMethod]
        //public void TestSettingsSerialize()
        //{
        //    var expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Settings xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Method>MinimumEntropy</Method><Size>128</Size></Settings>";
        //    var settings = new Settings();
        //    var data = settings.Serialize();
        //    //Assert.AreEqual(data, expected);
        //    var b = 3;
        //}

        //[TestMethod]
        //public void TestSettingsDeserialize()
        //{
        //    var data = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Settings xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Method>WordCount</Method><Methods><Method xsi:type=\"WordCount\"><Count>1</Count></Method><Method xsi:type=\"MinimumEntropy\"><Count>2</Count></Method><Method xsi:type=\"MinimumCharacters\"><Count>3</Count></Method></Methods><Separator> </Separator><MethodIncludeSeparator>false</MethodIncludeSeparator></Settings>";
        //    var settings = Settings.Deserialize(data);
        //    var a = 3;
        //    //Assert.AreEqual(settings.Method, _Method.MinimumEntropy);
        //    //Assert.AreEqual(settings.Size, (uint)128);
        //    //Assert.AreEqual(settings.Separator, "_");
        //    //Assert.AreEqual(settings.MethodIncludeSeparator, true);
        //}
    }
}
