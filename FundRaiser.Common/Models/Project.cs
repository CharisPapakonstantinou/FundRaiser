using System;
using System.Collections.Generic;

namespace FundRaiser.Common.Models
{
    public class Project
    {
        //Primary and foreign keys
        public int Id { get; set; }
        public int UserId { get; set; }
        
        //Base properties
        public string Title { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public decimal Goal { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfBackers { get; set; }

        //Navigation properties
        public User User { get; set; }
        public List<Reward> Rewards { get; set; }
        public List<Update> Updates { get; set; }
        public List<Media> Media { get; set; }
    }
}