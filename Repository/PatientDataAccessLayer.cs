using Sample_webproject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sample_webproject.Repository
{
    public class PatientDataAccessLayer
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["connectionDB"].ToString();
            con = new SqlConnection(constring);
        }
      
        public bool AddPatient(Patient patient)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddPatient", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@First_Name", patient.First_Name);
            cmd.Parameters.AddWithValue("@Last_Name", patient.Last_Name);
            cmd.Parameters.AddWithValue("@Age", patient.Age);
            cmd.Parameters.AddWithValue("@City", patient.City);
            cmd.Parameters.AddWithValue("@DOB", patient.DOB);
            cmd.Parameters.AddWithValue("@Admission_Date", patient.Admission_Date);
            cmd.Parameters.AddWithValue("@Hospital", patient.Hospital);
            cmd.Parameters.AddWithValue("@Discharge_Date", patient.Discharge_Date);
            cmd.Parameters.AddWithValue("@Total_Amount", patient.Total_Amount);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        //Add Test
        public bool AddTest(Test test)
        {
            connection();
            SqlCommand sqlCommand = new SqlCommand("sp_InsertUpdateDelete_Test", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Test_Name", test.Test_Name);
            sqlCommand.Parameters.AddWithValue("@Test_Date", test.Test_Date);
            sqlCommand.Parameters.AddWithValue("@Test_Price", test.Test_Price);
            sqlCommand.Parameters.AddWithValue("@Test_Result", test.Test_Result);
            sqlCommand.Parameters.AddWithValue("@Doctor_Remarks", test.Doctor_Remarks);
            sqlCommand.Parameters.AddWithValue("@Query", 1);

            con.Open();
            int i = sqlCommand.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
       public List<Test> GetList()
        {
            connection();
            List<Test> studentlist = new List<Test>();

            SqlCommand cmd = new SqlCommand("sp_InsertUpdateDelete_Test", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Query", 4);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                studentlist.Add(
                    new Test
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        Test_Name = Convert.ToString(dr["Test_Name"]),
                        Test_Date = Convert.ToDateTime(dr["Test_Date"]),
                        Test_Price = Convert.ToDecimal(dr["Test_Price"]),
                        Test_Result = Convert.ToString(dr["Test_Result"]),
                        Doctor_Remarks = Convert.ToString(dr["Doctor_Remarks"])
                    });
            }
            return studentlist;
        }
        public bool UpdateDetails(Test test)
        {
            connection();
            SqlCommand sqlCmd = new SqlCommand("sp_InsertUpdateDelete_Test", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", test.ID);
            sqlCmd.Parameters.AddWithValue("@Test_Name", test.Test_Name);
            sqlCmd.Parameters.AddWithValue("@Test_Date", test.Test_Date);
            sqlCmd.Parameters.AddWithValue("@Test_Price", test.Test_Price);
            sqlCmd.Parameters.AddWithValue("@Test_Result", test.Test_Result);
            sqlCmd.Parameters.AddWithValue("@Doctor_Remarks", test.Doctor_Remarks);
            sqlCmd.Parameters.AddWithValue("@Query", 2);

            con.Open();
            int i = sqlCmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }


        public bool DeleteTest(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("sp_InsertUpdateDelete_Test", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Query", 3);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
       
    }
}
