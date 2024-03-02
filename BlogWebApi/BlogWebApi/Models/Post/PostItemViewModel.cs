using BlogWebApi.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BlogWebApi.Models.Tag;
using BlogWebApi.Models.Category;

namespace BlogWebApi.Models.Post
{
    public class PostItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Meta { get; set; }
        public string UrlSlug { get; set; }
        public virtual bool Published { get; set; }
        public virtual DateTime PostedOn { get; set; }
        public virtual DateTime? Modified { get; set; }
        public virtual CategoryItemViewModel Category { get; set; }
        public virtual List<TagItemViewModel> Tags { get; set; }
    }
}
