using AutoMapper;
using Prose.Application.Interfaces;
using Prose.Application.ViewModels;
using Prose.Core.Entities;
using Prose.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;

namespace Prose.Application.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IUserService _userService;
        IMapper _mapper { get; }
        public TopicService(ITopicRepository topicRepository,           
            IMapper mapper,
            IUserService userService)
        {
            _topicRepository = topicRepository;
            _userService = userService;
            _mapper = mapper;
         }
        public TopicViewModel GetTopicById(int Id)
        {
          return  _mapper.Map<Topic,TopicViewModel>(_topicRepository.GetById(Id));
        }

        public void Add(TopicViewModel topicCreate)
        {
            try
            {
                int idUser = _userService.GetUserIdByName(topicCreate.Username);

                Topic topic = new Topic
                {
                    Creation = DateTime.Now,
                    Enable = true,
                    Keyword = topicCreate.Keyword,
                    Text = topicCreate.Text,
                    Title = topicCreate.Title,
                    UserId = idUser
                };

                _topicRepository.Add(topic);
            }
            catch (Exception ex)
            {

                throw ex;
            }            
        }

        public void Update(TopicViewModel topicCreate)
        {
            try
            {
                Topic topic = new Topic
                {
                    UpdateDate = DateTime.Now,
                    Keyword = topicCreate.Keyword,
                    Text = topicCreate.Text,
                    Title = topicCreate.Title,
                    TopicId = topicCreate.TopicId
                };

                _topicRepository.Update(topic);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public TopicViewModel GetTopicForEdit(int idTopic, string nameUser)
        {
            try
            {
                Topic topic = _topicRepository.GetById(idTopic);
                int userId = _userService.GetUserIdByName(nameUser);
                ValidUserCreaterTopic(idTopic, nameUser, topic, userId);

                return _mapper.Map<Topic, TopicViewModel>(topic);
            }
            catch (ArgumentException ex)
            {

                throw ex;
            }
        }

        private static void ValidUserCreaterTopic(int idTopic, string nameUser, Topic topic, int userId)
        {
            if (topic != null && topic.TopicId > 0 && topic.UserId != userId)
            {
                throw new ArgumentException(string.Format("Topic Id: {0} isn't of this user: {1}", idTopic, nameUser));
            }
        }

        public IList<TopicViewModel> GetByPage(int page)
        {
            IList<Topic> topic = _topicRepository.GetByPage(page); 
            return _mapper.Map<IList<Topic>, IList<TopicViewModel>>(topic);
        }

        public IList<TopicViewModel> GetByPageBydUser(int page, string userUser)
        {
            IList<Topic> topic = _topicRepository.GetByPageByUser(page, userUser);
            return _mapper.Map<IList<Topic>, IList<TopicViewModel>>(topic);
        }
    }
}
