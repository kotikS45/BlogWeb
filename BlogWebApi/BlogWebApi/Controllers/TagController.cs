using AutoMapper;
using BlogWebApi.Data.Entities;
using BlogWebApi.Data;
using BlogWebApi.Models.Category;
using Microsoft.AspNetCore.Mvc;
using BlogWebApi.Models.Tag;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly AppEFContext _appEFContext;
        private readonly IMapper _mapper;

        public TagController(AppEFContext appEFContext, IMapper mapper)
        {
            _appEFContext = appEFContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var list = await _appEFContext.Tags
                .Where(c => !c.IsDeleted)
                .Select(x => _mapper.Map<TagItemViewModel>(x))
                .ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TagCreateViewModel model)
        {
            var tag = _mapper.Map<TagEntity>(model);
            await _appEFContext.Tags.AddAsync(tag);
            await _appEFContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] TagEditViewModel model)
        {
            var tag = _appEFContext.Tags
                .Where(c => !c.IsDeleted)
                .SingleOrDefault(x => x.Id == model.Id);
            if (tag == null)
            {
                return NotFound();
            }
            tag.Name = model.Name;
            tag.UrlSlug = model.UrlSlug;
            tag.Description = model.Description;
            await _appEFContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tag = _appEFContext.Categories
                .Where(c => !c.IsDeleted)
                .SingleOrDefault(x => x.Id == id);
            if (tag == null)
            {
                return NotFound();
            }
            tag.IsDeleted = true;
            await _appEFContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tag = await _appEFContext.Tags
                .Where(c => !c.IsDeleted)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CategoryItemViewModel>(tag));
        }
    }
}
