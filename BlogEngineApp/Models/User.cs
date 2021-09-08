using System;
using System.Collections.Generic;

#nullable disable

namespace BlogEngineApp.Models
{
    public partial class User
    {
        public User()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
