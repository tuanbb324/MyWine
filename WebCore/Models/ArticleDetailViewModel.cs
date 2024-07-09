using WebCore.Domain.Catalog;

namespace WebCore.Models;

public class ArticleDetailViewModel
{
    public Articles Articles { get; set; }
    public ArticleCategory ArticleCategory { get; set; }

    public IList<ArticleCategory> ArticleCategories { get; set; }
    public IList<Articles> ArticleSuggesion { get; set; }
    public Articles ArticleNext { get; set; }
    public Articles ArticlePre { get; set; }
}

