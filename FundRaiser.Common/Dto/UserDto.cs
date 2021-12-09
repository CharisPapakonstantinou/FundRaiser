using FundRaiser.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace FundRaiser.Common.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<ProjectDto> ProjectsAsCreator { get; set; } = new();
        public List<ProjectDto> ProjectsAsBacker { get; set; } = new();

        public UserDto() { }

        public UserDto(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;

            
            if (user.Projects != null)
            {
                var list = user.Projects
                    .Where(p => p.UserId == Id)
                    .ToList();
                foreach (var l in list)
                {
                    ProjectsAsCreator.Add(new ProjectDto(l));
                }
            }

            
            if (user.Projects != null)
            {
                var lista = user.Funds
                .Where(f => f.UserId == Id)
                .Select(f => f.Reward.Project)
                .Distinct()
                .ToList();
                foreach (var k in lista)
                {
                    ProjectsAsBacker.Add(new ProjectDto(k));
                }
            }
        }
    }
}
