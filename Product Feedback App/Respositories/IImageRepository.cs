using Microsoft.AspNetCore.Mvc;

namespace Product_Feedback_App.Respositories
{
    public interface IImageRepository
    {
        Task<string?> UploadImageAsync(IFormFile formFile); 
    }
}
