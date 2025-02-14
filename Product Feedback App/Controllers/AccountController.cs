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
        private readonly UserManager<AppUser> userManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel accountRegisterViewModel)
        {
            if (!ModelState.IsValid) return View();

            bool usernameTaken = await UsernameTaken(accountRegisterViewModel.Username);
            bool emailTaken = await EmailTaken(accountRegisterViewModel.Email);

            if (usernameTaken || emailTaken)
            {
                if (usernameTaken) ModelState.AddModelError("Username", "Username is taken");
                if (emailTaken) ModelState.AddModelError("Email", "Email is taken");

                return View();
            }

            AppUser user = new AppUser
            {
                UserName = accountRegisterViewModel.Username,
                NormalizedUserName = accountRegisterViewModel.Username.ToUpper(),
                Email = accountRegisterViewModel.Email,
                NormalizedEmail = accountRegisterViewModel.Email.ToUpper()
            };

            user.PasswordHash = userManager.PasswordHasher.HashPassword(user, accountRegisterViewModel.Password);

            await userManager.CreateAsync(user);

            return RedirectToAction("Index", "Home");
        }

        public async Task<bool> UsernameTaken(string username)
        {
            return (await userManager.FindByNameAsync(username)) != null;
        }

        public async Task<bool> EmailTaken(string email)
        {
            return (await userManager.FindByEmailAsync(email)) != null;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
