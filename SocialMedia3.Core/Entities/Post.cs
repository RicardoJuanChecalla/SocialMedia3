﻿using System;
using System.Collections.Generic;

namespace SocialMedia3.Core.Entities
{
    public partial class Post : BaseEntity
    {
        public Post() => Comments = new HashSet<Comment>();
        //public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
