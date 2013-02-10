using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OnlineFileSystem.Helpers;
using OnlineFileSystem.Models;

namespace OnlineFileSystemTests
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class DiObjectGetItemsTests
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

        [TestCase("")]
        [TestCase("foo")]
        [TestCase(null)]
        public void GetAllItemsInTheDirectory_returns_error_for_nonexisten_directory(string folderPath)
        {
            if (!string.IsNullOrWhiteSpace(folderPath))
            {
                _diObject.DeleteFolder(folderPath,true);
            }
          var result =  _diObject.GetAllItemsInTheDirectory(folderPath);
            Assert.IsNull(result);

        }
        [TestCase("foo")]
        public void GetAllItemsInTheDirectory_returns_list(string folderPath)
        {
            folderPath = Path.Combine(baseLocation, folderPath);
            _diObject.CreateFolder(folderPath);
            CreateEmptyFile(Path.Combine(folderPath, "dummy1.txt"));
            CreateEmptyFile(Path.Combine(folderPath, "dummy2.txt"));
            var result = _diObject.GetAllItemsInTheDirectory(folderPath);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<FileDetail>>(result);
            Assert.AreEqual(2, result.Count());
            //foreach (var detail in result)
            //{
            //    Console.WriteLine(detail.FullPath);
            //}
            _diObject.DeleteFolder(folderPath, true);
        }

        public static void CreateEmptyFile(string filename)
        {
            File.Create(filename).Dispose();
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}