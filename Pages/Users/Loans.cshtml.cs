using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BankingProject.Pages.Users
{
    public class LoansModel : PageModel
    {
        public Loans l = new Loans();
        public void OnGet()
        {
        }
        public ActionResult OnPost()
        {
            try
            {
                string ConnectionString = "Data Source=INLPF3KG409\\SQLEXPRESS;Initial Catalog=banking_project;trusted_connection=true";
                SqlConnection sqlCon = new SqlConnection(ConnectionString);


                Random rnd = new Random();
                long loan_id = rnd.Next(10000);
                l.loan_id = loan_id;
                l.username = HttpContext.Session.GetString("username");
                l.fullname = Request.Form["fullname"];
                l.email_id = Request.Form["email_id"];
                l.phno = Convert.ToInt64(Request.Form["phno"]);
                l.age = Request.Form["age"];
                l.annual_income = Convert.ToInt64(Request.Form["annual_income"]);
                l.loan_type = Request.Form["loan_type"];
                l.years = Convert.ToInt32(Request.Form["years"]);
                l.amount = Convert.ToInt64(Request.Form["amount"]);
                l.status = "Pending";

                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("loans_procedure", sqlCon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@loan_id", System.Data.SqlDbType.BigInt).Value = l.loan_id;
                cmd.Parameters.Add("@fullname", System.Data.SqlDbType.VarChar).Value = l.fullname;
                cmd.Parameters.Add("@email_id", System.Data.SqlDbType.VarChar).Value = l.email_id;
                cmd.Parameters.Add("@phno", System.Data.SqlDbType.BigInt).Value = l.phno;
                cmd.Parameters.Add("@age", System.Data.SqlDbType.VarChar).Value = l.age;
                cmd.Parameters.Add("annual_income", System.Data.SqlDbType.BigInt).Value = l.annual_income;
                cmd.Parameters.Add("@loan_type", System.Data.SqlDbType.VarChar).Value = l.loan_type;
                cmd.Parameters.Add("@years", System.Data.SqlDbType.Int).Value = l.years;
                cmd.Parameters.Add("@loan_amount", System.Data.SqlDbType.BigInt).Value = l.amount;
                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar).Value = l.username;
                cmd.Parameters.Add("@status", System.Data.SqlDbType.VarChar).Value = l.status;


                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    TempData["success_msg"] = "Your request has been submitted to admin";
                    return RedirectToPage("/Users/Loans");
                }
                else
                {
                    TempData["error_msg"] = "something went wrong";
                }



                sqlCon.Close();






            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToPage("/Users/Loans");
        }
    }
    public class Loans
    {
        public long loan_id;
        public string fullname, email_id, username;
        public long phno;
        public string age;
        public string loan_type;
        public int years;
        public long annual_income;
        public long amount;
        public string status;



    }
}


