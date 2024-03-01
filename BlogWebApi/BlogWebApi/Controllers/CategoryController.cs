using AutoMapper;
using BlogWebApi.Data;
using BlogWebApi.Data.Entities;
using BlogWebApi.Models.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppEFContext _appEFContext;
        private readonly IMapper _mapper;

        public CategoryController(AppEFContext appEFContext, IMapper mapper)
        {
            _appEFContext = appEFContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var list = await _appEFContext.Categories
                .Where(c => !c.IsDeleted)
                .Select(x => _mapper.Map<CategoryItemViewModel>(x))
                .ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateViewModel model)
        {
            var category = _mapper.Map<CategoryEntity>(model);
            await _appEFContext.Categories.AddAsync(category);
            await _appEFContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] CategoryEditViewModel model)
        {
            var category = _appEFContext.Categories
                .Where(c => !c.IsDeleted)
                .SingleOrDefault(x => x.Id == model.Id);
            if (category == null)
            {
                return NotFound();
            }
            category.Name = model.Name;
            category.Description = model.Description;
            await _appEFContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = _appEFContext.Categories
                .Where(c => !c.IsDeleted)
                .SingleOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            category.IsDeleted = true;
            await _appEFContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _appEFContext.Categories
                .Where(c => !c.IsDeleted)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CategoryItemViewModel>(category));
        }
    }
}
