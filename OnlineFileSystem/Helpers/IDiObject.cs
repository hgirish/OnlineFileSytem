using System.Collections.Generic;
using OnlineFileSystem.Models;

namespace OnlineFileSystem.Helpers
{
    public interface IDiObject
    {
        string CreateFolder(string folderPath);
        string DeleteFolder(string folderPath, bool recursive);
        IEnumerable<FileDetail> GetAllItemsInTheDirectory(string folderPath);
    }
}