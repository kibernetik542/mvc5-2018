using MvcTWA.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MvcTWA.Controllers
{
    public class CategoryController : Controller
    {
        NorthWindDataModel ctx = new NorthWindDataModel();
        // GET: Category
        public async Task<ActionResult> Index()
        {
            var cat = await ctx.Categories.ToListAsync();
            return View(cat);
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(Category cat)
        {
            ctx.Categories.Add(cat);
            await ctx.SaveChangesAsync();
            Thread.Sleep(1500);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Category cat = ctx.Categories.FirstOrDefault(x => x.CategoryID == id);
            ctx.Categories.Remove(cat);
            ctx.SaveChanges();
            Thread.Sleep(1500);
            return RedirectToAction("Index");
        }

    }
}