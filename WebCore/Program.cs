using Microsoft.EntityFrameworkCore;

using WebCore.Infrastructures.Repositories;
using WebCore.Infrastructures.Services;
using WebCore.Infrastructures.Services.Catalog;
using WebCore.Infrastructures.Services.Interfaces.Article;
using WebCore.Infrastructures.Services.Interfaces.Catalog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer("Data Source=localhost; Database=wine_db; Trusted_Connection=False; Encrypt=false;"));
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IArticleCategoryService, ArticleCategoryService>();
builder.Services.AddScoped<ISpecificationService, SpecificationService>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1800);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddControllersWithViews();

var app = builder.Build();

{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    app.MapControllerRoute(name: "danh-muc-bai-viet",
                pattern: "danh-muc-bai-viet/{slug}-{id}",
                defaults: new { controller = "Article", action = "Index" });
    app.MapControllerRoute(name: "bai-viet",
                pattern: "bai-viet/{slug}-{id}",
                defaults: new { controller = "Article", action = "Detail" });
    app.MapControllerRoute(name: "danh-muc-san-pham",
                pattern: "danh-muc-san-pham/{slug}-{id}",
                defaults: new { controller = "Product", action = "Category" });
    app.MapControllerRoute(name: "blog",
                pattern: "san-pham/{slug}-{id}",
                defaults: new { controller = "Product", action = "Detail" });
    app.MapControllerRoute(name: "tim-kiem-san-pham",
                pattern: "tim-kiem-san-pham",
                defaults: new { controller = "Product", action = "Search" });

    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();

