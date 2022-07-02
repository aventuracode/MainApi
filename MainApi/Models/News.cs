﻿namespace MainApi.Models
{
    public class News
    {
        public int IdArticle { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime publishedAt { get; set; }
        public string Content { get; set; }
    }
}
