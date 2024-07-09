using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Areas.Admin.Models
{
    public class EditProductSpecificationViewModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
