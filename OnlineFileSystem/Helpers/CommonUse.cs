using System;

namespace OnlineFileSystem.Helpers
{
    public class CommonUse
    {
        public static string FormatFileSize(long fileLength)
        {
            if (fileLength < 0)
            {
                throw new ArgumentException("filelength cannot be negative");
            }
            if (fileLength < 1024)
            {
                 return string.Format("{0} bytes", fileLength);
            }
           var kbLength = fileLength/1024;
            if (kbLength < 1024)
            {
                 return string.Format("{0} KB", kbLength);
            }
            var mbLength = kbLength/1024;
            if (mbLength < 1024)
            {
                return string.Format("{0} MB", mbLength); 
            }
            var gbLength = mbLength/1024;
            if (gbLength < 1024)
            {
                return string.Format("{0} GB", gbLength); 
            }
            return string.Format("{0} TB", gbLength/1024);
        }
    }
}