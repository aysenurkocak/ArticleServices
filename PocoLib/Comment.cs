using System;

namespace PocoLib
{
    /// <summary>
    /// Comment nesnesi, Comment tablosundaki kayda ait alanları içerir
    /// </summary>
    public class Comment
    {
        public int Id { get; set; }
        public Nullable<int> Star { get; set; }
        public string Contents { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Allowance { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

    }

}
