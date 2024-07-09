

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebCore.Domain.Catalog;
using WebCore.Infrastructures.Repositories;
using WebCore.Infrastructures.Repositories.Infrastructure.Core.Repositories;
using WebCore.Infrastructures.Services.Interfaces.Article;
using WebCore.Infrastructures.Services.Interfaces.Catalog;

namespace WebCore.Infrastructures.Services.Catalog
{
    public class ArticleCategoryService : BaseServiceV2, IArticleCategoryService
    {
        #region Fields



        #endregion

        #region Constructor

        private readonly GenericRepository<ArticleCategory> _articleCategoryRepository;
        private readonly GenericRepository<Articles> _articleRepository;

        public ArticleCategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

            _articleCategoryRepository = unitOfWork.RepositoryDbWrite<ArticleCategory>();
            _articleRepository = unitOfWork.RepositoryDbWrite<Articles>();

            //_giftRepositoryWrite = _unitOfWork.RepositoryDbWrite<Gifts>();
            //_giftCategoryRepositoryWrite = _unitOfWork.RepositoryDbWrite<GiftCategories>();
        }

        

        public IList<ArticleCategory> GetAll()
        {
            var data = _articleCategoryRepository.GetAll().ToList();
            foreach (var item in data)
            {
                item.ArticleCount = _articleRepository.GetAll().Where(d => d.ArticleCategoryId == item.Id).Count();
            }
            return data;
        }

        public ArticleCategory GetById(int Id)
        {
            return _articleCategoryRepository.GetById(Id);
        }

        public int Insert(ArticleCategory articleCategory)
        {
            _articleCategoryRepository.Add(articleCategory);
             var res = _unitOfWork.SaveChanges();
            return res;
        }

        public int Update(ArticleCategory articleCategory)
        {
            _articleCategoryRepository.Update(articleCategory);
            var res = _unitOfWork.SaveChanges();
            return res;
        }
        public int Delete(int id)
        {
            var entity = _articleCategoryRepository.GetById(id);
            entity.IsDeleted = true;
            _articleCategoryRepository.Update(entity);
            return _unitOfWork.SaveChanges();
        }
        #endregion


    }
}
