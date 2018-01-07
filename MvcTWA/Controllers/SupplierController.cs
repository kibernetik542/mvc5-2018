using MvcTWA.Models;
using MvcTWA.PagingFunctionality;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MvcTWA.Controllers
{
    public class SupplierController : Controller
    {
        NorthWindDataModel ctx = new NorthWindDataModel();
        public int PageSize = 10;

        //public async Task<ActionResult> Index()
        //{
        //    var suppliers = await ctx.Suppliers.ToListAsync();
        //    return View(suppliers);
        //}


        public async Task<ActionResult> Index(int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Suppliers = await ctx.Suppliers.OrderBy(p => p.SupplierID)
                .Skip((page - 1) * PageSize).Take(PageSize).ToListAsync(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = ctx.Suppliers.Count()
                }
            };
            return View(model);
        }

        [HttpPost]
        public string Delete(int id)
        {
            Supplier s = ctx.Suppliers.FirstOrDefault(x => x.SupplierID == id);
            ctx.Suppliers.Remove(s);
            try
            {
                ctx.SaveChanges();
                return "success";
            }
            catch (Exception)
            {
                return "failed";
            }
        }
    }
}