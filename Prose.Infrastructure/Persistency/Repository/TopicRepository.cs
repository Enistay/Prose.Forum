using Prose.Core.Entities;
using Prose.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Prose.Infrastructure.Persistency.Repository
{
    public class TopicRepository : ITopicRepository, IDisposable
    {
        private ProseDbContext db = new ProseDbContext();

        public Topic GetById(int id)
        {
            return db.Topic.Include("User").FirstOrDefault(p => p.TopicId == id && p.Enable);            
        }
        public void Add(Topic Topic)
        {
            db.Topic.Add(Topic);
            db.SaveChanges();
        }

        public IEnumerable<Topic> Get(Expression<Func<Topic, bool>> filter)
        {
            return db.Topic.Where(filter).ToList();
        }

        public void Update(Topic topic)
        {
            var current = db.Topic.FirstOrDefault(f => f.TopicId == topic.TopicId);

            current.UpdateDate = topic.UpdateDate;
            current.Text = topic.Text;
            current.Title = topic.Title;
            current.Keyword = topic.Keyword;
            db.SaveChanges();
        }

        public void DeleteId(int id)
        {
            var current = db.Topic.FirstOrDefault(f => f.TopicId == id);
            current.Enable = false;
            current.UpdateDate = DateTime.Now;
            //TODO: create routine to remove after 30 days
            db.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IList<Topic> GetByPage(int page)
        {
            int totalPage = 15;
            int skipRows = totalPage * (page - 1);


            var topicText = db.Topic
                .Include("User")
                .Where(p => p.Enable)
                .OrderByDescending(a => a.Creation)
                .Skip(skipRows)
                .Take(totalPage)
                .Select(a => new
                {
                    Title = a.Title,
                    Text = a.Text.Substring(0, 200),
                    TopicId = a.TopicId,
                    Username = a.User.Username,
                    UserId = a.User.UserId,
                    Creation = a.Creation,
                    Enable = a.Enable,
                    Updated = a.UpdateDate
                }).ToList();

            return topicText.Select(a => new Topic
                        {
                            Title = a.Title,
                            Text = a.Text,
                            TopicId = a.TopicId,
                            User = new User { Username = a.Username },
                            UserId = a.UserId,
                            Creation = a.Creation,
                            UpdateDate = a.Updated
                        }).ToList();
        }

        public IList<Topic> GetByPageByUser(int page, string user)
        {
            int totalPage = 15;
            int skipRows = totalPage * (page - 1);


            var topicText = db.Topic
                .Include("User")
                .Where(p => p.Enable && user.Equals(p.User.Username))
                .OrderByDescending(a => a.Creation)
                .Skip(skipRows)
                .Take(totalPage)
                .Select(a => new
                {
                    Title = a.Title,
                    Text = a.Text.Substring(0, 200),
                    TopicId = a.TopicId,
                    Username = a.User.Username,
                    UserId = a.User.UserId,
                    Creation = a.Creation,
                    Enable = a.Enable,
                    Updated = a.UpdateDate
                }).ToList();

            return topicText.Select(a => new Topic
            {
                Title = a.Title,
                Text = a.Text,
                TopicId = a.TopicId,
                User = new User { Username = a.Username },
                UserId = a.UserId,
                Creation = a.Creation,
                UpdateDate = a.Updated
            }).ToList();
        }
    }
}
