using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Domain.Catalog
{
    public class ProductManufacturerMapping: IAggregateRoot
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ManufacturerId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
    }
}
