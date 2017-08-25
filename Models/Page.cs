using System.ComponentModel.DataAnnotations;
using MDW.Entity;
using System.Collections.Generic;

namespace MDW.Models
{
    public class PageListModel
    {
        public List<PageModel> Pages { get; set; }

        public PageListModel()
        {
            Pages = new List<PageModel>();
        }
    }

    public class PageModel
    {
        public List<GroupModel> Groups { get; set; }

        public bool Authorized { get; set; }

        public string AbsoluteUrl { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public string Group { get; set; }

        public string Html { get; set; }

        public string Markdown { get; set; }

        public PageModel()
        {
            Groups = new List<GroupModel>();
        }
    }

    public class CreatePageModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Group { get; set; }
    }

    public class UpdatePageModel
    {
        [Required]
        public string Url { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Group { get; set; }

        [Required]
        public string Body { get; set; }
    }

    public class DeletePageModel
    {
        [Required]
        public string Url { get; set; }
    }
}