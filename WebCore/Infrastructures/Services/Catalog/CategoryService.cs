

using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebCore.Domain.Catalog;
using WebCore.Domain.Dto;
using WebCore.Infrastructures.Repositories;
using WebCore.Infrastructures.Repositories.Infrastructure.Core.Repositories;

using WebCore.Infrastructures.Services.Interfaces.Catalog;

namespace WebCore.Infrastructures.Services.Catalog
{
    public class CategoryService : BaseServiceV2, ICategoryService
    {
        #region Fields


        private readonly GenericRepository<Category> _categoryRepository;
        private readonly GenericRepository<Product> _productRepository;
        private readonly GenericRepository<ProductCategoryMapping> _productCategoryMappingRepository;
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructor



        public CategoryService(IUnitOfWork unitOfWork, ApplicationDbContext context) : base(unitOfWork)
        {

            _categoryRepository = unitOfWork.RepositoryDbWrite<Category>();
            _productRepository = unitOfWork.RepositoryDbWrite<Product>();
            _productCategoryMappingRepository = unitOfWork.RepositoryDbWrite<ProductCategoryMapping>();
            _context = context;
            //_giftRepositoryWrite = _unitOfWork.RepositoryDbWrite<Gifts>();
            //_giftCategoryRepositoryWrite = _unitOfWork.RepositoryDbWrite<GiftCategories>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>List of category entities</returns>
        public IList<Category> GetAllCategories()
        {
            var entities = _categoryRepository.GetAll()
                .OrderBy(x => x.Name)
                .ToList();

            return entities;
        }
        public Category GetAllCategoryAdmin(int id)
        {
            var entities = _categoryRepository.GetById(id);
            return entities;
        }
        public IList<Category> GetAllCategory()
        {
            var categoryNames = _categoryRepository.GetAll().Where(c => c.Published && c.IsShowMenu && c.IsDeleted == false).ToList();

            return categoryNames;
        }
        public IList<Category> GetAllimage()
        {
            var category = _categoryRepository.GetAll()
       .Where(c => c.Published && c.ParentCategoryId == 0 && c.IsDeleted==false)
       .Take(6).ToList();

            return category;
        }

        public List<Category> GetCategoryId()
        {
            var categoryId = _categoryRepository.GetAll().Where(c => c.Published && c.IsShowHome)
                .ToList();
            return categoryId;
        }

        public List<Product> GetProductsByCategoryId(int categoryId)
        {

            var productIds = _productCategoryMappingRepository.GetAll()
                .Where(mapping => mapping.Category.Id == categoryId)
                .Select(mapping => mapping.ProductId).ToList();
            var products = _productRepository.GetAll().Where(product => productIds.Contains(product.Id))
                .Take(10).ToList();
            return products;
        }

        public List<Product> GetListProducts()
        {
            var newProducts = _productRepository.GetAll()
                .Where(p => p.Published && p.DateAdded <= DateTime.Now)
                .OrderByDescending(p => p.DateAdded)
                .Take(10)
                .ToList();

            return newProducts;
        }
        public IList<Category> AdminGetAll()
        {
            var getAllCategory = _categoryRepository.GetAll().ToList();
            return getAllCategory;
        }

        public List<Category> GetCategoryByProductId(int Id)
        {
            var Ids = _productCategoryMappingRepository.GetAll().Where(x => x.ProductId == Id 
            ).Select(x => x.CategoryId).ToList();

            return _categoryRepository.GetAll().Where(c => Ids.Contains(c.Id) && c.Published).ToList();
        }

        public int Update(Category category)
        {
            var existingCategory = _categoryRepository.GetById(category.Id);
            existingCategory = category;
            _categoryRepository.Update(existingCategory);
            return existingCategory.Id;

        }
        public int Delete(int id)
        {
            var entity = _categoryRepository.GetById(id);
            entity.IsDeleted = true;
            _categoryRepository.Update(entity);
            return _unitOfWork.SaveChanges();
        }

        #endregion
    }
}