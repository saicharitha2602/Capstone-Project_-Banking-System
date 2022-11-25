using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BankingProject.Pages.Users
{
    public class LoginModel : PageModel
    {
        public Credentials c = new Credentials();
       



        [BindProperty]
        [Required(ErrorMessage="username is required")]
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "password is required")]
        public string Password { get; set; }
        public void OnGet()
        {
        }
        public ActionResult OnPost()
        {
            
            try
            {
                string ConnectionString = "Data Source=INLPF3KG409\\SQLEXPRESS;Initial Catalog=banking_project;trusted_connection=true";
                SqlConnection sqlCon = new SqlConnection(ConnectionString);

                c.username = Request.Form["username"];
                c.password = Request.Form["password"];

               

                HttpContext.Session.SetString("username", c.username);

                sqlCon.Open();

                SqlCommand cmd = new SqlCommand("authenticate_user", sqlCon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar).Value = c.username;
                cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar).Value = c.password;

                SqlDataReader sdr= cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    sdr.Read();
                    
                    TempData["success_message"] = "success";
                    TempData["Message"] = HttpContext.Session.GetString("username");
                    //HttpContext.Session.SetString("username", c.username);
                    return RedirectToPage("/Users/Home");

                    
                }
                else
                {
                    TempData["error_message"] = "Invalid username or password";
                }

                sqlCon.Close();

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Sql related problem");
               
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("C# related problem");
                
               
            }
            //success_msg = "login successful";
            // Response.AddHeader("REFRESH", "5;URL=/Users/Home.cshtml");
            //TempData["error_message"] = "Invalid username or password";
            return RedirectToPage("/Users/Login");
        }
        public class Credentials
        {

            public string username = null;
            public string password;
         }
    }
}
