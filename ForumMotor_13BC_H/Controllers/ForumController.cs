using ForumMotor_13BC_H.Data;
using ForumMotor_13BC_H.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumMotor_13BC_H.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ForumController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ForumController(ApplicationDbContext context) { _context = context; }
        //like, dislike, create topic, category, post, edit post
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<List<Topic>>> GetTopics(int categoryId) { return await _context.Topics.Where(x => x.CategoryId == categoryId).ToListAsync(); }
        [HttpGet("{topicId}")]
        public async Task<ActionResult<List<Post>>> GetPosts(int topicId) { return await _context.Posts.Where(x => x.TopicId == topicId).ToListAsync(); }
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return category == null ? NotFound() : category;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Topic>> GetTopic(int id)
        {
            var topic = await _context.Topics.FindAsync(id); return topic == null ? NotFound() : topic;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id); return post == null ? NotFound() : post;
        }
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCategory", new {id = category.Id}, category);
        }
        [HttpPost]
        public async Task<ActionResult<Topic>> PostTopic(Topic topic)
        {
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTopic", new {id = topic.Id}, topic);
        }
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            Topic topic = post.Topic;
            if (topic.IsLocked)
            {
                return Forbid(topic.LockReason != null ? topic.LockReason : "No reason was given.");
            }
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPost", new {id = post.Id}, post);
        }

    }
}
