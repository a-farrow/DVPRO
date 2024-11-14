using System;
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
    public class SalesController : Controller
    {
        private readonly AtomicContext _context;

        public SalesController(AtomicContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            var atomicContext = _context.Sales.Include(s => s.Customer).Include(s => s.Salesperson);
            return View(await atomicContext.ToListAsync());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Salesperson)
                .FirstOrDefaultAsync(m => m.SaleId == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName");
            ViewData["SalespersonId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaleId,SaleDate,SalespersonId,CustomerId,SaleTotal")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", sale.CustomerId);
            ViewData["SalespersonId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", sale.SalespersonId);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "BillingAddress", sale.CustomerId);
            ViewData["SalespersonId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", sale.SalespersonId);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaleId,SaleDate,SalespersonId,CustomerId,SaleTotal")] Sale sale)
        {
            if (id != sale.SaleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.SaleId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "BillingAddress", sale.CustomerId);
            ViewData["SalespersonId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", sale.SalespersonId);
            return View(sale);
        }

        #region Original Delete
        //// GET: Sales/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Sales == null)
        //    {
        //        return NotFound();
        //    }

        //    var sale = await _context.Sales
        //        .Include(s => s.Customer)
        //        .Include(s => s.Salesperson)
        //        .FirstOrDefaultAsync(m => m.SaleId == id);
        //    if (sale == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(sale);
        //}

        //// POST: Sales/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Sales == null)
        //    {
        //        return Problem("Entity set 'AtomicContext.Sales'  is null.");
        //    }
        //    var sale = await _context.Sales.FindAsync(id);
        //    if (sale != null)
        //    {
        //        _context.Sales.Remove(sale);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        #endregion

        //Write the AjaxDelete Action
        [AcceptVerbs("POST")]
        public JsonResult AjaxDelete(int id)
        {
            Sale sale = _context.Sales.Find(id);
            _context.Sales.Remove(sale);
            _context.SaveChanges();

            string confirmMessage = $"Deleted the sale that happened on {sale.SaleDate} from the database";

            return Json(new { id = id, message = confirmMessage });
        }

        private bool SaleExists(int id)
        {
          return (_context.Sales?.Any(e => e.SaleId == id)).GetValueOrDefault();
        }
    }
}
