using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using OnlineFileSystem.Models;

namespace OnlineFileSystem.Helpers
{
    public class DiObject : IDiObject
    {
        private static string _basepath;

        public DiObject(string basepath)
        {
            if (string.IsNullOrWhiteSpace(basepath))
            {
                throw new ArgumentNullException("basepath");
            }
            _basepath = basepath;
            if (_basepath.StartsWith("~"))
            {
                _basepath = HostingEnvironment.MapPath(_basepath);
            }
           // Console.WriteLine("basepath: {0}", _basepath);
        }

        public string CreateFolder(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
            {
                return "No path provided";
            }
            if (!(folderPath.ToLower().StartsWith(_basepath.ToLower())))
            {
                Console.WriteLine("folderpath: {0}", folderPath);
                return "Path not allowed";
            }

            string folderName;
            try
            {
                DirInformation = new DirectoryInfo(folderPath);
                folderName = DirInformation.Name;
                if (folderName.Length > 248)
                {
                    return "Failed to create folder, because path too long";
                }

                Directory.CreateDirectory(folderPath);
            }
            catch (PathTooLongException)
            {
                return "Failed to create folder, because path too long";
            }
            return string.Format("The folder \"{0}\" was created successfully!",
                folderName);
        }

        protected static DirectoryInfo DirInformation { get; set; }

        public string DeleteFolder(string folderPath, bool recursive)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
            {
                return "No path provided";
            }
            if (!(folderPath.ToLower().StartsWith(_basepath.ToLower())))
            {
                Console.WriteLine("folderpath: {0}", folderPath);
                return "Path not allowed";
            }
            string folderName;

            try
            {

                DirInformation = new DirectoryInfo(folderPath);
                folderName = DirInformation.Name;
                Directory.Delete(folderPath, recursive);
            }
            catch (PathTooLongException)
            {

                return "Failed to Delete folder, because path too long";
            }
            catch (DirectoryNotFoundException)
            {
                return "Failed to Delete folder, because directory does not exist";
            }
            catch (IOException ex)
            {
                return string.Format("Fail to Delete folder. {0}", ex.Message);
            }

            return string.Format(
               "The folder \"{0}\" was deleted successfully!", folderName);
        }

        public IEnumerable<FileDetail> GetAllItemsInTheDirectory(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                return null;
            }
            var subFolders = Directory.GetDirectories(folderPath);
            var list = subFolders.Select(subFolderPath =>
                new DirectoryInfo(subFolderPath)).Select(subFolder =>
                    new FileDetail
                    {
                        Type = "Folder",
                        Name = subFolder.Name,
                        Size = "",
                        UploadTime = subFolder.CreationTime.ToString(CultureInfo.InvariantCulture),
                        Location = subFolder.Parent != null ? subFolder.Parent.FullName : "",
                        FullPath = subFolder.FullName
                    }).ToList();

            var files = Directory.GetFiles(folderPath);
            list.AddRange(files.Select(filePath =>
                new FileInfo(filePath)).Select(fle =>
                    new FileDetail
                    {
                        Type = fle.Name.LastIndexOf('.') < 0 ? "" : fle.Name.Substring(fle.Name.LastIndexOf('.')).ToUpper(),
                        Name = fle.Name,
                        Size = CommonUse.FormatFileSize(fle.Length),
                        UploadTime = fle.CreationTime.ToString(CultureInfo.InvariantCulture),
                        Location = fle.DirectoryName,
                        FullPath = fle.FullName
                    }));

            return list;
        }
    }
}