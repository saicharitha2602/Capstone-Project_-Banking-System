using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BankingProject.Pages.Users
{
    public class DepositModel : PageModel
    {
        public  Deposits d = new Deposits();
        
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
                long deposit_id = rnd.Next(10000);
                d.deposit_id=deposit_id;
                d.username= HttpContext.Session.GetString("username");
                d.fullname = Request.Form["fullname"];
                d.email_id = Request.Form["email_id"];
                d.phno = Convert.ToInt64(Request.Form["phno"]);
                d.deposit_type = Request.Form["deposit_type"];
                d.years = Convert.ToInt32(Request.Form["years"]);
                d.amount = Convert.ToInt64(Request.Form["amount"]);
                d.status = "Pending";

                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("deposits_procedure", sqlCon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@deposit_id", System.Data.SqlDbType.BigInt).Value = d.deposit_id;
                cmd.Parameters.Add("@fullname", System.Data.SqlDbType.VarChar).Value = d.fullname;
                cmd.Parameters.Add("@email_id", System.Data.SqlDbType.VarChar).Value = d.email_id;
                cmd.Parameters.Add("@phno", System.Data.SqlDbType.BigInt).Value = d.phno;
               cmd.Parameters.Add("@deposit_type",System.Data.SqlDbType.VarChar).Value = d.deposit_type;
                cmd.Parameters.Add("@years", System.Data.SqlDbType.Int).Value = d.years;
                cmd.Parameters.Add("@amount", System.Data.SqlDbType.BigInt).Value = d.amount;
                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar).Value = d.username;
                cmd.Parameters.Add("@status", System.Data.SqlDbType.VarChar).Value = d.status;


                int res=cmd.ExecuteNonQuery();
                if(res>0)
                {
                    TempData["success_msg"] = "Your request has been submitted to admin";
                    return RedirectToPage("/Users/Deposits");
                }
                else
                {
                    TempData["error_msg"] = "something went wrong,please try after sometime";
                }



                sqlCon.Close();
                 



                

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToPage("/Users/Deposits");
        }
    }
    public class Deposits
    {
        public long deposit_id;
        public string fullname, email_id,username;
        public long phno;
        public string deposit_type;
        public int years;
        public long amount;
        public string status;



    }
}
