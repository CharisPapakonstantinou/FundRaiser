using System.Collections.Generic;

namespace FundRaiser.Common.Models
{
    public class User
    {
        //Primary key
        public int Id { get; set; }

        //Base properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        //Navigation properties 
        public List<Project> Projects { get; set; }
        public List<Fund> Funds { get; set; }
    }
}