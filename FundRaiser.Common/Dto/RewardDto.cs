namespace FundRaiser.Common.Dto
{
    public class RewardDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }

    }
}
