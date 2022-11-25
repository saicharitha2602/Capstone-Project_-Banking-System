using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BankingProject.Pages.Users
{
    public class RegisterModel : PageModel
    {
        public UserAccounts user = new UserAccounts();
        public string error_msg = "";
        public string success_msg = "";
        public void OnGet()
        {

        }
        public void OnPost()
        {
            try
            {
                string ConnectionString = "Data Source=INLPF3KG409\\SQLEXPRESS;Initial Catalog=banking_project;trusted_connection=true";
                SqlConnection sqlCon = new SqlConnection(ConnectionString);

                user.fullname = Request.Form["fullname"];
                user.email_id = Request.Form["email_id"];
                user.phno = Convert.ToInt64(Request.Form["phno"]);
                user.acc_no = Convert.ToInt64(Request.Form["acc_no"]);
                user.balance = Convert.ToInt64(Request.Form["balance"]);
                user.acc_type = Request.Form["acc_type"];
                user.username = Request.Form["username"];
                user.password = Request.Form["password"];




                sqlCon.Open();

                SqlCommand cmd = new SqlCommand("add_user_accounts", sqlCon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@fullname", System.Data.SqlDbType.VarChar).Value = user.fullname;
                cmd.Parameters.Add("@email_id", System.Data.SqlDbType.VarChar).Value = user.email_id;
                cmd.Parameters.Add("@phno", System.Data.SqlDbType.BigInt).Value = user.phno;
                cmd.Parameters.Add("@acc_no", System.Data.SqlDbType.BigInt).Value = user.acc_no;
                cmd.Parameters.Add("@acc_type", System.Data.SqlDbType.VarChar).Value = user.acc_type;
                cmd.Parameters.Add("@balance", System.Data.SqlDbType.BigInt).Value = user.balance;
                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar).Value = user.username;
                cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar).Value = user.password;


                cmd.ExecuteNonQuery();



                sqlCon.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Sql related problem");
                error_msg = ex.Message;
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("C# related problem");
                error_msg = ex.Message;
                return;
            }
            success_msg = "Successfully added";

        }
        
        public class UserAccounts
        {
            public string fullname, email_id;
            public long phno;
            public long acc_no;
            public string acc_type;
            public long balance;
            public string username, password;



        }
    }
}
