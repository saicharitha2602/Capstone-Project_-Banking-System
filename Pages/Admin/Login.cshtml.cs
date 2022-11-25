
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankingProject.Pages.Admin
{
    public class LoginModel : PageModel
    {
        Admin_Credentials adc = new Admin_Credentials();


        
        
        [Required(ErrorMessage ="Username is required")]
       
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        //public Admin_Login admin_login { get; set; }

        public void OnGet()
        {
        }
        public ActionResult OnPost()
        {

            try
            {
                adc.username = Request.Form["username"];
                adc.password = Request.Form["password"];

                if (adc.username == "admin" && adc.password == "admin123")
                {
                    TempData["success_message"] = "You are logged in ";
                    return RedirectToPage("/Admin/Dashboard");
                }

               else
                {
                    TempData["error_message"] = "Invalid username or password";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        
                return RedirectToPage("/Admin/Login");
    }
        
    }
    public class Admin_Credentials
    {

        public string username;

        
        public string password;
    }
}
