using System;

namespace FundRaiser.Common.Dto
{
    public class UpdateDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PostDate { get; set; }
        
    }
}
