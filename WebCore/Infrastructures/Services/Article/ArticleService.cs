

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebCore.Domain.Catalog;
using WebCore.Infrastructures.Repositories;
using WebCore.Infrastructures.Repositories.Infrastructure.Core.Repositories;
using WebCore.Infrastructures.Services.Interfaces.Article;
using WebCore.Infrastructures.Services.Interfaces.Catalog;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace WebCore.Infrastructures.Services.Catalog
{
    public class ArticleService : BaseServiceV2, IArticleService
    {
        #region Fields



        #endregion

        #region Constructor

        private readonly GenericRepository<Articles> _artiRepository;
        
        public ArticleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _artiRepository = unitOfWork.RepositoryDbWrite<Articles>();
        }

      

        public IList<Articles> GetByArticleCategory(int Id)
        {
            return _artiRepository.GetAll().Where(x=>x.ArticleCategoryId ==Id && x.Published).OrderByDescending(x => x.DateModified).ToList();
        }

        public Articles GetById(int Id)
        {
            return _artiRepository.GetById(Id);
        }

        public Articles GetByNext(int Id)
        {
            return _artiRepository.GetAll().Where(x => x.Id > Id && x.Published).FirstOrDefault();
        }

        public Articles GetByPre(int Id)
        {
            return _artiRepository.GetAll().Where(x => x.Id < Id && x.Published).FirstOrDefault();
        }

        public IList<Articles> GetSuggestion()
        {
            return _artiRepository.GetAll().Where(x=>x.Published).OrderByDescending(x=>x.DateModified).ToList();
        }

        public int Insert(Articles articleCategory)
        {
            _artiRepository.Add(articleCategory);
            var res = _unitOfWork.SaveChanges();
            return res;
        }

        public int Update(Articles articleCategory)
        {
            var article = _artiRepository.GetById(articleCategory.Id);
            article.Image = articleCategory.Image;
            article.Name = articleCategory.Name;
            article.Decsription = articleCategory.Decsription;
            article.Content = articleCategory.Content;
            article.SeoUrl = articleCategory.SeoUrl;
            article.Keywords = articleCategory.Keywords; 
            article.ArticleCategoryId = articleCategory.ArticleCategoryId;
            article.Published = articleCategory.Published;
            article.DateModified = DateTime.Now;
            _artiRepository.Update(article);
            var res = _unitOfWork.SaveChanges();
            return res;
        }
        public IList<Articles> AdminGetAll()
        {
            var entity = _artiRepository.GetAll().ToList();
            return entity;
        }

        public int Delete(int id)
        {
            var entity = _artiRepository.GetById(id);
            entity.IsDeleted = true;
            _artiRepository.Update(entity);
            return _unitOfWork.SaveChanges();
        }
        #endregion


    }
}
