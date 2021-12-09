using FundRaiser.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundRaiser.Common.Dto
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public decimal CurrentAmount { get; set; }
        public decimal Goal { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfBackers { get; set; }
        public List<RewardDto> Rewards { get; set; } = new();
        public List<UpdateDto> Updates { get; set; } = new();
        public List<MediaDto> Media { get; set; } = new();

        public ProjectDto() { }

        public ProjectDto(Project project)
        {
            Id = project.Id;
            UserId = project.UserId;
            Title = project.Title;
            Description = project.Description;
            Category = project.Category;
            Goal = project.Goal;
            CurrentAmount = project.CurrentAmount;
            StartDate = project.StartDate;
            EndDate = project.EndDate;
            NumberOfBackers = project.NumberOfBackers;

            //Check Rewards collection
            if (project.Rewards != null)
            {
                var listReward = project.Rewards
                    .Where(r => r.ProjectId == Id)
                    .ToList();
                foreach (var r in listReward)
                {
                    Rewards.Add(new RewardDto(r));
                }
            }
            //Check Updates collection
            if (project.Updates != null)
            {
                var listUpdate = project.Updates
                     .Where(u => u.ProjectId == Id)
                     .ToList();
                foreach (var u in listUpdate)
                {
                    Updates.Add(new UpdateDto(u));
                }
            }
            //Check Media collection
            if (project.Media != null)
            {
                var listMedia = project.Media
                     .Where(m => m.ProjectId == Id)
                     .ToList();
                foreach (var m in listMedia)
                {
                    Media.Add(new MediaDto(m));
                }
            }
        }
    }
}
