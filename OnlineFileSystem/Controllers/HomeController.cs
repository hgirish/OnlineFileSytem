using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;
using OnlineFileSystem.Helpers;
using OnlineFileSystem.Models;

namespace OnlineFileSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _rootPath;
        private string _currentLocation;
        private readonly IConfiguration<AppSettings> _appSettings;
        private readonly IDiObject _diObject;
        public HomeController(IConfiguration<AppSettings> appSettings, IDiObject diObject)
        {
            _appSettings = appSettings;
            _diObject = diObject;
            _rootPath = _appSettings.Settings.RootPath;
            if (string.IsNullOrWhiteSpace(_rootPath))
            {
                _rootPath = "~/Uploads";
            }
            _rootPath = HostingEnvironment.MapPath(_rootPath);
            if (string.IsNullOrWhiteSpace(_currentLocation))
            {
                _currentLocation = _rootPath;
            }
        }

        public ActionResult Index(string location)
        {
            _currentLocation = string.IsNullOrWhiteSpace(location) 
                ? _rootPath 
                : location;
            _currentLocation = _currentLocation.Replace("FileServerRoot", _rootPath);

            MakeRootDirectory();
            
            ViewBag.Message = string.Format("Root path: {0}, Current Location: {1}", _rootPath, _currentLocation);

            return View();
        }

        public ActionResult ShowCurrentLocation()
        {
            var locations = _currentLocation.Replace(_rootPath, "FileServerRoot").Split('\\');
            return PartialView(locations);
        }
        public ActionResult ShowFolderItems()
        {
            var items = _diObject.GetAllItemsInTheDirectory(_currentLocation);
            return PartialView(items);
        }
        private void MakeRootDirectory()
        {
            if (!Directory.Exists(_rootPath))
            {
                Directory.CreateDirectory(_rootPath);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
