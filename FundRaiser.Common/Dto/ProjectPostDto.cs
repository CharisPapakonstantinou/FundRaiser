using FundRaiser.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace FundRaiser.Common.Dto
{
    public class ProjectPostDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Category Category { get; set; }

        [Range(0,int.MaxValue)]
        public decimal Goal { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

    }
}
