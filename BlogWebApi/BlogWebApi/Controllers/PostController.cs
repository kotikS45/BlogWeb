using AutoMapper;
using BlogWebApi.Data.Entities;
using BlogWebApi.Data;
using BlogWebApi.Models.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogWebApi.Models.Post;
using BlogWebApi.Models.Tag;
using Microsoft.Extensions.Hosting;

namespace BlogWebApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly AppEFContext _appEFContext;
        private readonly IMapper _mapper;

        public PostController(AppEFContext appEFContext, IMapper mapper)
        {
            _appEFContext = appEFContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var list = await _appEFContext.Posts
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            var mapedList = new List<PostItemViewModel>();
            foreach (var post in list)
                mapedList.Add(_mapper.Map<PostItemViewModel>(post));

            for (int i = 0; i < list.Count; i++)
            {
                var category = _appEFContext.Categories.Where(x => x.Id == list[i].CategoryId).FirstOrDefault();
                if (category != null)
                    mapedList[i].Category = _mapper.Map<CategoryItemViewModel>(category);

                mapedList[i].Tags = new List<TagItemViewModel>();
                var postTags = _appEFContext.PostTags.Where(x => x.PostId == mapedList[i].Id).ToList();
                foreach (var postTag in postTags)
                {
                    var tag = _appEFContext.Tags.Where(x => x.Id == postTag.TagId).FirstOrDefault();
                    if (tag != null)
                        mapedList[i].Tags.Add(_mapper.Map<TagItemViewModel>(tag));
                }
            }
            return Ok(mapedList);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PostCreateViewModel model)
        {
            if (model == null)
                return BadRequest("Model is NULL");

            var post = _mapper.Map<PostEntity>(model);
            post.DateCreated = DateTime.UtcNow;

            if (post.Published)
                post.PostedOn = DateTime.UtcNow;

            var category = _appEFContext.Categories.Where(x => x.Id == post.CategoryId).FirstOrDefault();
            if (category != null)
                post.Category = category;

            if (model.Tags.Any())
            {
                await _appEFContext.Posts.AddAsync(post);
                foreach (var tagId in model.Tags)
                {
                    var existingTag = await _appEFContext.Tags.SingleOrDefaultAsync(t => t.Id == tagId);
                    if (existingTag != null)
                        await _appEFContext.PostTags.AddAsync(new PostTagEntity { Tag = existingTag, Post = post });
                    else
                        return BadRequest($"Tag with Id = {tagId} does not exist");
                }
            }

            await _appEFContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] PostEditViewModel model)
        {
            if (model == null)
                return BadRequest("Model is NULL");

            try
            {
                var post = _mapper.Map<PostEntity>(model);
                var entity = _appEFContext.Posts.AsNoTracking().Where(x => x.Id == post.Id).FirstOrDefault();
                if (entity == null)
                    BadRequest("Post does not exist");

                post.DateCreated = entity.DateCreated;
                post.Modified = DateTime.UtcNow;
                if (post.Published)
                {
                    if (entity.Published)
                        post.PostedOn = entity.PostedOn;
                    else
                        post.PostedOn = DateTime.UtcNow;
                }

                _appEFContext.Posts.Update(post);
                await _appEFContext.SaveChangesAsync();

                var list = _appEFContext.PostTags.Where(x => x.PostId == post.Id);
                _appEFContext.RemoveRange(list);

                foreach (int id in model.Tags)
                    _appEFContext.PostTags.Add(new PostTagEntity { PostId = post.Id, TagId = id });
                await _appEFContext.SaveChangesAsync();

            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = _appEFContext.Posts
                .Where(c => !c.IsDeleted)
                .SingleOrDefault(x => x.Id == id);
            if (post == null)
                return NotFound();
            post.IsDeleted = true;
            await _appEFContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("urlSlug/{urlSlug}")]
        public async Task<IActionResult> GetById(string urlSlug)
        {
            var item = await _appEFContext.Posts
                .Where(c => !c.IsDeleted)
                .SingleOrDefaultAsync(x => x.UrlSlug == urlSlug);
            if (item == null)
                return NotFound();

            var post = _mapper.Map<PostItemViewModel>(item);


            var category = _appEFContext.Categories.Where(x => x.Id == item.CategoryId).FirstOrDefault();
            if (category != null)
                post.Category = _mapper.Map<CategoryItemViewModel>(category);

            post.Tags = new List<TagItemViewModel>();
            var postTags = _appEFContext.PostTags.Where(x => x.PostId == post.Id).ToList();
            foreach (var postTag in postTags)
            {
                var tag = _appEFContext.Tags.Where(x => x.Id == postTag.TagId).FirstOrDefault();
                if (tag != null)
                    post.Tags.Add(_mapper.Map<TagItemViewModel>(tag));
            }

            return Ok(post);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _appEFContext.Posts
                .Where(c => !c.IsDeleted)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (item == null)
                return NotFound();

            var post = _mapper.Map<PostItemViewModel>(item);


            var category = _appEFContext.Categories.Where(x => x.Id == item.CategoryId).FirstOrDefault();
            if (category != null)
                post.Category = _mapper.Map<CategoryItemViewModel>(category);

            post.Tags = new List<TagItemViewModel>();
            var postTags = _appEFContext.PostTags.Where(x => x.PostId == post.Id).ToList();
            foreach (var postTag in postTags)
            {
                var tag = _appEFContext.Tags.Where(x => x.Id == postTag.TagId).FirstOrDefault();
                if (tag != null)
                    post.Tags.Add(_mapper.Map<TagItemViewModel>(tag));
            }

            return Ok(post);
        }

        [HttpGet("category/{slugUrl}")]
        public async Task<IActionResult> GetByCategory(string slugUrl)
        {
            var category = await _appEFContext.Categories
                .Where(c => !c.IsDeleted)
                .Where(c => c.UrlSlug == slugUrl)
                .SingleOrDefaultAsync();
            if (category == null)
                return NotFound();

            var list = await _appEFContext.Posts
                .Where(c => !c.IsDeleted)
                .Where(c => c.CategoryId == category.Id)
                .ToListAsync();

            var mapedList = new List<PostItemViewModel>();
            foreach (var post in list)
                mapedList.Add(_mapper.Map<PostItemViewModel>(post));

            for (int i = 0; i < list.Count; i++)
            {
                mapedList[i].Category = _mapper.Map<CategoryItemViewModel>(category);
                mapedList[i].Tags = new List<TagItemViewModel>();
                var postTags = _appEFContext.PostTags.Where(x => x.PostId == mapedList[i].Id).ToList();
                foreach (var postTag in postTags)
                {
                    var tag = _appEFContext.Tags.Where(x => x.Id == postTag.TagId).FirstOrDefault();
                    if (tag != null)
                        mapedList[i].Tags.Add(_mapper.Map<TagItemViewModel>(tag));
                }
            }
            return Ok(mapedList);
        }

        [HttpGet("tag/{slug}")]
        public async Task<IActionResult> GetByTag(string slug)
        {
            var tagFind = await _appEFContext.Tags
                .Where(c => !c.IsDeleted)
                .Where(c => c.UrlSlug == slug)
                .SingleOrDefaultAsync();
            if (tagFind == null)
                return NotFound();

            var list = await _appEFContext.PostTags
                .Where(c => c.TagId == tagFind.Id)
                .ToListAsync();

            var posts = new List<PostEntity>();

            foreach (var item in list)
            {
                var post = _appEFContext.Posts.Where(x => x.Id == item.PostId).Where(c => !c.IsDeleted).FirstOrDefault();
                if (post != null)
                    posts.Add(post);
            }

            var mapedList = new List<PostItemViewModel>();
            foreach (var post in posts)
                mapedList.Add(_mapper.Map<PostItemViewModel>(post));

            for (int i = 0; i < posts.Count; i++)
            {
                var category = _appEFContext.Categories.Where(x => x.Id == posts[i].CategoryId).FirstOrDefault();
                if (category != null)
                    mapedList[i].Category = _mapper.Map<CategoryItemViewModel>(category);

                mapedList[i].Tags = new List<TagItemViewModel>();
                var postTags = _appEFContext.PostTags.Where(x => x.PostId == mapedList[i].Id).ToList();
                foreach (var postTag in postTags)
                {
                    var tag = _appEFContext.Tags.Where(x => x.Id == postTag.TagId).FirstOrDefault();
                    if (tag != null)
                        mapedList[i].Tags.Add(_mapper.Map<TagItemViewModel>(tag));
                }
            }
            return Ok(mapedList);
        }
    }
}
