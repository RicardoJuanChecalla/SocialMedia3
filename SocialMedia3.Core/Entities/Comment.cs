﻿using System;
using System.Collections.Generic;

namespace SocialMedia3.Core.Entities
{
    public partial class Comment : BaseEntity
    {
        //public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string?  Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public virtual Post? Post { get; set; }
        public virtual User? User { get; set; }
    }
}
