using MvcTWA.Models;
using MvcTWA.PagingFunctionality;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MvcTWA.Controllers
{
    public class ProductsController : Controller
    {
        NorthWindDataModel ctx = new NorthWindDataModel();
        public int PageSize = 10;



        public async Task<ActionResult> Index(int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = await ctx.Products.OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize).Take(PageSize).ToListAsync(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = ctx.Products.Count()
                }
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            ViewBag.Categories = ctx.Categories.ToList();
            ViewBag.Suppliers = ctx.Suppliers.ToList();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            ctx.Products.Add(product);
            await ctx.SaveChangesAsync();
            Thread.Sleep(1500);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Product p = ctx.Products.FirstOrDefault(x => x.ProductID == id);
            Thread.Sleep(1500);
            return View(p);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Product p)
        {
            Product product = await ctx.Products
                .FirstOrDefaultAsync(x => x.ProductID == p.ProductID);

            ctx.Products.Remove(product);
            await ctx.SaveChangesAsync();
            Thread.Sleep(1500);
            return RedirectToAction("Index");
        }
    }
}