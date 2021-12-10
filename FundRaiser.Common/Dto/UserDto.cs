using System.Collections.Generic;

namespace FundRaiser.Common.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<ProjectDto> ProjectsAsCreator { get; set; } = new();
        public List<ProjectDto> ProjectsAsBacker { get; set; } = new();
       
    }
}
