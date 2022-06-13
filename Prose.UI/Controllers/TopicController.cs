using Prose.Application.Interfaces;
using Prose.Application.ViewModels;
using Prose.UI.Models;
using System;
using System.Web.Mvc;

namespace Prose.UI.Controllers
{
    [Authorize]
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;
        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public ActionResult MyTopics(int page = 1)
        {
            ViewBag.PageNumber = page;
            ViewBag.PageAction = "MyTopics";
            ViewBag.PageController = "Topic";
            return View(_topicService.GetByPageBydUser(page, User.Identity.Name));
        }

        public ActionResult Create()
        {
            return View(new TopicCreate {TopicId = 0 });
        }

        [HttpPost]
        public ActionResult Create(TopicCreate topicCreate)
        {
            if (ModelState.IsValid)
            {
                TopicViewModel topicViewModel = new TopicViewModel
                {
                    Text = topicCreate.Text,
                    Title = topicCreate.Title,
                    Username = User.Identity.Name,
                    Keyword = topicCreate.Keyword
                };

                _topicService.Add(topicViewModel);

                return RedirectToAction("MyTopics", "Topic");
            }

            return View(topicCreate);
        }

        public ActionResult Edit(int id)
        {
            try
            {
                TopicViewModel topicEdit = _topicService.GetTopicForEdit(id, User.Identity.Name);
                ViewBag.IsSuccess = false;
                if (topicEdit != null && topicEdit.TopicId > 0)
                {                    
                    return View(new TopicCreate
                    {
                        TopicId = topicEdit.TopicId,
                        Keyword = topicEdit.Keyword,
                        Text = topicEdit.Text,
                        Title = topicEdit.Title
                    });
                }

                ModelState.AddModelError(string.Empty, "Topic not found");
                return View();

            }
            catch (Exception)
            {
                
                    ModelState.AddModelError(string.Empty, "Error on Edit, please contact " +
                                             "the administrator of the application." +
                                             " This action can block your user.");
                    return View();
            }
        }

        [HttpPost]
        public ActionResult Edit(TopicCreate topicCreate)
        {
            if (ModelState.IsValid)
            {
                TopicViewModel topicViewModel = new TopicViewModel
                {
                    Text = topicCreate.Text,
                    Title = topicCreate.Title,
                    Username = User.Identity.Name,
                    Keyword = topicCreate.Keyword,
                    TopicId = topicCreate.TopicId
                };

                _topicService.Update(topicViewModel);

                ViewBag.IsSuccess = true;
            }

            return View(topicCreate);
        }


    }
}