using System;

namespace Prose.Application.ViewModels
{
    public class TopicViewModel
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Creation { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Keyword { get; set; }
        public string Username { get; set; }
    }
}
