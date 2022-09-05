using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication6.Pages
{
    public class CreateEmpModel : PageModel
    {
       public CreateEmpInfo emp = new  CreateEmpInfo();
        public string errorMsg = "";
        public string successMsg = "";
        public void OnGet()
        {

        }

        public void OnPost()
        {
            emp.id = Convert.ToInt32(Request.Form["id"]);
            emp.name = Request.Form["name"];
            if (Request.Form["dob"].ToString().Length != 0)
            {
                emp.dob = Convert.ToDateTime(Request.Form["dob"]);
            }
            emp.baseoff = Request.Form["baseoff"];
            if (Request.Form["phone"].ToString().Length!= 0)
            { 
                    emp.phone = Convert.ToInt64(Request.Form["phone"]);
             }
            emp.des = Request.Form["des"];
            if (Request.Form["doj"].ToString().Length != 0)
            {
                emp.dateofjoining = Convert.ToDateTime(Request.Form["doj"]);
            }

            if (emp.id == 0 || emp.name.Length == 0 || emp.dob.ToString().Length==0 ||
                emp.dob==DateTime.MinValue || emp.dateofjoining==DateTime.MinValue ||
                emp.baseoff.Length==0 || emp.phone==0 || emp.des.Length==0 ||
                emp.dateofjoining.ToString().Length==0)
            {
                errorMsg = "All fields are mandatory";
                return;
            }

            Console.WriteLine("dob is" + emp.dob.ToString());
            Console.WriteLine("doj is" + emp.dateofjoining.ToString());
            Console.WriteLine(" Minimum Date is" + DateTime.MinValue);

            try
            {
                string connectionstring = "Data Source=DESKTOP-1UU1KC8\\MSSQLSERVER1;Initial Catalog=student;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlConnection sqlconn = new SqlConnection(connectionstring);
                sqlconn.Open();
                string QueryString = "insert into EmpInfo values(@id,@name,@dob,@baseoff,@phone,@des,@doj)";
                using (SqlCommand cmd = new SqlCommand(QueryString, sqlconn))
                {
                    cmd.Parameters.AddWithValue("@id", emp.id);
                    cmd.Parameters.AddWithValue("@name", emp.name);
                    cmd.Parameters.AddWithValue("@dob", emp.dob);
                    cmd.Parameters.AddWithValue("@baseoff", emp.baseoff);
                    cmd.Parameters.AddWithValue("@phone", emp.phone);
                    cmd.Parameters.AddWithValue("@des", emp.des);
                    cmd.Parameters.AddWithValue("@doj", emp.dateofjoining);
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
            emp.dateofjoining= DateTime.MinValue;
            successMsg = "New Employee added successfully";

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
