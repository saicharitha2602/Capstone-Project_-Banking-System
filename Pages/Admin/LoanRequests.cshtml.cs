using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BankingProject.Pages.Admin
{
    public class LoanRequestsModel : PageModel
    {
        public List<LoanRequests> obj = new List<LoanRequests>();
        public void OnGet()
        {
            try
            {
                string ConnectionString = "Data Source=INLPF3KG409\\SQLEXPRESS;Initial Catalog=banking_project;trusted_connection=true";
                SqlConnection sqlCon = new SqlConnection(ConnectionString);

                sqlCon.Open();

                string query = "select loan_id,fullname,email_id,phno,age,annual_income,loan_type,years,loan_amount,status from loans where status='pending' ";

                SqlCommand cmd = new SqlCommand(query, sqlCon);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    LoanRequests lr = new LoanRequests();
                    lr.loan_id = reader.GetInt64(0);
                    lr.fullname = reader.GetString(1);
                    lr.email_id = reader.GetString(2);
                    lr.phno = reader.GetInt64(3);
                    lr.age = reader.GetString(4);
                    lr.annual_income = reader.GetInt64(5);
                    lr.loan_type = reader.GetString(6);
                    lr.years = reader.GetInt32(7);
                    lr.loan_amount = reader.GetInt64(8);
                    lr.status = reader.GetString(9);

                    obj.Add(lr);


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
        public ActionResult OnPostUpdateLoans(long loan_id)
        {
            try
            {
                long id = loan_id;
                string temp = Request.Form["description"];

                string ConnectionString = "Data Source=INLPF3KG409\\SQLEXPRESS;Initial Catalog=banking_project;trusted_connection=true";
                SqlConnection sqlCon = new SqlConnection(ConnectionString);

                sqlCon.Open();

                string query = "update loans set status=@temp where loan_id=@id";
                SqlCommand cmd = new SqlCommand(query, sqlCon);

                cmd.Parameters.Add("@id", System.Data.SqlDbType.BigInt).Value = id;
                cmd.Parameters.Add("@temp", System.Data.SqlDbType.VarChar).Value = temp;

                int res = cmd.ExecuteNonQuery();
                Console.WriteLine(loan_id);
                Console.WriteLine(temp);
                Console.WriteLine(res);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //Console.WriteLine(temp);
            return RedirectToPage("/Admin/Dashboard");
        }
    }

    public class LoanRequests
    {
        public long loan_id;
        public string fullname;
        public string email_id;
        public long phno;
        public string age;
        public long annual_income;
        public string loan_type;
        public int years;
        public long loan_amount;
        public string status;

    }
}
