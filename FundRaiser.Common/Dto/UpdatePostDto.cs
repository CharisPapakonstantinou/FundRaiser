using FundRaiser.Common.Models;
using System;

namespace FundRaiser.Common.Dto
{
    public class UpdatePostDto
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Descripiton { get; set; }
       
        public UpdatePostDto() { }

        public UpdatePostDto(Update update)
        {
            ProjectId = update.ProjectId;
            Title = update.Title;
            Descripiton = update.Description;
        }
    }
}
