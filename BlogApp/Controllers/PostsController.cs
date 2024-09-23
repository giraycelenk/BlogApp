using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class PostsController:Controller
    {
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;
        private ITagRepository _tagRepository;
        public PostsController(IPostRepository postRepository,ICommentRepository commentRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _tagRepository = tagRepository;
        }
        public async Task<IActionResult> Index(string tag,int page_num=1)
        {
            var posts = _postRepository.Posts.Where(i => i.IsActive == true);
            int skipPosts = (page_num-1)*5;
            int totalPages = (int)Math.Ceiling((double)posts.Count() / 5);
            if(!string.IsNullOrEmpty(tag))
            {
                posts = posts.Where(x => x.Tags.Any(t=>t.Url==tag));
            }
            return View(new PostViewModel{Posts = await posts.OrderByDescending(p => p.PublishedOn).Skip(skipPosts).Take(5).ToListAsync(),TotalPagesCount=totalPages,PageNum=page_num});
        }
        public async Task<IActionResult> Details(string url)
        {
            return View(await _postRepository
                            .Posts
                            .Include(x => x.User)
                            .Include(x => x.Tags)
                            .Include(x => x.Comments)
                            .ThenInclude(x => x.User)
                            .FirstOrDefaultAsync(p=>p.Url == url));
        }
        [HttpPost]
        public JsonResult AddComment(int PostId, string Text)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);
            var avatar = User.FindFirstValue(ClaimTypes.UserData);
            var name = User.FindFirstValue(ClaimTypes.GivenName);
            var entity = new Comment {
                PostId = PostId,
                Text = Text,
                PublishedOn = DateTime.Now,
                UserId = int.Parse(userId ?? "")
            };
            _commentRepository.CreateComment(entity);

            return Json(new{
                username,
                name,
                Text,
                entity.PublishedOn,
                avatar
            });
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PostCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var post = await _postRepository.Posts.FirstOrDefaultAsync(x => x.Url == model.Url);
                if(post == null)
                {
                    _postRepository.CreatePost(
                        new Post{
                            Title = model.Title,
                            Description = model.Description,
                            Content = model.Content,
                            Url = model.Url,
                            UserId = int.Parse(userId ?? ""),
                            PublishedOn = DateTime.Now,
                            Image = "1.jpg",
                            IsActive = true
                        }
                    );
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("","Bu url kullanımda.");
                }
                
            }
            return View();
        }
        [Authorize]
        public async Task<IActionResult> List()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            var role = User.FindFirstValue(ClaimTypes.Role);

            var posts = _postRepository.Posts;

            if(string.IsNullOrEmpty(role))
            {
                posts = posts.Where(i => i.UserId == userId);
            }
            return View(await posts.ToListAsync());
        }
        [Authorize]
        public IActionResult Edit(string url)
        {
            if(string.IsNullOrEmpty(url))
            {
                return NotFound();
            }

            var post = _postRepository
                        .Posts
                        .Include(x => x.Tags)
                        .FirstOrDefault(i => i.Url == url);
            if(post == null)
            {
                return NotFound();
            }

            ViewBag.Tags = _tagRepository.Tags.ToList();

            return View(new PostCreateViewModel{
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                Url = post.Url,
                IsActive = post.IsActive,
                Tags = post.Tags
            });
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(PostCreateViewModel model, int[] tagIds)
        {
            if(ModelState.IsValid)
            {
                var entityToUpdate = new Post {
                    PostId = model.PostId,
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    Url = model.Url,
                };

                if(User.FindFirstValue(ClaimTypes.Role) == "admin")
                {
                    entityToUpdate.IsActive = model.IsActive;
                }

                _postRepository.EditPost(entityToUpdate,tagIds);
                return RedirectToAction("List");
            }
            ViewBag.Tags = _tagRepository.Tags.ToList();
            return View(model);
        }
        public IActionResult Delete(string url)
        {
            if(string.IsNullOrEmpty(url))
            {
                return NotFound();
            }
            var post = _postRepository.Posts.FirstOrDefault(i => i.Url == url);
            if(post == null)
            {
                return NotFound();
            }
            return View(new PostDeleteViewModel{
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                Url = post.Url,
                IsActive = post.IsActive,
            });
            
        }
        [Authorize]
        [HttpPost]
        public IActionResult Delete(PostDeleteViewModel model)
        {
            if(model == null)
            {
                return NotFound();
            }

            var post = _postRepository.Posts.FirstOrDefault(i => i.PostId == model.PostId);

            if(post == null)
            {
                return NotFound();
            }

            _postRepository.DeletePost(post);
            return RedirectToAction("List");
        }
    }
}