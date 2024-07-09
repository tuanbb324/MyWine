using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Infrastructures.Services.Interfaces.Catalog;
using WebCore.Domain.Catalog;
using WebCore.Infrastructures.Repositories.Infrastructure.Core.Repositories;
using WebCore.Infrastructures.Repositories;

namespace WebCore.Infrastructures.Services.Catalog
{
    public class ImageManagerService : BaseServiceV2, IImageManagerService
    {
        #region Fields

        private readonly GenericRepository<Image> _imageRepository;
        private readonly GenericRepository<ProductImageMapping> _productImagesRepository;


        #endregion

        #region Constructor

        public ImageManagerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _imageRepository = unitOfWork.RepositoryDbWrite<Image>(); ;
            _productImagesRepository = unitOfWork.RepositoryDbWrite<ProductImageMapping>(); ;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all images
        /// </summary>
        /// <returns>List of image entities</returns>
        public IList<Image> GetAllImages()
        {
            var entites = _imageRepository.GetAll()
                .OrderBy(x => x.FileName)
                .ToList();

            return entites;
        }

        /// <summary>
        /// Get Image using Id
        /// </summary>
        /// <param name="id">Image id</param>
        /// <returns>Image entity</returns>
        public Image GetImageById(int id)
        {
            return _imageRepository.GetById(id);
        }

        /// <summary>
        /// Search images
        /// </summary>
        /// <param name="keyword">keyword</param>
        /// <returns>List of image entities</returns>
        public IList<Image> SearchImages(string keyword)
        {
            return _imageRepository.GetAll().Where(x => x.FileName.Contains(keyword))
                .OrderBy(x => x.FileName)
                .ToList();
        }

        /// <summary>
        /// Insert image
        /// </summary>
        /// <param name="images">List of image entities to insert</param>
        public void InsertImages(IList<Image> images)
        {
            if (images == null)
                throw new ArgumentNullException("images");

            foreach (var image in images)
                _imageRepository.Add(image);

            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Delete image
        /// </summary>
        /// <param name="ids">Ids of image entities to delete</param>
        public void DeleteImages(IList<int> ids)
        {
            if (ids == null)
                throw new ArgumentNullException("ids");

            foreach (var id in ids)
                _imageRepository.Remove(id);

            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Insert product image mapping
        /// </summary>
        /// <param name="productImageMappings">List of product image mapping</param>
        public void InsertProductImageMappings(IList<ProductImageMapping> productImageMappings)
        {
            if (productImageMappings == null)
                throw new ArgumentNullException("productImageMappings");

            foreach (var mapping in productImageMappings)
                _productImagesRepository.Add(mapping);

            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Delete product image mapping
        /// </summary>
        /// <param name="productId">Product id</param>
        public void DeleteAllProductImageMappings(int productId)
        {
            if (productId == null)
                throw new ArgumentNullException("productId");

            var mappings = _productImagesRepository.GetAll().Where(x => x.ProductId == productId);

            foreach (var mapping in mappings)
                _productImagesRepository.Remove(mapping.Id);

            _unitOfWork.SaveChanges();
        }

        #endregion
    }
}
