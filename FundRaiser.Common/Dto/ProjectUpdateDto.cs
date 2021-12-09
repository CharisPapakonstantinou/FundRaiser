using FundRaiser.Common.Models;
using System;

namespace FundRaiser.Common.Dto
{
    public class ProjectPatchDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public decimal? Goal { get; set; }
        public DateTime EndDate { get; set; }

        public ProjectPatchDto() { }

        public ProjectPatchDto(Project project)
        {
            Title = project.Title;
            Description = project.Description;
            Category = project.Category;
            Goal = project.Goal;
            EndDate = project.EndDate;
        }
    }
}
