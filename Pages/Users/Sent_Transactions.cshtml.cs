using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BankingProject.Pages.Users
{
    public class TransactionsModel : PageModel
    {
        public List<Transactions_info> list_name = new List<Transactions_info>();

        Account_No ac=new Account_No();
        public void OnGet()
        {
            try
            {

                string ConnectionString = "Data Source=INLPF3KG409\\SQLEXPRESS;Initial Catalog=banking_project;trusted_connection=true";
                
                
                SqlConnection sqlCon = new SqlConnection(ConnectionString);
                sqlCon.Open();


                //string temp = Request.Form["acc_no"];

                ac.acc_no = Convert.ToInt64(Request.Query["acc_no"]);
                SqlCommand cmd = new SqlCommand("sent_transactions", sqlCon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
               cmd.Parameters.Add("@acc_no", System.Data.SqlDbType.BigInt).Value =ac.acc_no;

                cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                 while (reader.Read())
                 {
                     Transactions_info info = new Transactions_info();
                     info.transaction_id = reader.GetInt64(0);
                     info.sender_accno = reader.GetInt64(1);
                     info.receiver_accno = reader.GetInt64(2);
                     info.amount = reader.GetInt64(3);
                     info.tx_time = reader.GetDateTime(4);

                     list_name.Add(info);


                 }

                sqlCon.Close();
            }
            
            catch (Exception ex)
            {
                //Console.WriteLine(ex.StackTrace);
                //Console.Write(ex.ToString());
                Console.WriteLine(ex.Message);
                Console.WriteLine("C# related problem");
            }
        }

    }
    public class Transactions_info
    {
        public long transaction_id;
        public long sender_accno, receiver_accno;
        public long amount;
        public DateTime tx_time;


    }
    public class Account_No
    {
        public long acc_no;
    }
}
    

