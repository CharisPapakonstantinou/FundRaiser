using FundRaiser.Common.Models;
using System;

namespace FundRaiser.Common.Dto
{
    public class ProjectPostDto
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public decimal Goal { get; set; }
        public DateTime EndDate { get; set; }

        public ProjectPostDto() { }

        public ProjectPostDto(Project project)
        {
            UserId = project.UserId;
            Title = project.Title;
            Description = project.Description;
            Category = project.Category;
            Goal = project.Goal;
            EndDate = project.EndDate;
        }
    }
}
