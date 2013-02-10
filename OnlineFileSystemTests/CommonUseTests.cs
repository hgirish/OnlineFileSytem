using System;
using NUnit.Framework;
using OnlineFileSystem.Helpers;

namespace OnlineFileSystemTests
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class CommonUseTests
    {
        
        [TestCase(0,"0 bytes")]
        [TestCase(1000, "1000 bytes")]
        [TestCase(1024, "1 KB")]
        [TestCase(2048,"2 KB")]
        [TestCase(1024 * 1024,"1 MB")]
        [TestCase(1024 * 1024 * 1024, "1 GB")]
        [TestCase(1099511627776, "1 TB")]
        [TestCase(10995116277760, "10 TB")]
        public void Fomrat_File_Size_returnsa_correct_string(long input, string expected)
        {
            var result = CommonUse.FormatFileSize(input);

            Assert.AreEqual(expected,result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void FomratFilSize_throws_on_negative()
        {
            CommonUse.FormatFileSize(-1);
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}