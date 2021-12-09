using FundRaiser.Common.Models;
using System;

namespace FundRaiser.Common.Dto
{
    public class ProjectUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public decimal? Goal { get; set; }
        public DateTime EndDate { get; set; }

        public ProjectUpdateDto() { }

        public ProjectUpdateDto(Project project)
        {
            Title = project.Title;
            Description = project.Description;
            Category = project.Category;
            Goal = project.Goal;
            EndDate = project.EndDate;
        }
    }
}
