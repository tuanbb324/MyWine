using System;
namespace WebCore.Areas.Admin.Models
{
	public class EditCategoryViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public string? SeoUrl { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaKeywords { get; set; }
        public string? MetaDescription { get; set; }
        public bool Published { get; set; }
        public int ParentCategoryId { get; set; }
        public bool IsShowMenu { get; set; }
        public bool IsShowHome { get; set; }
        public string? Image { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}

