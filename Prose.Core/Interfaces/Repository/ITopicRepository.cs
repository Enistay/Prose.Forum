using Prose.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Prose.Core.Interfaces.Repository
{
    public interface ITopicRepository
    {
        Topic GetById(int id);
        IEnumerable<Topic> Get(Expression<Func<Topic, bool>> filter);
        void Add(Topic Topic);        
        void Update(Topic topic);
        void DeleteId(int id);
        IList<Topic> GetByPage(int page);
        IList<Topic> GetByPageByUser(int page, string user);
    }
}
