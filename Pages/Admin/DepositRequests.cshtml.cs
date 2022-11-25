using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BankingProject.Pages.Admin
{
    public class DepositRequestsModel : PageModel
    {
        public List<DepositRequests> obj = new List<DepositRequests>();
        public void OnGet()
        {
            try
            {
                string ConnectionString = "Data Source=INLPF3KG409\\SQLEXPRESS;Initial Catalog=banking_project;trusted_connection=true";
                SqlConnection sqlCon = new SqlConnection(ConnectionString);

                sqlCon.Open();

                string query = "select deposit_id,fullname,email_id,phno,deposit_type,years,amount,status from deposits where status='pending' ";

                SqlCommand cmd = new SqlCommand(query, sqlCon);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DepositRequests dr = new DepositRequests();
                    dr.deposit_id = reader.GetInt64(0);
                    dr.fullname = reader.GetString(1);
                    dr.email_id = reader.GetString(2);
                    dr.phno = reader.GetInt64(3);
                    dr.deposit_type = reader.GetString(4);
                    dr.years = reader.GetInt32(5);
                    dr.amount = reader.GetInt64(6);
                    dr.status = reader.GetString(7);

                    obj.Add(dr);


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public ActionResult OnPostUpdateRequests(long deposit_id)
        {
            try
            {
                long id = deposit_id;
                string temp = Request.Form["description"];

                string ConnectionString = "Data Source=INLPF3KG409\\SQLEXPRESS;Initial Catalog=banking_project;trusted_connection=true";
                SqlConnection sqlCon = new SqlConnection(ConnectionString);

                sqlCon.Open();

                string query = "update deposits set status=@temp where deposit_id=@id";
                SqlCommand cmd = new SqlCommand(query, sqlCon);

                cmd.Parameters.Add("@id", System.Data.SqlDbType.BigInt).Value = id;
                cmd.Parameters.Add("@temp", System.Data.SqlDbType.VarChar).Value = temp;

                int res = cmd.ExecuteNonQuery();
                Console.WriteLine(deposit_id);
                Console.WriteLine(temp);
                Console.WriteLine(res);

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //Console.WriteLine(temp);
            return RedirectToPage("/Admin/Dashboard");
        }
        public class DepositRequests
        {
            public long deposit_id;
            public string fullname;
            public string email_id;
            public long phno;
            public string deposit_type;
            public int years;
            public long amount;
            public string status;

        }
    }
}