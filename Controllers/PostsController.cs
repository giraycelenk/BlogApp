using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class PostsController:Controller
    {
        private IPostRepository _postrepository;
        private ITagRepository _tagrepository;

        public PostsController(IPostRepository postrepository,ITagRepository tagrepository)
        {
            _postrepository = postrepository;
            _tagrepository = tagrepository;
        }
        public IActionResult Index()
        {
            return View(
                new PostViewModel
                {
                    Posts = _postrepository.Posts.ToList(),
                    Tags = _tagrepository.Tags.ToList(),
                }
            );
        }
    }
}