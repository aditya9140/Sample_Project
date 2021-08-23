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
        public ActionResult InsertUserDetails()
         {
            string connection = ConfigurationManager.ConnectionStrings["connectionDB"].ConnectionString;
            Test objuser = new Test();
            DataSet ds = new DataSet();
                 using (SqlConnection con = new SqlConnection(connection))
                 {
                  SqlCommand cmd = new SqlCommand("Select * from Test", con);
                
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
               
                    da.Fill(ds);
                    List<Test> userlist = new List<Test>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Test uobj = new Test();
                        uobj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                        uobj.Test_Name = ds.Tables[0].Rows[i]["Test_Name"].ToString();
                        uobj.Test_Date = Convert.ToDateTime(ds.Tables[0].Rows[i]["Test_Date"].ToString());
                        uobj.Test_Price = Convert.ToDecimal(ds.Tables[0].Rows[i]["Test_Price"].ToString());
                        uobj.Test_Result = ds.Tables[0].Rows[i]["Test_Result"].ToString();
                        uobj.Doctor_Remarks = ds.Tables[0].Rows[i]["Doctor_Remarks"].ToString();
                        userlist.Add(uobj);
                    }
                    objuser.usersinfo = userlist;
                
                con.Close();
            }
            return View(objuser);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            string connection = ConfigurationManager.ConnectionStrings["connectionDB"].ConnectionString;

            Test test = new Test();
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                string query = "Select * from Test where ID=@ID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@Id", id);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count == 1)
            {
                test.ID = Convert.ToInt32(dtbl.Rows[0][0].ToString());
                test.Test_Name = dtbl.Rows[0][1].ToString();
                test.Test_Date = Convert.ToDateTime(dtbl.Rows[0][2].ToString());
                test.Test_Price = Convert.ToDecimal(dtbl.Rows[0][3].ToString());
                test.Test_Result = dtbl.Rows[0][4].ToString();
                test.Doctor_Remarks = dtbl.Rows[0][5].ToString();
                return View(test);
            }
            else
                return RedirectToAction("InsertUserDetails");

        }

       
        [HttpPost]
        public ActionResult Edit(Test test)
        {
            string connection = ConfigurationManager.ConnectionStrings["connectionDB"].ConnectionString;
            test.Test_Date = Convert.ToDateTime(test.Test_Date);
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("sp_InsertUpdateDelete_Test", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@ID", test.ID);
                sqlCmd.Parameters.AddWithValue("@Test_Name", test.Test_Name);
                sqlCmd.Parameters.AddWithValue("@Test_Date", test.Test_Date);
                sqlCmd.Parameters.AddWithValue("@Test_Price", test.Test_Price);
                sqlCmd.Parameters.AddWithValue("@Test_Result", test.Test_Result);
                sqlCmd.Parameters.AddWithValue("@Doctor_Remarks", test.Doctor_Remarks);
                sqlCmd.Parameters.AddWithValue("@Query", 2);
                sqlCmd.ExecuteNonQuery();



            }
            return RedirectToAction("InsertUserDetails");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            string connection = ConfigurationManager.ConnectionStrings["connectionDB"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                string query = "Delete from Test where ID=@ID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ID", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("InsertUserDetails");
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
