using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Domain.Catalog
{
    public class ProductImageMapping: IAggregateRoot
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ImageId { get; set; }
        public int SortOrder { get; set; }
        public int Position { get; set; }

        public virtual Product Product { get; set; }
        public virtual Image Image { get; set; }
    }
}
