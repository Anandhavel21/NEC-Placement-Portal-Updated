using AuthenticateMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;

namespace AuthenticateMVC.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        TACV_DBEntities entity = new TACV_DBEntities();
        public ActionResult Index()
        {
           return View();
        }

        [HttpGet]
        public ActionResult add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult add(StudentDetail model)
        {
            string filename = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            string extension = Path.GetExtension(model.ImageFile.FileName);
            filename = model.Roll_No.ToString() + "Photo" + extension;
            model.Photo = "~/Files/" + filename;
            filename = Path.Combine(Server.MapPath("~/Files/"),filename);
            model.ImageFile.SaveAs(filename);
            using(TACV_DBEntities sd = new TACV_DBEntities())
            {
                sd.StudentDetails.Add(model);
                sd.SaveChanges();
            }
            ModelState.Clear();

            return View();
        }
    }
}