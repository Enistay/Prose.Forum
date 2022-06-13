using System;

namespace Prose.Core.Entities
{
    public class Topic
    {
        public int TopicId { get; set; }       
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Creation { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Enable { get; set; }
        public string Keyword { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
