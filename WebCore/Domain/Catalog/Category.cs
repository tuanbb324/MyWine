using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Domain.Catalog
{
    public class Category:IAggregateRoot
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public string? SeoUrl { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaKeywords { get; set; }
        public string? MetaDescription { get; set; }

        public bool Published { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }
        public int ParentCategoryId { get; set; }
        public bool IsShowMenu { get; set; }
        public bool IsShowHome { get; set; }
        public string? Image { get; set; }
        public virtual ICollection<ProductCategoryMapping> Categories { get; set; }
    }
}
