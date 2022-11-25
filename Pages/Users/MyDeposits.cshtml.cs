using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BankingProject.Pages.Users
{
    public class MyDepositsModel : PageModel
    {
        public List<DepositStatus>obj =new List<DepositStatus>();
        public void OnGet()
        {
            try
            {
                string user = HttpContext.Session.GetString("username");
                string ConnectionString = "Data Source=INLPF3KG409\\SQLEXPRESS;Initial Catalog=banking_project;trusted_connection=true";


                SqlConnection sqlCon = new SqlConnection(ConnectionString);
                sqlCon.Open();

                string query = "select deposit_id,fullname,phno,deposit_type,years,amount,status from deposits where username=@user ";


                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.Add("@user", System.Data.SqlDbType.VarChar).Value = user;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DepositStatus ds = new DepositStatus();
                    ds.deposit_id = reader.GetInt64(0);
                    ds.fullname = reader.GetString(1);
                    ds.phno = reader.GetInt64(2);
                    ds.deposit_type = reader.GetString(3);
                    ds.years = reader.GetInt32(4);
                    ds.amount = reader.GetInt64(5);
                    ds.status = reader.GetString(6);

                    obj.Add(ds);


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
    public class DepositStatus
    {
        public long deposit_id;
        public string fullname;
        public long phno;
        public string deposit_type;
        public int years;
        public long amount;
        public string status;
    }
}
