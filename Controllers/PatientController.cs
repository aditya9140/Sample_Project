using Sample_webproject.Models;
using Sample_webproject.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample_webproject.Controllers
{
    public class PatientController : Controller
    {
       
      public ActionResult InsertUserDetails()
        {
            AT_Data_Access_Layer dbhandle = new AT_Data_Access_Layer();
            ModelState.Clear();
            return View(dbhandle.GetList());
        }
      


        public ActionResult AddPatient()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        public ActionResult AddPatient(Patient smodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PatientDataAccessLayer sdb = new PatientDataAccessLayer();
                    if (sdb.AddPatient(smodel))
                    {
                        ViewBag.Message = "Patient Details Added Successfully";
                        ModelState.Clear();
                    }
                }
                return RedirectToAction("InsertUserDetails");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult AddTestDetail()
        {
            return View();
        }

        // POST: AddTest/Create
        [HttpPost]
        public ActionResult AddTestDetail(Test smodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PatientDataAccessLayer sdb = new PatientDataAccessLayer();
                    if (sdb.AddTest(smodel))
                    {
                        ViewBag.Message = "Test Details Added Successfully";
                        ModelState.Clear();
                    }
                }
                return RedirectToAction("InsertUserDetails");
            }
            catch
            {
                return View();
            }
        }


      

    
    
      [HttpGet]
        public ActionResult Edit(int id)
        {
            PatientDataAccessLayer sdb = new PatientDataAccessLayer();
            return View(sdb.GetList().Find(smodel => smodel.ID == id));
        }


        [HttpPost]
        public ActionResult Edit(int id, Test smodel)
        {
            try
            {
                PatientDataAccessLayer sdb = new PatientDataAccessLayer();
                sdb.UpdateDetails(smodel);
                return RedirectToAction("InsertUserDetails");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            try
            {
                PatientDataAccessLayer sdb = new PatientDataAccessLayer();
                if (sdb.DeleteTest(id))
                {
                    ViewBag.AlertMsg = "Test Deleted Successfully";
                }
                return RedirectToAction("InsertUserDetails");
            }
            catch
            {
                return View();
            }
        }
    
    }


}
