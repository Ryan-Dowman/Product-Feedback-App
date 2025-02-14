using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Product_Feedback_App.Models.Identity;
using Product_Feedback_App.Models.View;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Product_Feedback_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(SignInManager<AppUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel accountLoginViewModel)
        {
            if (!ModelState.IsValid) return View();

            SignInResult signInResult = await signInManager.PasswordSignInAsync(accountLoginViewModel.Username, accountLoginViewModel.Password, false, false);

            if (signInResult == null || !signInResult.Succeeded)
            {
                ModelState.AddModelError("Username", "Username or Password is incorrect");
                ModelState.AddModelError("Password", "Username or Password is incorrect");

                return View();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
