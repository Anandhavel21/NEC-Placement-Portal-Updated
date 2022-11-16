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
            string mainconn = ConfigurationManager.ConnectionStrings["TACV_DBEntities"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "insert into StudentDetails (Name,Department,Roll_No,Academic_year,Semester,SSLC_Percentage,SSLC_Marksheet,HSC_Percentage,HSC_Marksheet,CGPA,Active_Backlogs,Phone_Number,Personal_Email,Address,Resume,Photo) values (@Name,@Department,@Roll_No,@Academic_year,@Semester,@SSLC_Percentage,@SSLC_Marksheet,@HSC_Percentage,@HSC_Marksheet,@CGPA,@Active_Backlogs,@Phone_Number,@Personal_Email,@Address,@Resume,@Photo)";
            SqlCommand sqlcomm = new SqlCommand(sqlquery,sqlconn); 
            sqlconn.Open();
            sqlcomm.Parameters.AddWithValue("@Name", sd.Name);
            sqlcomm.Parameters.AddWithValue("@Department", sd.Department);
            sqlcomm.Parameters.AddWithValue("@Roll_No", sd.Roll_No);
            sqlcomm.Parameters.AddWithValue("@Semester", sd.Semester);
            sqlcomm.Parameters.AddWithValue("@SSLC_Percentage", sd.SSLC_Percentage);
            sqlcomm.Parameters.AddWithValue("@HSC_Percentage", sd.HSC_Percentage);
            sqlcomm.Parameters.AddWithValue("@CGPA", sd.CGPA);
            sqlcomm.Parameters.AddWithValue("@Active_Backlogs", sd.Active_Backlogs);
            sqlcomm.Parameters.AddWithValue("@Phone_Number", sd.Phone_Number);
            sqlcomm.Parameters.AddWithValue("@Personal_Email", sd.Personal_Email);
            if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string imgpath = Path.Combine(Server.MapPath("~/Files/"), filename);
                file.SaveAs(imgpath);
            }
            sqlcomm.Parameters.AddWithValue("@PhotoS", "~/Files/" + sd.Roll_No + "Photo");
            sqlcomm.ExecuteNonQuery();
            sqlconn.Close();
            ViewData["SuccessMessage"] = sd.Name+", Your details are saved Successfully !!";

            return View();
        }
    }
}