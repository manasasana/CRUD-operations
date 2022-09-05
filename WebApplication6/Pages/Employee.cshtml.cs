using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication6.Pages
{
    public class EmployeeModel : PageModel
    {
        public List<EmpInfo> listofemployees = new List<EmpInfo>();
        public void OnGet()
        {
            try
            {
                string connectionstring = "Data Source=DESKTOP-1UU1KC8\\MSSQLSERVER1;Initial Catalog=student;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlConnection sqlconn = new SqlConnection(connectionstring);
                sqlconn.Open();
                //string QueryString = "fetchemp";
                SqlCommand cmd = new SqlCommand("fetchemp", sqlconn);
                //cmd.ExecuteNonQuery();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    EmpInfo db = new EmpInfo();
                    db.id = reader.GetInt32(0);
                    db.name = reader.GetString(1);
                    //Console.WriteLine(db.name);
                    db.doj = reader.GetDateTime(2);
                    db.stringdate = db.doj.ToShortDateString();
                    db.baseoff = reader.GetString(3);
                    db.des = reader.GetString(4);

                    listofemployees.Add(db);
                }
            }
                 catch (Exception ex)
            {
            }
        }
    }
    public class EmpInfo
    {
        public int id;
        public string name;
        public DateTime dob;
        public string stringdate;
        public string baseoff;
        public string des;
        public DateTime doj;

    }
}
