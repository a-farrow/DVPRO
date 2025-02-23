﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DVPRO.DATA.EF.Models;
using Microsoft.AspNetCore.Authorization;

namespace DVPRO.UI.MVC.Controllers
{
    [Authorize(Roles = "Accounting, Admin")]
    public class ProductsController : Controller
    {
        private readonly AtomicContext _context;

        public ProductsController(AtomicContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var atomicContext = _context.Products.Include(p => p.ProductStatus).Include(p => p.ProductType).Include(p => p.Vendor);
            return View(await atomicContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductStatus)
                .Include(p => p.ProductType)
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ProductStatusId"] = new SelectList(_context.ProductStatuses, "ProductStatusId", "ProductStatusName");
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "ProductTypeName");
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Description,CostPerUnit,PricePerUnit,UnitType,UnitsInStock,UnitsOnOrder,ProductStatusId,ProductTypeId,VendorId,ProductImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductStatusId"] = new SelectList(_context.ProductStatuses, "ProductStatusId", "ProductStatusName", product.ProductStatusId);
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "ProductTypeName", product.ProductTypeId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName", product.VendorId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductStatusId"] = new SelectList(_context.ProductStatuses, "ProductStatusId", "ProductStatusName", product.ProductStatusId);
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "ProductTypeName", product.ProductTypeId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorAddress", product.VendorId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Description,CostPerUnit,PricePerUnit,UnitType,UnitsInStock,UnitsOnOrder,ProductStatusId,ProductTypeId,VendorId,ProductImage")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductStatusId"] = new SelectList(_context.ProductStatuses, "ProductStatusId", "ProductStatusName", product.ProductStatusId);
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "ProductTypeName", product.ProductTypeId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorAddress", product.VendorId);
            return View(product);
        }

        #region Delete
        //// GET: Products/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Products == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products
        //        .Include(p => p.ProductStatus)
        //        .Include(p => p.ProductType)
        //        .Include(p => p.Vendor)
        //        .FirstOrDefaultAsync(m => m.ProductId == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Products == null)
        //    {
        //        return Problem("Entity set 'AtomicContext.Products'  is null.");
        //    }
        //    var product = await _context.Products.FindAsync(id);
        //    if (product != null)
        //    {
        //        _context.Products.Remove(product);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        #endregion

        //Write the AjaxDelete Action
        [AcceptVerbs("POST")]
        public JsonResult AjaxDelete(int id)
        {
            Product product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();

            string confirmMessage = $"Deleted the product {product.ProductName} from the database";

            return Json(new { id = id, message = confirmMessage });
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
