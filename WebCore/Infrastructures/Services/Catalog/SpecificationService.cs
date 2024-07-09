using WebCore.Domain.Catalog;
using WebCore.Infrastructures.Repositories;
using WebCore.Infrastructures.Repositories.Infrastructure.Core.Repositories;
using WebCore.Infrastructures.Services;
using WebCore.Infrastructures.Services.Interfaces.Catalog;

namespace WebCore.Infrastructures.Services.Catalog
{
    public class SpecificationService : BaseServiceV2, ISpecificationService
    {
        #region Fields

        private readonly GenericRepository<Specification> _specificationRepository;
        private readonly GenericRepository<ProductSpecificationMapping> _productSpecificationMappingRepository;

        #endregion

        #region Constructor

        public SpecificationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _specificationRepository = unitOfWork.RepositoryDbWrite<Specification>();
            _productSpecificationMappingRepository = unitOfWork.RepositoryDbWrite<ProductSpecificationMapping>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all specifications
        /// </summary>
        /// <returns>List of specification entities</returns>
        public IList<Specification> GetAllSpecifications()
        {
            var entities = _specificationRepository.GetAll()
                .OrderBy(x => x.Name)
                .ToList();

            return entities;
        }

        /// <summary>
        /// Get specification by id
        /// </summary>
        /// <param name="id">Specification id</param>
        /// <returns>Specification entity</returns>
        public Specification GetSpecificationById(int id)
        {
            return _specificationRepository.GetById(id);
        }

        /// <summary>
        /// Insert specification
        /// </summary>
        /// <param name="specification">Specification entity</param>
        public void InsertSpecification(Specification specification)
        {
            if (specification == null)
                throw new ArgumentNullException("specification");

            _specificationRepository.Add(specification);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Update specification
        /// </summary>
        /// <param name="specification">Specification entity</param>
        public void UpdateSpecification(Specification specification)
        {
            if (specification == null)
                throw new ArgumentNullException("specification");

            _specificationRepository.Update(specification);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Delete specifications
        /// </summary>
        /// <param name="ids">List of specification ids</param>
        public void DeleteSpecifications(IList<int> ids)
        {
            if (ids == null)
                throw new ArgumentNullException("specifications");

            foreach (var id in ids)
                _specificationRepository.Remove(id);

            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Insert product specification mappings
        /// </summary>
        /// <param name="productSpecificationMappings">Product specification mappings</param>
        public void InsertProductSpecificationMappings(IList<ProductSpecificationMapping> productSpecificationMappings)
        {
            if (productSpecificationMappings == null)
                throw new ArgumentNullException("productSpecificationMappings");

            foreach (var mapping in productSpecificationMappings)
                _productSpecificationMappingRepository.Add(mapping);

            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Delete all product specification by product id
        /// </summary>
        /// <param name="productId">Product id</param>
        public void DeleteAllProductSpecificationMappings(int productId)
        {
            if (productId == null)
                throw new ArgumentNullException("productId");

            var mappings = _productSpecificationMappingRepository.GetAll().Where(x => x.ProductId == productId);

            foreach (var mapping in mappings)
                _productSpecificationMappingRepository.Remove(mapping.Id);

            _unitOfWork.SaveChanges();
        }

        public int Delete(Specification articleCategory)
        {
            var art = GetById(articleCategory.Id);
            _specificationRepository.Update(art);
            var res = _unitOfWork.SaveChanges();
            return res;
        }

        public Specification GetById(int Id)
        {
            return _specificationRepository.GetById(Id);
        }

        public int Insert(Specification articleCategory)
        {
            _specificationRepository.Add(articleCategory);
            var res = _unitOfWork.SaveChanges();
            return res;
        }

        public int Update(Specification articleCategory)
        {
            _specificationRepository.Update(articleCategory);
            var res = _unitOfWork.SaveChanges();
            return res;
        }

        public int InsertAndUpdates(List<Specification> articleCategory)
        {
            foreach (var item in articleCategory)
            {
                if (item.Id > 0)
                    _specificationRepository.Update(item);
                else
                    _specificationRepository.Add(item);
            }

            var res = _unitOfWork.SaveChanges();
            return res;
        }

        #endregion
    }
}
