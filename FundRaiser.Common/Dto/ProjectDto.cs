using FundRaiser.Common.Models;
using System;
using System.Collections.Generic;

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

    }
}
