using FundRaiser.Common.Models;
using System.IO;

namespace FundRaiser.Common.Dto
{
    public class MediaDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public MediaType MediaType { get; set; }

        public MediaDto(Media media, string appBasePath)
        {
            Id = media.Id;
            ProjectId = media.ProjectId;
            Description = media.Description;
            Path = $"{appBasePath}{media.Path}";
            MediaType = media.MediaType;
        }

        public MediaDto(Media media)
        {
            //For testing purpose. Domain info should be injected from Configuration.
            string appBasePath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory())?.ToString();
            
            Id = media.Id;
            ProjectId = media.ProjectId;
            Description = media.Description;
            Path = $"{appBasePath}{media.Path}";
            MediaType = media.MediaType;
        }  
    }
}