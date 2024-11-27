using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;
using KooliProjekt.Services;
using KooliProjekt.Models;

namespace KooliProjekt.Controllers
{
    public class RentingsController : Controller
    {
        private readonly IRentingService _rentingService;
        private readonly ICustomerService _customerService;

        public RentingsController(IRentingService rentingService, ICustomerService customerService)
        {
            _rentingService = rentingService;
            _customerService = customerService;
        }

        // GET: Rentings
        public async Task<IActionResult> Index(int page = 1, RentingsIndexModel model = null)
        {
            model = model ?? new RentingsIndexModel();
            model.Data = await _rentingService.List(page, 5, model.Search);

            return View(model);
        }

        // GET: Rentings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renting = await _rentingService.Get(id.Value);
            if (renting == null)
            {
                return NotFound();
            }

            return View(renting);
        }

        // GET: Rentings/Create
        public async Task<IActionResult> Create()
        {
            var customers = await _customerService.Lookup();
            ViewData["CustomerId"] = new SelectList(customers, "Id", "FullName");
            return View();
        }





        // POST: Rentings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RentalNo,RentalDate,RentalDueTime,DriveDistance,CustomerId")] Renting renting)
        {
            if (ModelState.IsValid)
            {
                await _rentingService.Save(renting);
                return RedirectToAction(nameof(Index));
            }
            var customers = await _customerService.Lookup();
            ViewData["CustomerId"] = new SelectList(customers, "Id", "FullName", renting.CustomerId);
            return View(renting);
        }

        // GET: Rentings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renting = await _rentingService.Get(id.Value);
            if (renting == null)
            {
                return NotFound();
            }
            var customers = await _customerService.Lookup();
            ViewData["CustomerId"] = new SelectList(customers, "Id", "FullName", renting.CustomerId);
            return View(renting);
        }

        // POST: Rentings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RentalNo,RentalDate,RentalDueTime,DriveDistance,CustomerId")] Renting renting)
        {
            if (id != renting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _rentingService.Save(renting);               
                return RedirectToAction(nameof(Index));
            }
            var customers = await _customerService.Lookup();
            ViewData["CustomerId"] = new SelectList(customers, "Id", "FullName", renting.CustomerId);
            return View(renting);
        }

        // GET: Rentings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renting = await _rentingService.Get(id.Value);
            if (renting == null)
            {
                return NotFound();
            }

            return View(renting);
        }

        // POST: Rentings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _rentingService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
