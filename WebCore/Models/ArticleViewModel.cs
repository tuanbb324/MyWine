using WebCore.Domain.Catalog;

namespace WebCore.Models;

public class ArticleViewModel
{
    public IList<Articles> Articles { get; set; }
    public ArticleCategory ArticleCategory { get; set; }
    public IList<ArticleCategory> ArticleCategories { get; set; }
    public IList<Articles> ArticleSuggesion { get; set; }
}

