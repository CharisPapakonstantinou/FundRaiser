using FundRaiser.Common.Models;

namespace FundRaiser.Common.Dto
{
    public class MediaDto
    {
        public MediaDto(Media media, string appBasePath)
        {
            Id = media.Id;
            ProjectId = media.ProjectId;
            Description = media.Description;
            Path = $"{appBasePath}{media.Path}";
            MediaType = media.MediaType;
        }
        
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public MediaType MediaType { get; set; }   
    }
}