using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace BankingProject.Pages.Users
{
    public class FundTransferModel : PageModel
    {
       
        FundTransfer ft = new FundTransfer();
        

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
                int transaction_id = rnd.Next(1000);
               ft.transaction_id = transaction_id;
                TempData["transaction_id"] = ft.transaction_id;
                ft.sender_accno = Convert.ToInt64(Request.Form["sender_accno"]);
                ft.receiver_accno = Convert.ToInt64(Request.Form["receiver_accno"]);
                ft.amount = Convert.ToInt64(Request.Form["amount"]);
                ft.tx_time = Convert.ToDateTime(DateTime.Now);

                sqlCon.Open();

                SqlCommand cmd = new SqlCommand("transaction_history", sqlCon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@transaction_id", System.Data.SqlDbType.Int).Value = ft.transaction_id;
                cmd.Parameters.Add("@sender_accno", System.Data.SqlDbType.BigInt).Value = ft.sender_accno;
                cmd.Parameters.Add("@receiver_accno", System.Data.SqlDbType.BigInt).Value = ft.receiver_accno;
                cmd.Parameters.Add("@amount", System.Data.SqlDbType.BigInt).Value = ft.amount;
                cmd.Parameters.Add("@tx_time", System.Data.SqlDbType.SmallDateTime).Value = ft.tx_time;
                int res=cmd.ExecuteNonQuery();

                

                if (res==3)
                {
                    

                    TempData["success_message"] = "Your Transaction is Successful";
                    /*TempData["transaction_id"] = long(ft.transaction_id); ;
                    TempData["sender_accno"]=Convert.ToInt64(ft.sender_accno);
                    TempData["receiver_accno"] = Convert.ToInt64(ft.receiver_accno);
                    TempData["amount"] = Convert.ToInt64(ft.amount);
                    TempData["tx_time"] = Convert.ToDateTime(ft.tx_time);*/
                    return RedirectToPage("/Users/FundTransfer");



                }
                else
                {
                    TempData["error_message"] = "something went wrong , pls try after sometime";
                    
                }

                
                sqlCon.Close();
                return RedirectToPage("/Users/FundTransfer");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToPage("/Users/FundTransfer");
        }
        class FundTransfer
        {
            public int transaction_id;
            public long sender_accno, receiver_accno;
            public long amount;
            public DateTime tx_time;
        }
    }
}