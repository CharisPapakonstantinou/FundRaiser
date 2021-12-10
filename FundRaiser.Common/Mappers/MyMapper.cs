using FundRaiser.Common.Dto;
using FundRaiser.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace FundRaiser.Common.Mappers
{
    public static class MyMapper
    {
        public static User UserPostDtoToUser(UserPostDto userPostDto)
        {
            return new User()
            {
                FirstName = userPostDto.FirstName,
                LastName = userPostDto.LastName
            };
        }

        public static UserDto UserToUserDto(User user)
        {
            var newlistcreator = new List<ProjectDto>();

            if (user.Projects != null)
            {
                var list = user.Projects
                    .Where(p => p.UserId == user.Id)
                    .ToList();
                foreach (var l in list)
                {
                    // newlistcreator.Add(new ProjectDto(l));
                    newlistcreator.Add(ProjectToProjectDto(l));
                }
            }

            var newlistbacker = new List<ProjectDto>();
            if (user.Projects != null)
            {
                var lista = user.Funds
                .Where(f => f.UserId == user.Id)
                .Select(f => f.Reward.Project)
                .Distinct()
                .ToList();
                foreach (var k in lista)
                {
                    //newlistbacker.Add(new ProjectDto(k));
                    newlistbacker.Add(ProjectToProjectDto(k));
                }
            }

            return new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProjectsAsCreator = newlistcreator,
                ProjectsAsBacker = newlistbacker
            };

        }

        public static Update UpdatePatchDtoToUpdate(UpdatePatchDto updatePatchDto)
        {
            return new Update()
            {
                Title = updatePatchDto.Title,
                Description = updatePatchDto.Description
            };
        }

        public static UpdateDto UpdateToUpdateDto(Update update)
        {
            return new UpdateDto()
            {
                Id = update.Id,
                ProjectId = update.ProjectId,
                Title = update.Title,
                Description = update.Description,
                PostDate = update.PostDate
            };
        }

        public static Update UpdatePostDtoToUpdate(UpdatePostDto updatePostDto)
        {
            return new Update()
            {
                ProjectId = updatePostDto.ProjectId,
                Title = updatePostDto.Title,
                Description = updatePostDto.Description
            };
        }

        public static Reward RewardPostDtoToReward(RewardPostDto rewardPostDto)
        {
            return new Reward()
            {
                ProjectId = rewardPostDto.ProjectId,
                Title = rewardPostDto.Title,
                Description = rewardPostDto.Description,
                Price = (decimal)rewardPostDto.Price
            };
        }

        public static RewardDto RewardToRewardDto(Reward reward)
        {
            return new RewardDto()
            {
                Id = reward.Id,
                ProjectId = reward.ProjectId,
                Title = reward.Title,
                Description = reward.Description,
                Price = reward.Price
            };
        }

        public static Project ProjectPostDtoToProject(ProjectPostDto projectPostDto)
        {
            return new Project()
            {
                UserId = projectPostDto.UserId,
                Title = projectPostDto.Title,
                Description = projectPostDto.Description,
                Category = projectPostDto.Category,
                Goal = projectPostDto.Goal,
                EndDate = projectPostDto.EndDate
            };
        }

        public static ProjectDto ProjectToProjectDto(Project project)
        {
            var rewardList = new List<RewardDto>();
            var updatesList = new List<UpdateDto>();
            var mediaList = new List<MediaDto>();

            //Check Rewards collection
            if (project.Rewards != null)
            {
                var listReward = project.Rewards
                    .Where(r => r.ProjectId == project.Id)
                    .ToList();
                foreach (var reward in listReward)
                {
                    rewardList.Add(RewardToRewardDto(reward));
                }
            }
            //Check Updates collection
            if (project.Updates != null)
            {
                var listUpdate = project.Updates
                     .Where(u => u.ProjectId == project.Id)
                     .ToList();
                foreach (var update in listUpdate)
                {
                    updatesList.Add(UpdateToUpdateDto(update));
                }
            }
            //Check Media collection
            if (project.Media != null)
            {
                var listMedia = project.Media
                     .Where(m => m.ProjectId == project.Id)
                     .ToList();
                foreach (var m in listMedia)
                {
                    mediaList.Add(new MediaDto(m));
                }
            }

            return new ProjectDto()
            {
                Id = project.Id,
                UserId = project.UserId,
                Title = project.Title,
                Description = project.Description,
                Category = project.Category,
                Goal = project.Goal,
                CurrentAmount = project.CurrentAmount,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                NumberOfBackers = project.NumberOfBackers,
                Rewards = rewardList,
                Updates = updatesList,
                Media = mediaList
            };
        }

    }
}
