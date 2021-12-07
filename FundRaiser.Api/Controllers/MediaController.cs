using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundRaiser.Common.ConfigMappers;
using FundRaiser.Common.Interfaces;
using FundRaiser.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundRaiser.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaController : Controller
    {
        private readonly StorageSettings _settings;
        private readonly IMediaService _mediaService;
        private const string videoExtension = "mp4";
        private const string imageExtension = "png";
        
        public MediaController(IMediaService mediaService, StorageSettings settings)
        {
            _settings = settings;
            _mediaService = mediaService;
        }
        
        [HttpPost("Upload")]
        public async Task<object> OnPostUploadAsync(List<IFormFile> files, [FromForm] int projectId)
        {
            var mediaList = new List<Media>();
            
            long size = files.Sum(f => f.Length);
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var (success, media) = GenerateMediaAndPath(formFile, projectId);

                    if (!success)
                        break;
                    
                    using(var stream = System.IO.File.Create(media.Path))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    
                    mediaList.Add(media);
                }
            }
            
            //Add media into db
            await _mediaService.Create(mediaList);
            
            return mediaList;
        }

        private (bool success, Media media) GenerateMediaAndPath(IFormFile formFile, int projectId)
        {
            if (formFile.ContentType.ToLower().Contains("video"))
                return 
                (   true,
                    new Media
                    {
                        Path = $"{Environment.CurrentDirectory}/../{_settings.BasePath}/{_settings.VideoPath}/{Guid.NewGuid()}.{videoExtension}",
                        ProjectId = projectId,
                        MediaType = MediaType.Video
                    }
                );
            
            if(formFile.ContentType.ToLower().Contains("image"))
                return 
                    (   true,
                        new Media
                        {
                            Path = $"{Environment.CurrentDirectory}/../{_settings.BasePath}/{_settings.ImagesPath}/{Guid.NewGuid()}.{imageExtension}",
                            ProjectId = projectId,
                            MediaType = MediaType.Image
                        }
                    );
            
            return (false, null);
        }
    }
}