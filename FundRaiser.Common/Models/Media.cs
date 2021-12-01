using System.Collections.Generic;

namespace FundRaiser.Common.Models
{
    public class Media
    {
        //Primary and foreign keys
        public int Id { get; set; }
        public int ProjectId { get; set; }
        
        //Base properties
        public string Description { get; set; }
        public string Path { get; set; }
        public MediaType MediaType { get; set; }

        //Navigation Properties
        public Project Project { get; set; }
    }
}