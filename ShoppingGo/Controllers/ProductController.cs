using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingGo.Models;
using ShoppingGo.Repositories;
using ShoppingGo.ViewModels;

namespace ShoppingGo.Controllers
{
    public class ProductController : Controller
    {
        private UnitOfWork unitOfWork;

        public ProductController()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public ProductController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET: Product
        public async Task<ActionResult> Index()
        {
            return View(await unitOfWork.ProductRepository.GetAsync());
        }

        // GET: Product/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await unitOfWork.ProductRepository.GetAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public async Task<ActionResult> Create()
        {
            var productViewModel = new ProductViewModel();
            productViewModel.Categories = await unitOfWork.CategoryRepository.GetAsync();
            return View(productViewModel);
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductId,Name,Price,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.ProductRepository.InsertAsync(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await unitOfWork.ProductRepository.GetAsync(id);
            var productViewModel = new ProductViewModel(product);
            productViewModel.Categories = await unitOfWork.CategoryRepository.GetAsync();        
        
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(productViewModel);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductId,Name,Price,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.ProductRepository.UpdateAsync(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await unitOfWork.ProductRepository.GetAsync(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await unitOfWork.ProductRepository.GetAsync(id);
            await unitOfWork.ProductRepository.DeleteAsync(product);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
