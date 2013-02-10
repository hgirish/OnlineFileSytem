using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OnlineFileSystem.Helpers;

namespace OnlineFileSystemTests
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class DiObjectTests
    {
        private string baseLocation;
        private DiObject _diObject;
        [SetUp]
        public void Setup()
        {
            baseLocation = AppDomain.CurrentDomain.BaseDirectory;
            _diObject = new DiObject(baseLocation);
            // Console.WriteLine(baseLocation);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void createfolder_not_allowed_without_basepath()
        {
            _diObject = new DiObject(null);
            var result = _diObject.CreateFolder("foo");

        }
        [Test]
        public void CrateFolder_returns_message_for_empty_path()
        {
            var result = _diObject.CreateFolder("");

            Assert.AreEqual("No path provided", result);

        }
        [Test]
        public void CrateFolder_returns_message_for_null_path()
        {
            var result = _diObject.CreateFolder(null);

            Assert.AreEqual("No path provided", result);

        }

        [Test]
        public void CreateFolder_Creates_folder()
        {

            var newpath = Path.Combine(baseLocation, "foo");
            var result = _diObject.CreateFolder(newpath);
            Console.WriteLine("newpath: {0}", newpath);
            Assert.True(Directory.Exists(newpath));
            var expectedString = string.Format("The folder \"{0}\" was created successfully!",
                "foo");
            Assert.AreEqual(expectedString, result);
            Directory.Delete(newpath,true);
        }

        [Test]
        public void CreateFolder_throws_on_long_pathname()
        {
            string folderPath = "fooooaoaoaofsdflfadlf faslfslflf fldflf dflaf lf dflafldf dlfaldfl falf alfafooooaoaoaofsdflfadlf faslfslflf fldflf dflaf lf dflafldf dlfaldfl falf alfafooooaoaoaofsdflfadlf faslfslflf fldflf dflaf lf dflafldf dlfaldfl falf alfa";

            var newpath = Path.Combine(baseLocation, folderPath);
            var result = _diObject.CreateFolder(newpath);

            Assert.False(Directory.Exists(newpath));
            var expectedString = string.Format("Failed to create folder, because path too long");
            Assert.AreEqual(expectedString, result);
        }

        [Test]
        public void CreateFolder_fails_invalid_path()
        {
            string folderPath = "\\jafoo\\lafoos\\sbar";

            var newpath = Path.Combine(baseLocation, folderPath);
            var result = _diObject.CreateFolder(newpath);
            Assert.False(Directory.Exists(newpath));
            var expectedString = string.Format("Path not allowed");
            Assert.AreEqual(expectedString, result);
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}