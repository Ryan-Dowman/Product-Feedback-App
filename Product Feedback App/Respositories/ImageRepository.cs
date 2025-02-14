using Microsoft.AspNetCore.Mvc;

namespace Product_Feedback_App.Respositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<string?> UploadImageAsync(IFormFile formFile)
        {
            string wwwrootPath = webHostEnvironment.WebRootPath;
            string profilePath = Path.Combine(wwwrootPath, "images", "profiles");

            string fileId = Guid.NewGuid().ToString();
            string fileExtension = Path.GetExtension(formFile.FileName);
            string fileName = $"{fileId}{fileExtension}";
            string filePath = Path.Combine(profilePath, fileName);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            HttpRequest? request = httpContextAccessor.HttpContext?.Request;

            if (request == null)
            {
                return null;
            }

            string fileUrl = $"{request.Scheme}://{request.Host}/images/profiles/{fileName}";
            return fileUrl;
        }
    }
}
