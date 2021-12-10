using FundRaiser.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace FundRaiser.Common.Dto
{
    public class ProjectPatchDto
    {      
        public string Title { get; set; }        
        public string Description { get; set; }       
        public Category? Category { get; set; }

        [Range(0, int.MaxValue)]
        public decimal? Goal { get; set; }      
        public DateTime? EndDate { get; set; }

    }
}
