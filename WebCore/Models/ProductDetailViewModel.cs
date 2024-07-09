using WebCore.Domain.Catalog;

namespace WebCore.Models;

public class ProductDetailViewModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double? Price { get; set; }
    public double? SpecialPrice { get; set; }
    //public DateTime? SpecialPriceStartDate { get; set; }
    //public DateTime? SpecialPriceEndDate { get; set; }

    //public int StockQuantity { get; set; }
    //public int MinimumStockQuantity { get; set; }
    //public int NotifyForQuantityBelow { get; set; }
    //public bool DisplayAvailability { get; set; }
    //public int MinimumCartQuantity { get; set; }
    //public int MaximumCartQuantity { get; set; }

    public string? Content { get; set; }
    public string SeoUrl { get; set; }
    public string MetaTitle { get; set; }
    public string MetaKeywords { get; set; }
    public string MetaDescription { get; set; }
    public string? Note { get; set; }
    public string? Enjoy { get; set; }
    public string? BasicInformation { get; set; }
    
    public IList<ProductSpecificationMapping> Specifications { get; set; } = new List<ProductSpecificationMapping>();
    public List<string> Images { get; set; }
    public List<Category> Categories { get; set; }
    public IList<Product> ProductSuggestions { get; set; } = new List<Product>();
    public ProductDetailViewModel(Product p)
    {
        Id = p.Id;
        Name = p.Name;
        Description = p.Description;
        SeoUrl = p.SeoUrl;
        MetaKeywords = p.MetaKeywords;
        Images = p.ImageUrls;
        Content = p.Content;
        Note = p.Note;
        Enjoy = p.Enjoy;
        BasicInformation = p.BasicInformation;
    }
    public ProductDetailViewModel() { }
}

