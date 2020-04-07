using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleServices.Models
{
    public class Article
    {
        public int Id { get; set; }
        public Categories Category { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Contents { get; set; }
        public string MediaBase64 { get; set; }

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
