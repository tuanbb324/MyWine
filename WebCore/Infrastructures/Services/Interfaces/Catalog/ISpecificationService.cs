
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Domain.Catalog;

namespace WebCore.Infrastructures.Services.Interfaces.Catalog
{
    public interface ISpecificationService
    {
        /// <summary>
        /// Get all specifications
        /// </summary>
        /// <returns>List of specification entities</returns>
        IList<Specification> GetAllSpecifications();

        /// <summary>
        /// Get specification by id
        /// </summary>
        /// <param name="id">Specification id</param>
        /// <returns>Specification entity</returns>
        Specification GetSpecificationById(int id);

        /// <summary>
        /// Insert specification
        /// </summary>
        /// <param name="specification">Specification entity</param>
        void InsertSpecification(Specification specification);

        /// <summary>
        /// Update specification
        /// </summary>
        /// <param name="specification">Specification entity</param>
        void UpdateSpecification(Specification specification);

        /// <summary>
        /// Delete specifications
        /// </summary>
        /// <param name="ids">List of specification ids</param>
        void DeleteSpecifications(IList<int> ids);

        /// <summary>
        /// Insert product specification mappings
        /// </summary>
        /// <param name="productSpecificationMappings">Product specification mappings</param>
        void InsertProductSpecificationMappings(IList<ProductSpecificationMapping> productSpecificationMappings);

        /// <summary>
        /// Delete all product specification by product id
        /// </summary>
        /// <param name="productId">Product id</param>
        void DeleteAllProductSpecificationMappings(int productId);

        Specification GetById(int Id);
        int Insert(Specification articleCategory);
        int InsertAndUpdates(List<Specification> articleCategory);
        int Update(Specification articleCategory);
        int Delete(Specification articleCategory);
    }
}
