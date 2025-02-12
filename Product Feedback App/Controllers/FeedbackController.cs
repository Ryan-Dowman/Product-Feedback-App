using Microsoft.AspNetCore.Mvc;
using Product_Feedback_App.Models.View;

namespace Product_Feedback_App.Controllers
{
    public class FeedbackController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(FeedbackCreateViewModel createViewModel)
        {
            return View();
        }

        [HttpGet]
        public IActionResult View(Guid id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult View(FeedbackViewViewModel feedbackViewViewModel)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(FeedbackEditViewModel feedbackEditViewModel)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
