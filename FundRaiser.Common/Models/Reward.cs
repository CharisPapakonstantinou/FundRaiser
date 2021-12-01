using System.Collections.Generic;

namespace FundRaiser.Common.Models
{
    public class Reward
    {
        //Primary and foreign keys
        public int Id { get; set; }
        public int ProjectId { get; set; }

        //Base properties
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal RequiredAmount { get; set; }
        
        //Navigation properties
        public List<Fund> Funds { get; set; }
        public Project Project { get; set; }
    }
}