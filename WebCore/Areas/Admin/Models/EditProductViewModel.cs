using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Areas.Admin.Models
{
    public class EditProductViewModel
    {

        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string SeoUrl { get; set; }
        public string MetaKeywords { get; set; }
        public string? BasicInformation { get; set; }
        
        public bool Published { get; set; }
        public string? Content { get; set; }
        public List<int> CategoryId { get; set; }
        
        public List<string> Images { get; set; }
        public List<EditProductSpecificationViewModel> Specifitions { get; set; }
        public string? Image { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
        public string? Image4 { get; set; }
        public string? Note { get; set; }
        public string? Enjoy { get; set; }

    }
}
