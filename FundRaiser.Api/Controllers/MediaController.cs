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
    [Route("api/[controller]")]
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

        [HttpGet]
        public async Task<ActionResult<ApiResult<MediaDto>>> Get([Required] int id)
        {
            var media = await _mediaService.GetMediaById(id);

            return media == null
                ? NotFound(new ApiResult<MediaDto>(null, false, "No media found for corresponding id"))
                : Ok(new ApiResult<MediaDto>(new MediaDto(media, startUpPath)));
        }

        [HttpGet("Content")]
        public async Task<IActionResult> GetContent([Required] int id)
        {
            var media = await _mediaService.GetMediaById(id);

            Byte[] b = System.IO.File.ReadAllBytes($"{Directory.GetParent(Environment.CurrentDirectory)}/{media.Path}");

            return File(b, media.MediaType == MediaType.Image ? "image/jpeg" : "video/mp4");
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<IEnumerable<MediaDto>>>> Upload([Required] List<IFormFile> files,
            [FromForm] [Required] int projectId)
        {
            if (await _projectService.GetProject(projectId) == null)
            {
                return NotFound(($"Not found project for id {projectId}"));
            }

            var mediaList = new List<Media>();


            long size = files.Sum(f => f.Length);
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var (success, media) = GenerateMediaAndPath(formFile, projectId);

                    if (!success)
                        break;

                    using (var stream = System.IO.File.Create($"{startUpPath}{media.Path}"))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    mediaList.Add(media);
                }
            }

            //Add media into db
            await _mediaService.Create(mediaList);

            return Ok(new ApiResult<IEnumerable<MediaDto>>(mediaList.Select(m => new MediaDto(m, startUpPath))));
        }

        private (bool success, Media media) GenerateMediaAndPath(IFormFile formFile, int projectId)
        {
            if (formFile.ContentType.ToLower().Contains("video"))
                return
                    (true,
                        new Media
                        {
                            Path = $"{_settings.BasePath}{_settings.VideoPath}/{Guid.NewGuid()}.{videoExtension}",
                            ProjectId = projectId,
                            MediaType = MediaType.Video
                        }
                    );

            if (formFile.ContentType.ToLower().Contains("image"))
                return
                    (true,
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