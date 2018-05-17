using System;

namespace DemoSwastikaHeart.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDateUTC { get; set; }        
    }
}