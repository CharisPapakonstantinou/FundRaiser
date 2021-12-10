using FundRaiser.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FundRaiser.Mvc.ViewModels
{
    public class ProjectUpdateViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Category? Category { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }

        [DataType(DataType.Currency)]
        [Range(1, Int32.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public decimal? Goal { get; set; }
    }
}
