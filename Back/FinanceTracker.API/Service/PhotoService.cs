﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FinanceTracker.API.Helpers;
using FinanceTracker.API.Interfaces;
using Microsoft.Extensions.Options;

namespace FinanceTracker.API.Service;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloudinary;
    public PhotoService(IOptions<CloudinarySettings> config)
    {
        var acc = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
        _cloudinary = new Cloudinary(acc);
    }

    public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();
        if(file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                Folder = "financetracker-receipt"
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }
        return uploadResult;
    }

    public async Task<DeletionResult> DeletePhotoAsync(string Id)
    {
        var deleteParams = new DeletionParams(Id);
        return await _cloudinary.DestroyAsync(deleteParams);
    }
}