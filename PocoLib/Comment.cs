﻿using System;

namespace PocoLib
{
    /// <summary>
    /// Comment nesnesi, Comment tablosundaki kayda ait alanları içerir
    /// </summary>
    public class Comment
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }       
        public Nullable<int> Star { get; set; }
        public string Contents { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Allowance { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }

    }

}
