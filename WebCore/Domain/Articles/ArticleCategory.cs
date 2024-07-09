using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Domain.Catalog
{
    public class ArticleCategory : IAggregateRoot
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Decsription { get; set; }

        public string? SeoUrl { get; set; }
        public string? Keywords { get; set; }

        public bool Published { get; set; }
        public bool IsDeleted { get; set; }
        
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
      
        public string? Image { get; set; }

        [NotMapped]
        public int ArticleCount { get; set; } = 0;
    }
}
