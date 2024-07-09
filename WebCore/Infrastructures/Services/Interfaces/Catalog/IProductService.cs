
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Domain.Catalog;

namespace WebCore.Infrastructures.Services.Interfaces.Catalog
{
    public interface IProductService
    {
        IList<Product> GetAllProducts();
        Product GetProductById(int id);
        int InsertProduct(Product product, List<int> CategoryId);
        int UpdateProduct(Product product, List<int> categoryId);

        int DeleteProduct(int id);
        IList<Product> SearchProduct(
            string nameFilter = null,
            string seoFilter = null,
            string[] categoryFilter = null,
            string[] manufacturerFilter = null,
            string[] priceFilter = null,
            bool isPublished = true);

        IList<Product> GetProductByCategoryId(int categoryId);
        IList<Product> AdminGetAll();
    }
}
