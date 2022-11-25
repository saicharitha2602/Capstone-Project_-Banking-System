using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BankingProject.Pages.Users
{
    public class ViewBalanceModel : PageModel
    {
        public List<ViewBalance> list = new List<ViewBalance>();
        public void OnPost()
        {
            try
            {
                string ConnectionString = "Data Source=INLPF3KG409\\SQLEXPRESS;Initial Catalog=banking_project;trusted_connection=true";
                SqlConnection sqlCon = new SqlConnection(ConnectionString);
                sqlCon.Open();

                long acc_no = Convert.ToInt64(Request.Form["acc_no"]);

                SqlCommand cmd = new SqlCommand("view_balance", sqlCon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@acc_no", System.Data.SqlDbType.BigInt).Value =acc_no;
                cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ViewBalance info = new ViewBalance();
                    info.acc_no = reader.GetInt64(0);
                    info.acc_type = reader.GetString(1);
                    info.balance = reader.GetInt64(2);
                   

                    list.Add(info);

                    sqlCon.Close();
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
    public class ViewBalance
    {
        public long acc_no;
        public string acc_type;
        public long balance;
    }
}
