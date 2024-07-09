using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace WebCore.Domain.Catalog
{
    public class Product : IAggregateRoot
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }

        public string? SeoUrl { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaKeywords { get; set; }
        public string? MetaDescription { get; set; }

        public string? BasicInformation { get; set; }
        
        public bool Published { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }
        public string? Content { get; set; }
        public string? Image {

            get
            {
              
                if (!string.IsNullOrWhiteSpace(Images))
                {
                    var list = new List<string>();
                    list = JsonConvert.DeserializeObject<List<string>>(Images);
                     return list.FirstOrDefault();
                }
                return "";
            }
        }
        public string? Image2
        {

            get
            {

                if (!string.IsNullOrWhiteSpace(Images))
                {
                    var list = new List<string>();
                    list = JsonConvert.DeserializeObject<List<string>>(Images);
                    if (list.Count>1)
                    {
                        return list[1];
                    }
                   
                }
                return "";
            }
        }
        public string? Image3
        {

            get
            {

                if (!string.IsNullOrWhiteSpace(Images))
                {
                    var list = new List<string>();
                    list = JsonConvert.DeserializeObject<List<string>>(Images);
                    if (list.Count > 2)
                    {
                        return list[2];
                    }
                }
                return "";
            }
        }
        public string? Image4
        {

            get
            {

                if (!string.IsNullOrWhiteSpace(Images))
                {
                    var list = new List<string>();
                    list = JsonConvert.DeserializeObject<List<string>>(Images);
                    if (list.Count > 3)
                    {
                        return list[3];
                    }
                }
                return "";
            }
        }

        public string? Images { get; set; }
        public string? Note { get; set; }
        public string? Enjoy { get; set; }


        [NotMapped]
        public List<string> ImageUrls
        {

            get
            {
              
                if (!string.IsNullOrWhiteSpace(Images))
                {
                    var list = new List<string>();
                    list = JsonConvert.DeserializeObject<List<string>>(Images);
                    return list;
                }
              return new List<string>();
            }
        }
        public virtual ICollection<ProductCategoryMapping> Categories { get; set; }
        //public virtual ICollection<ProductImageMapping> Images { get; set; }
        public virtual ICollection<ProductManufacturerMapping> Manufacturers { get; set; }
        public virtual ICollection<ProductSpecificationMapping> Specifications { get; set; }
    }
}
