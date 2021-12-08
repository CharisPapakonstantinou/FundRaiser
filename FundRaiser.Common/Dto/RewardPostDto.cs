using FundRaiser.Common.Models;

namespace FundRaiser.Common.Dto
{
    public class RewardPostDto
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        
        public RewardPostDto() { }
        
        public RewardPostDto(Reward reward)
        {
            ProjectId = reward.ProjectId;
            Title = reward.Title;
            Description = reward.Description;
            Price = reward.Price;
        }
    }
}
