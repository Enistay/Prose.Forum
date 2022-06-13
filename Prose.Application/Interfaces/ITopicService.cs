using Prose.Application.ViewModels;
using System.Collections.Generic;

namespace Prose.Application.Interfaces
{
    public interface ITopicService
    {
        TopicViewModel GetTopicById(int Id);
        void Add(TopicViewModel topicCreate);
        void Update(TopicViewModel topicCreate);
        TopicViewModel GetTopicForEdit(int idTopic, string nameUser);
        IList<TopicViewModel> GetByPage(int page);
        IList<TopicViewModel> GetByPageBydUser(int page, string nameUser);
        
    }
}
