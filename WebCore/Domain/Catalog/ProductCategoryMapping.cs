using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Domain.Catalog;

namespace WebCore.Domain.Catalog
{
    public class ProductCategoryMapping: IAggregateRoot
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Category Category { get; set; }
    }
}
