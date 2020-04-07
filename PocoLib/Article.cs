using System;

namespace PocoLib
{
    /// <summary>
    /// Article(Makale) nesnesi, Article tablosundaki kayda ait alanları içerir
    /// </summary>
    public class Article
    {
        public int Id { get; set; }
        public Nullable<int> Category { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
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
