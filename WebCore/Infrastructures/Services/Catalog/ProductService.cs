
using Microsoft.EntityFrameworkCore;

using WebCore.Infrastructures.Services.Interfaces.Catalog;
using WebCore.Domain.Catalog;
using WebCore.Infrastructures.Services;
using WebCore.Infrastructures.Repositories.Infrastructure.Core.Repositories;
using WebCore.Infrastructures.Repositories;

namespace WebCore.Infrastructures.Services.Catalog
{
    public class ProductService : BaseServiceV2, IProductService
    {
        #region Fields

        private readonly ApplicationDbContext _context;
        private readonly GenericRepository<Product> _productRepository;
        private readonly GenericRepository<ProductCategoryMapping> _prodcutCategoryRepository;

        #endregion

        #region Constructor

        public ProductService(IUnitOfWork unitOfWork, ApplicationDbContext context) : base(unitOfWork)
        {
            _productRepository = unitOfWork.RepositoryDbWrite<Product>();
            _prodcutCategoryRepository = unitOfWork.RepositoryDbWrite<ProductCategoryMapping>();
            _context = context;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>List of product entities</returns>
        public IList<Product> GetAllProducts()
        {
            // TODO: update when lazy loading is available
            var entities = _context.Products.Where(x=>x.IsDeleted ==false)
                .Include(x => x.Categories).ThenInclude(x => x.Category)

                .Include(x => x.Manufacturers).ThenInclude(x => x.Manufacturer)
                .Include(x => x.Specifications).ThenInclude(x => x.Specification)
                .AsNoTracking()
                .ToList();

            return entities;
        }

        /// <summary>
        /// Get product using id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Product entity</returns>
        public Product GetProductById(int id)
        {
            if (id == null)
                return null;

            var e = _context.Products.Where(x => x.IsDeleted == false).ToList();
            // TODO: update when lazy loading is available
            var entity = _context.Products
                .Include(x => x.Categories).ThenInclude(x => x.Category)
                //.Include(x => x.Images).ThenInclude(x => x.Image)
                .Include(x => x.Manufacturers).ThenInclude(x => x.Manufacturer)
                .Include(x => x.Specifications).ThenInclude(x => x.Specification)
                .AsNoTracking()
                .SingleOrDefault(x => x.Id == id );

            return entity;
        }

        /// <summary>
        /// Get product using categoryId
        /// </summary>
        /// <param name="categoryId">CategoryId</param>
        /// <returns>Product entity</returns>
        public IList<Product> GetProductByCategoryId(int categoryId)
        {
            // TODO: update when lazy loading is available
            var entity = _context.Products.Where(x => x.IsDeleted == false)
                .Include(x => x.Categories).ThenInclude(x => x.Category)

                .Include(x => x.Manufacturers).ThenInclude(x => x.Manufacturer)
                .Include(x => x.Specifications).ThenInclude(x => x.Specification)
                .AsNoTracking()
                .Where(x => x.Categories.Any(c => c.CategoryId == categoryId) && x.Published).ToList();

            return entity;
        }
        /// <summary>
        /// Insert product
        /// </summary>
        /// <param name="product">Product entity</param>
        public int InsertProduct(Product product, List<int> CategoryId)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            _productRepository.Add(product);



            var res = _unitOfWork.SaveChanges();
            if (res > 0)
            {

                foreach (var item in CategoryId)
                {
                    ProductCategoryMapping category = new ProductCategoryMapping()
                    {
                        CategoryId = item,
                        ProductId = product.Id

                    };
                    _prodcutCategoryRepository.Add(category);
                }
                res = _unitOfWork.SaveChanges();
                if (res < 1)
                {

                    _productRepository.Remove(product.Id);
                    _unitOfWork.SaveChanges();
                }
            }
            return res;
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="product">Product entity</param>
        public int UpdateProduct(Product product, List<int> categoryId)
        {

            var entity = _productRepository.GetById(product.Id);

            entity.Name = product.Name;
            entity.Description = product.Description;
            entity.Content = product.Content;
            entity.SeoUrl = product.SeoUrl;
            entity.MetaKeywords = product.MetaKeywords;
            entity.Published = product.Published;
            entity.Images = product.Images;
            entity.Note = product.Note;
            entity.Enjoy = product.Enjoy;
            entity.DateModified = DateTime.Now;
            entity.BasicInformation = product.BasicInformation;
            _productRepository.Update(entity);
            var res = _unitOfWork.SaveChanges();
            if (res > 0)
            {

                var data = _prodcutCategoryRepository.GetAll().Where(x => x.ProductId == entity.Id).ToList();
                foreach (var item in data)
                {
                    _prodcutCategoryRepository.Remove(item.Id);
                }
                foreach (var item in categoryId)
                {

                    ProductCategoryMapping category = new ProductCategoryMapping()
                    {
                        CategoryId = item,
                        ProductId = product.Id

                    };
                    _prodcutCategoryRepository.Add(category);
                }
                res = _unitOfWork.SaveChanges();
                if (res < 1)
                {

                    //_productRepository.Remove(entity.Id);
                    //_unitOfWork.SaveChanges();
                }
            }
            return res;
        }

        /// <summary>
        /// Delete products
        /// </summary>
        /// <param name="ids">Ids of product to delete</param>
        public int DeleteProduct(int id)
        {
            var entity = _productRepository.GetById(id);
            entity.IsDeleted = true;
            _productRepository.Update(entity);
           return  _unitOfWork.SaveChanges();
        }

        public IList<Product> SearchProduct(
            string nameFilter = null,
            string seoFilter = null,
            string[] categoryFilter = null,
            string[] manufacturerFilter = null,
            string[] priceFilter = null,
            bool isPublished = true)
        {
            var result = _context.Products.Where(x => x.IsDeleted == false)
                .Include(x => x.Categories).ThenInclude(x => x.Category)
                //.Include(x => x.Images).ThenInclude(x => x.Image)
                .Include(x => x.Manufacturers).ThenInclude(x => x.Manufacturer)
                .Include(x => x.Specifications).ThenInclude(x => x.Specification)
                .AsNoTracking();

            // published filter
            if (isPublished == false)
            {
                result = result.Where(x => x.Published == false);
            }

            // name filter
            if (nameFilter != null && nameFilter.Length > 0)
            {
                result = result.Where(x => x.Name.ToLower().Contains(nameFilter.ToLower()));
            }

            // seo filter
            if (seoFilter != null && seoFilter.Length > 0)
            {
                throw new NotImplementedException();
            }

            // category filter
            if (categoryFilter != null && categoryFilter.Length > 0)
            {
                result = result.Where(x => x
                    .Categories.Select(c => c.Category.Name.ToLower())
                    .Intersect(categoryFilter.Select(cf => cf.ToLower()))
                    .Count() > 0
                );
            }

            // manufacturer filter
            if (manufacturerFilter != null && manufacturerFilter.Length > 0)
            {
                result = result.Where(x => x
                    .Manufacturers
                    .Select(c => c.Manufacturer.Name.ToLower())
                    .Intersect(manufacturerFilter.Select(mf => mf.ToLower()))
                    .Count() > 0
                );
            }

            // price filter
            if (priceFilter != null && priceFilter.Length > 0)
            {
                var tmpResult = new List<Product>();
                foreach (var price in priceFilter)
                {
                    var p = price.Split('-');
                    int minPrice = Int32.Parse(p[0]);
                    int maxPrice = Int32.Parse(p[1]);

                    var r = result;
                    if (r.Count() > 0) tmpResult.AddRange(r);
                }
                result = tmpResult.AsQueryable();
            }

            return result.ToList();
        }

        public IList<Product> AdminGetAll()
        {
            var entity = _productRepository.GetAll().Where(x => x.IsDeleted == false).ToList();
            return entity;
        }

        #endregion
    }
}
