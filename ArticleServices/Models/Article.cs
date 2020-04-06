﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleServices.Models
{
    public class Article
    {
        public Categories Category { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

    }

    public enum Categories
    {
        Personal,
        Arts,
        Food,
        Games,
        Music

    }
}
