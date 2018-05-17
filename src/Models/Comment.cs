using System;

namespace DemoSwastikaHeart.Models
{
    public class Comment
    {
        public string Id { get; set; }
        public string PostId { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDateUTC { get; set; }
    }
}