using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Domain.Catalog
{
    public class Image: IAggregateRoot
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
    }
}
