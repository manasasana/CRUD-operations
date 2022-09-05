using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication6.Pages
{
    public class EditEmployeeModel : PageModel
    {
        public CreateEmpInfo emp = new CreateEmpInfo();
        public string errorMsg = "";
        public string successMsg = "";
        public void OnGet()
        {

            string strid = Request.Query["id"];
            int id=Convert.ToInt32(strid);
            try
            {
                string connectionstring = "Data Source=DESKTOP-1UU1KC8\\MSSQLSERVER1;Initial Catalog=student;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlConnection sqlconn = new SqlConnection(connectionstring);
                sqlconn.Open();
                string sqlstr = "select Name,DOB,BaseOffice,Phone,Designation,DOJ " +
                    "from EmpInfo where EmpId=@id";
               
                using (SqlCommand cmd = new SqlCommand(sqlstr, sqlconn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader()) 
                    {
                        if (reader.Read())
                        {

                            emp.name = reader.GetString(0);
                            emp.dob = reader.GetDateTime(1);
                          
                            emp.baseoff = reader.GetString(2);
                            emp.phone = reader.GetInt64(3);
                            emp.des = reader.GetString(4);
                            emp.dateofjoining = reader.GetDateTime(5);
                            emp.dateofjoining.GetDateTimeFormats();
                            Console.WriteLine(emp.name + emp.dob);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

       
        public void OnPost()
        {
           
            Console.WriteLine(Request.Query["id"]);
            emp.id = Convert.ToInt32(Request.Query["id"]);
            emp.name = Request.Form["name"];
            if (Request.Form["dob"].ToString().Length != 0)
            {
                emp.dob = Convert.ToDateTime(Request.Form["dob"]);
            }
            emp.baseoff = Request.Form["baseoff"];
            if (Request.Form["phone"].ToString().Length != 0)
            {
                emp.phone = Convert.ToInt64(Request.Form["phone"]);
            }
            emp.des = Request.Form["des"];
            if (Request.Form["doj"].ToString().Length != 0)
            {
                emp.dateofjoining = Convert.ToDateTime(Request.Form["doj"]);
            }
            Console.WriteLine(emp.name + emp.dob + "    "+ emp.id +"    " + emp.dateofjoining + emp.phone 
                + emp.baseoff);

             if (emp.id == 0 || emp.name.Length == 0 || emp.dob.ToString().Length == 0 ||

                 emp.baseoff.Length == 0 || emp.phone == 0 || emp.des.Length == 0 ||
                 emp.dateofjoining.ToString().Length == 0)
             {
                 errorMsg = "All fields are mandatory";
                 return;
             }



            try
            {
               
                string connectionstring = "Data Source=DESKTOP-1UU1KC8\\MSSQLSERVER1;Initial Catalog=student;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlConnection sqlconn = new SqlConnection(connectionstring);
                sqlconn.Open();
                string QueryString = "update EmpInfo set Name=@name,DOB=@dob,BaseOffice=@baseoff" +
                    ",Phone=@phone,Designation=@des,DOJ=@doj where EmpId=@id";
                Console.WriteLine(QueryString);
                using (SqlCommand cmd = new SqlCommand(QueryString, sqlconn))
                {
                   
                    cmd.Parameters.AddWithValue("@name", emp.name);
                    cmd.Parameters.AddWithValue("@dob", emp.dob);
                    cmd.Parameters.AddWithValue("@baseoff", emp.baseoff);
                    cmd.Parameters.AddWithValue("@phone", emp.phone);
                    cmd.Parameters.AddWithValue("@des", emp.des);
                    cmd.Parameters.AddWithValue("@doj", emp.dateofjoining);
                    cmd.Parameters.AddWithValue("@id", emp.id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            emp.id = 0;
            emp.name = "";
            emp.dob = DateTime.MinValue;
            emp.baseoff = "";
            emp.phone = 0;
            emp.des = "";
            emp.dateofjoining = DateTime.MinValue;
            successMsg = " Employee updated successfully";

            Response.Redirect("/Employee");


        }
        public class CreateEmpInfo
        {
            public int id;
            public string name;
            public DateTime dob;
            public string baseoff;
            public long phone;
            public string des;
            public DateTime dateofjoining;

        }


    }
}
