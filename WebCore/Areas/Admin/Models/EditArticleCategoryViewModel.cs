using System;
namespace WebCore.Areas.Admin.Models
{
	public class EditArticleCategoryViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Decsription { get; set; }

        public string? SeoUrl { get; set; }
        public string? Keywords { get; set; }

        public bool Published { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }

        public string? Image { get; set; }
    }
}

