using AutoMapper;
using Prose.Application.ViewModels;
using Prose.Core.Entities;

namespace Prose.Application.Mappers
{
    public class TopicMapper : Profile
    {
        public TopicMapper()
        {
            CreateMap<Topic, TopicViewModel>()
                .ForMember(t => t.Username, tv => tv.MapFrom(m => m.User.Username));
            CreateMap<User, UserTopicViewModel>();                 
        }
    }
}
