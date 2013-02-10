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
    public class DiObjectDeleteFolderTests
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
        public void DeleteFolder_not_allowed_without_basepath()
        {
            _diObject = new DiObject(null);
            var result = _diObject.DeleteFolder("foo", false);

        }
        [Test]
        public void DeleteFolder_returns_message_for_empty_path()
        {
            var result = _diObject.DeleteFolder("", false);

            Assert.AreEqual("No path provided", result);

        }
        [Test]
        public void DeleteFolder_returns_message_for_null_path()
        {
            var result = _diObject.DeleteFolder(null, false);

            Assert.AreEqual("No path provided", result);

        }
        [Test]
        public void DeleteFolder_throws_on_long_pathname()
        {
            string folderPath = "fooooaoaoaofsdflfadlf faslfslflf fldflf dflaf lf dflafldf dlfaldfl falf alfafooooaoaoaofsdflfadlf faslfslflf fldflf dflaf lf dflafldf dlfaldfl falf alfafooooaoaoaofsdflfadlf faslfslflf fldflf dflaf lf dflafldf dlfaldfl falf alfa";

            var newpath = Path.Combine(baseLocation, folderPath);
            var result = _diObject.DeleteFolder(newpath, false);

            Assert.False(Directory.Exists(newpath));
            var expectedString = string.Format("Failed to Delete folder, because path too long");
            Assert.AreEqual(expectedString, result);
        }

        [Test]
        public void DeleteFolder_fails_invalid_path()
        {
            string folderPath = "\\jafoo\\lafoos\\sbar";

            var newpath = Path.Combine(baseLocation, folderPath);
            var result = _diObject.DeleteFolder(newpath, false);
            Assert.False(Directory.Exists(newpath));
            var expectedString = string.Format("Path not allowed");
            Assert.AreEqual(expectedString, result);
        }
        [TestCase("foo")]
        [TestCase("bar")]
        public void Can_Delete_Folder(string folderPath)
        {
            var newpath = Path.Combine(baseLocation, folderPath);
            // first create folder
            _diObject.CreateFolder(newpath);

          string result =  _diObject.DeleteFolder(newpath, true);
            string expected = string.Format("The folder \"{0}\" was deleted successfully!", folderPath);
            Assert.AreEqual( expected, result);
            Assert.False(Directory.Exists(newpath));

        }

        [TestCase("foo")]
        [TestCase("bar")]
        public void Cannot_DeleteFolder_if_folder_not_empty(string folderPath)
        {
            var newpath = Path.Combine(baseLocation, folderPath);
            // first create folder
            _diObject.CreateFolder(newpath);
            _diObject.CreateFolder(Path.Combine(newpath, "foobar"));
            string result = _diObject.DeleteFolder(newpath, false);
            string expected = string.Format("Fail to Delete folder. The directory is not empty.\r\n");
            Assert.AreEqual(expected, result);
            Assert.True(Directory.Exists(newpath));

            Directory.Delete(newpath, true);
        }

        [TestCase("foo")]
        [TestCase("bar")]
        public void DeleteFolder_Fails_if_folder_does_not_exist(string folderPath)
        {
            var newpath = Path.Combine(baseLocation, folderPath);
            string result = _diObject.DeleteFolder(newpath, false);
            Assert.AreEqual("Failed to Delete folder, because directory does not exist", result);
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}