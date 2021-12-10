using FundRaiser.Common.Models;

namespace FundRaiser.Common.Dto
{
    public class UserPostDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserPostDto() { }

        public UserPostDto(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
    }
}
