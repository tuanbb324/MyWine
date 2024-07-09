using WebCore.Domain.Catalog;

namespace WebCore.Models;

public class ProductViewModel
{
    public IList<Product> Products{ get; set; }
    public IList<Category> Category{ get; set; }

    public IDictionary<Category, int> Categories { get; set; }
    public IList<HomeCategoryViewModel> CategoryViewModels { get; set; } = new List<HomeCategoryViewModel>();
    public IList<Articles> Articles { get; set; }
    
}

