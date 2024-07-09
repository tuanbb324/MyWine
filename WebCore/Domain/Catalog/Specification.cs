using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Domain.Catalog
{
    public class Specification : IAggregateRoot
    {

        public int Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }
        public int Position { get; set; }

        public bool Published { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
