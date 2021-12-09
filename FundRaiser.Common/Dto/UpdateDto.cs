using FundRaiser.Common.Models;
using System;

namespace FundRaiser.Common.Dto
{
    public class UpdateDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Descripiton { get; set; }
        public DateTime PostDate { get; set; }

        public UpdateDto() { }
        
        public UpdateDto(Update update)
        {
            Id = update.Id;
            ProjectId = update.ProjectId;
            Title = update.Title;
            Descripiton = update.Description;
            PostDate = update.PostDate;
        }
    }
}
