using AuthenticateMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace AuthenticateMVC.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            var dept = new List<String> {"Mechanical", "CSE", "EEE", "ECE", "Civil", "IT" };
            ViewBag.dept = dept;
            var sem = new List<int> { 1,2,3,4,5,6,7,8 };
            ViewBag.sem = sem;
            var year = new List<int> { 2020, 2021, 2022, 2023, 2024, 2025, 2026 };
            ViewBag.year = year;
            return View();
        }

        [HttpPost]
        public ActionResult Index(StudentDetail sd,HttpPostedFileBase file)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["TACV_DBEntities2"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "insert into StudentDetails (Name,Department,RollNo,Semester,SSLC,HSC,CGPA,Backlogs,Phonenumber,Emailid,Photo) values (@Name,@Department,@RollNo,@Semester,@SSLC,@HSC,@CGPA,@Backlogs,@Phonenumber,@Emailid,@Photo)";
            SqlCommand sqlcomm = new SqlCommand(sqlquery,sqlconn); 
            sqlconn.Open();
            sqlcomm.Parameters.AddWithValue("@Name", sd.Name);
            sqlcomm.Parameters.AddWithValue("@Department", sd.Department);
            sqlcomm.Parameters.AddWithValue("@RollNo", sd.RollNo);
            sqlcomm.Parameters.AddWithValue("@Semester", sd.Semester);
            sqlcomm.Parameters.AddWithValue("@SSLC", sd.SSLC);
            sqlcomm.Parameters.AddWithValue("@HSC", sd.HSC);
            sqlcomm.Parameters.AddWithValue("@CGPA", sd.CGPA);
            sqlcomm.Parameters.AddWithValue("@Backlogs", sd.Backlogs);
            sqlcomm.Parameters.AddWithValue("@Phonenumber", sd.Phonenumber);
            sqlcomm.Parameters.AddWithValue("@Emailid", sd.Emailid);
            if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string imgpath = Path.Combine(Server.MapPath("~/Files/"), filename);
                file.SaveAs(imgpath);
            }
            sqlcomm.Parameters.AddWithValue("@Photo", "~/Files/" + sd.RollNo);
            sqlcomm.ExecuteNonQuery();
            sqlconn.Close();
            ViewData["SuccessMessage"] = sd.Name+", Your details are saved Successfully !!";

            return View();
        }
    }
}