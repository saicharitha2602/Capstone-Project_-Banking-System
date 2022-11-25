using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BankingProject.Pages.Users
{
    public class MyLoansModel : PageModel
    {
        public List<LoanStatus> obj=new List<LoanStatus>();
        public void OnGet()
        {
            try
            {
                string user=HttpContext.Session.GetString("username");
                string ConnectionString = "Data Source=INLPF3KG409\\SQLEXPRESS;Initial Catalog=banking_project;trusted_connection=true";


                SqlConnection sqlCon = new SqlConnection(ConnectionString);
                sqlCon.Open();

                string query = "select loan_id,fullname,phno,loan_type,years,loan_amount,status from loans where username=@user ";


                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.Add("@user", System.Data.SqlDbType.VarChar).Value = user;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                   LoanStatus ls = new LoanStatus();
                    ls.loan_id = reader.GetInt64(0);
                    ls.fullname = reader.GetString(1);
                    ls.phno = reader.GetInt64(2);
                    ls.loan_type = reader.GetString(3);
                    ls.years = reader.GetInt32(4);
                    ls.loan_amount = reader.GetInt64(5);
                    ls.status = reader.GetString(6);

                    obj.Add(ls);


                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
    public class LoanStatus
    {
        public long loan_id;
        public string fullname;
        public long phno;
        public string loan_type;
        public int years;
        public long loan_amount;
        public string status;
    }
}
