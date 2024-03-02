﻿namespace BlogWebApi.Models.Post
{
    public class PostEditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Meta { get; set; }
        public string UrlSlug { get; set; }
        public virtual bool Published { get; set; }
        public int CategoryId { get; set; }
        public List<int> Tags { get; set; }
    }
}
