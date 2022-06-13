using System;
using System.Collections.Generic;

namespace Prose.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Enable { get; set; }
        public DateTime Registration { get; set; }
        public ICollection<Topic> Topics { get; set; }
    }
}
