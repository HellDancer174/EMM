using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMM_API.Controllers.FileControllers
{
    public class InstallFileController : Controller
    {
        // GET: InstallFile
        public ActionResult Index()
        {
            return View();
        }
        public FilePathResult GetInstallFile()
        {
            // Путь к файлу
            string file_path = Server.MapPath("~/Files/com.VI.EMM_2_0.apk");
            // Тип файла - content-type
            string file_type = "application/apk";
            // Имя файла - необязательно
            string file_name = "com.VI.EMM_2_0.apk";
            return File(file_path, file_type, file_name);
        }
    }
}