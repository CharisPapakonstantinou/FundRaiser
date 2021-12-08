using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FundRaiser.Common.ConfigMappers;
using FundRaiser.Common.Dto;
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
        private readonly IProjectService _projectService;
        private const string videoExtension = "mp4";
        private const string imageExtension = "jpg";
        
        private string startUpPath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory())?.ToString();

        public MediaController(IMediaService mediaService, StorageSettings settings, IProjectService projectService)
        {
            _settings = settings;
            _mediaService = mediaService;
            _projectService = projectService;
        }

        [HttpGet("GetMedia")]
        public async Task<ApiResult<MediaDto>> GetMedia([Required] int mediaId)
        {
            var media = await _mediaService.GetMediaById(mediaId);

            return media == null 
                ? new ApiResult<MediaDto>(null, false, "No media found for corresponding id") 
                : new ApiResult<MediaDto>(new MediaDto(media, startUpPath));
        }
        
        [HttpPost("Upload")]
        public async Task<ApiResult<IEnumerable<MediaDto>>> Upload([Required] List<IFormFile> files, [FromForm] [Required] int projectId)
        {
            if (await _projectService.GetProject(projectId) == null)
                throw new Exception($"Not found project for id {projectId}");
            
            var mediaList = new List<Media>();
            

            long size = files.Sum(f => f.Length);
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var (success, media) = GenerateMediaAndPath(formFile, projectId);

                    if (!success)
                        break;
                    
                    using(var stream = System.IO.File.Create($"{startUpPath}{media.Path}"))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    
                    mediaList.Add(media);
                }
            }
            
            //Add media into db
            await _mediaService.Create(mediaList);
            
            return new ApiResult<IEnumerable<MediaDto>>(mediaList.Select(m => new MediaDto(m, startUpPath)));
        }

        private (bool success, Media media) GenerateMediaAndPath(IFormFile formFile, int projectId)
        {
            if (formFile.ContentType.ToLower().Contains("video"))
                return 
                (   true,
                    new Media
                    {
                        Path = $"{_settings.BasePath}{_settings.VideoPath}/{Guid.NewGuid()}.{videoExtension}",
                        ProjectId = projectId,
                        MediaType = MediaType.Video
                    }
                );
            
            if(formFile.ContentType.ToLower().Contains("image"))
                return 
                    (   true,
                        new Media
                        {
                            Path = $"{_settings.BasePath}{_settings.ImagesPath}/{Guid.NewGuid()}.{imageExtension}",
                            ProjectId = projectId,
                            MediaType = MediaType.Image
                        }
                    );
            
            return (false, null);
        }
    }
}