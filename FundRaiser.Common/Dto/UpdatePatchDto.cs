using FundRaiser.Common.Models;

namespace FundRaiser.Common.Dto
{
    public class UpdatePatchDto
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public UpdatePatchDto() { }

        public UpdatePatchDto(Update update)
        {
            Title = update.Title;
            Description = update.Description;
        }
    }
}
