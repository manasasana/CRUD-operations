using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace WebApplication6.Pages
{
    public class studentModel : PageModel
    {
        public List<StudentInfo> listofstudents = new List<StudentInfo>();
        public void OnGet()
        {
            try
            {
                string connectionstring = "Data Source=DESKTOP-1UU1KC8\\MSSQLSERVER1;Initial Catalog=student;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlConnection sqlconn = new SqlConnection(connectionstring);
                sqlconn.Open();
                string QueryString = "select StdId,StdName,StdCourse from StudentInfo";
                SqlCommand cmd = new SqlCommand(QueryString, sqlconn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StudentInfo db = new StudentInfo();
                    db.id = reader.GetInt32(0);
                    db.name = reader.GetString(1);
                    Console.WriteLine(db.name);
                  
                    db.course = reader.GetString(2);
          
                    listofstudents.Add(db);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
    public class StudentInfo
    {
        public int id;
        public string name;
        public string course;
       
    }
}












