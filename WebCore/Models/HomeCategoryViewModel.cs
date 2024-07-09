using WebCore.Domain.Catalog;

namespace WebCore.Models;

public class HomeCategoryViewModel
{

    public Category Category { get; set; }
    public IList<Product> Products { get; set; }
    public IList<HomeCategoryViewModel> CategoryViewModels { get; set; } = new List<HomeCategoryViewModel>();
}

