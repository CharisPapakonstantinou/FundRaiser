namespace FundRaiser.Common.Models
{
    public class Fund /* BackerProject */
    {
        //Primary keys
        public int UserId { get; set; } 
        public int RewardId { get; set; }
        
        //Navigation properties
        public User User { get; set; }
        public Reward Reward { get; set; }
    }
}

