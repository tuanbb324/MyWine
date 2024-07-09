using WebCore.Domain.Catalog;

namespace WebCore.Infrastructures.Services.Interfaces.Article
{
    public interface IArticleCategoryService
    {
        IList<ArticleCategory> GetAll();
        ArticleCategory GetById(int Id);

        int Insert(ArticleCategory articleCategory);
        int Update(ArticleCategory articleCategory);
        int Delete(int id);

    }
}
