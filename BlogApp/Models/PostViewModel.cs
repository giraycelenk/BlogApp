using BlogApp.Entity;

namespace BlogApp.Models
{
    public class PostViewModel
    {
        public List<Post> Posts { get; set; } = new();
        public int TotalPagesCount { get; set; }
        public int PageNum { get; set; }
        
    }
}