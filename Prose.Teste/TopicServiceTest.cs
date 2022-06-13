using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prose.Application.Interfaces;
using Prose.Application.Services;
using Prose.Application.ViewModels;
using Prose.Core.Entities;
using Prose.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prose.Teste
{
    //MSTest
    [TestClass]
    public class TopicServiceTest
    {
        int topicId = 1;
        string nameUser = "Andrade_10";
        private readonly TopicService service;

        public TopicServiceTest()
        {

            var listTopic = new List<Topic> {
                new Topic {TopicId = 1, UserId = 2},
                new Topic {TopicId = 2, UserId = 3}
            };

            var listUser = new List<User> {
                new User {Username = "Andrade_10", UserId = 2},
                new User {Username = "Rafael", UserId = 3}
            };

            TopicViewModel topicViewModel = new TopicViewModel { TopicId = 10 };


            Mock<ITopicRepository> mockTopicRepository = new Mock<ITopicRepository>();
            Mock<IUserService> moqUserService = new Mock<IUserService>();

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Topic, TopicViewModel>(It.IsAny<Topic>()))
                .Returns(topicViewModel);

            mockTopicRepository
           .Setup(x => x.GetById(topicId))
           .Returns(listTopic.FirstOrDefault(x => x.TopicId.Equals(topicId)));

            moqUserService
            .Setup(x => x.GetUserIdByName(nameUser))
            .Returns(listUser.FirstOrDefault(x => x.Username.Equals(nameUser)).UserId);


            service = new TopicService(mockTopicRepository.Object, mockMapper.Object, moqUserService.Object);
        }

        [TestMethod]
        public void Topic_Service_Should_Get_Topic_Same_User_Create_For_Edit_Test()
        {
            var result = service.GetTopicForEdit(1, nameUser);
            Assert.IsTrue(result.TopicId > 0);
        }

        [TestMethod]
        public void Topic_Service_Should_Not_Get_Topic_Diferent_User_Create_For_Edit_Test()
        {
            int idTopic = 2;
            try
            {
                service.GetTopicForEdit(idTopic, nameUser);
            }
            catch (Exception ex)
            {

                Assert.Equals(ex.Message,
                string.Format("Topic Id: {0} isn't of this user: {1}", idTopic, nameUser));
            }
        }
    }
}
