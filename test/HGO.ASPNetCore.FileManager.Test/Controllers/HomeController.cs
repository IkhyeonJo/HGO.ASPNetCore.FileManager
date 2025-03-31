using HGO.ASPNetCore.FileManager.Test.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HGO.ASPNetCore.FileManager.CommandsProcessor;
using HGO.ASPNetCore.FileManager.ViewComponentsModel;

namespace HGO.ASPNetCore.FileManager.Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileManagerCommandsProcessor _processor;

        public HomeController(IFileManagerCommandsProcessor processor)
        {
            _processor = processor;
        }

        public IActionResult Index()
        {
            FileManagerModel fileManagerModel = new FileManagerModel()
            {
                Id = "FM1",
                RootFolder = AppDomain.CurrentDomain.BaseDirectory,
                ApiEndPoint = Url.Action("HgoApi"),
                Config = new FileManagerConfig()
                { /*
           CompressionLevel = 9,
           StorageMaxSizeMByte = 10,
           DisabledFunctions = new List<string>()
            {
                // you can disable the following functions
                "Search",
               "CreateNewFolder",
               "CreateNewFile",
               "Delete",
               "Rename",
               "Zip",
               "Unzip",
               "Copy",
               "Cut",
               "EditFile",
               "Download",
               "GetFileContent",
               "Upload",
               "ToggleView",
               "Browse",
               "Reload",
               "Breadcrumb",
               "FoldersTree",
               "MenuBar",
               "ContextMenu",
               "FilePreview",
               "View"
            }
         */
                }
            };

            return View(fileManagerModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost, HttpGet]
        public async Task<IActionResult> HgoApi(string id, string command, string parameters, IFormFile file)
        {
            return await _processor.ProcessCommandAsync(id, command, parameters, file);
        }
    }
}
