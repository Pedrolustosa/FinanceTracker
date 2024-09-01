using CloudinaryDotNet.Actions;

namespace FinanceTracker.API.Interfaces;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    Task<DeletionResult> DeletePhotoAsync(string Id);
}
