using WebCore.Domain.Catalog;

namespace WebCore.Infrastructures.Services.Interfaces.Article
{
    public interface IArticleService
    {
        IList<Articles> GetSuggestion();
        IList<Articles> AdminGetAll();
        IList<Articles> GetByArticleCategory(int Id);
        Articles GetById(int Id);
        Articles GetByNext(int Id);
        Articles GetByPre(int Id);

        int Insert(Articles articleCategory);
        int Update(Articles articleCategory);
        int Delete(int id);
    }
}
