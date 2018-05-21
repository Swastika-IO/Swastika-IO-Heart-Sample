using System;

namespace SimpleBlog.Data.Blog
{
    public class Configuration
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int Type { get; set; }
        public DateTime CreatedDateUTC { get; set; }
    }
}