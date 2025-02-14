using Microsoft.AspNetCore.Mvc;
using Product_Feedback_App.Respositories;

namespace Product_Feedback_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile formFile)
        {
            if (formFile == null) return BadRequest("Failure to handle file");

            string? fileUploadUrl = await imageRepository.UploadImageAsync(formFile);

            if (fileUploadUrl == null) return BadRequest("Failure to handle file");

            return new JsonResult(new { imageUrl = fileUploadUrl });
        }
    }
}
