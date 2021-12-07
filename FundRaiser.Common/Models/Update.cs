using System;

namespace FundRaiser.Common.Models
{
    public class Update
    {
        //Primary and foreign keys
        public int Id { get; set; }
        public int ProjectId { get; set; }        
        
        //Base properties
        public string Title { get; set; }
        public DateTime PostDate { get; set; } = DateTime.UtcNow; 
        public string Description { get; set; }

        //Navigation properties
        public Project Project { get; set; }
    }
}