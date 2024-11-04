using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2.Data;
using Garage2.Models;

namespace Garage2.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly Garage2Context _context;

        public ParkedVehiclesController(Garage2Context context)
        {
            _context = context;
        }

        // GET: ParkedVehicles
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.ParkedVehicle.ToListAsync());
        }*/

        public async Task<IActionResult> Overview()
        {
            var vehicles = await _context.ParkedVehicle
            .Select(v => new OverviewViewModel
            {
                VehicleType = v.VehicleType,
                RegNr = v.RegNr,
                Arrival = v.Arrival
            }).ToListAsync();

            return View(vehicles);
        }

        public async Task<IActionResult> Filter(string regNr, int? type, string? date)
        {
            var context = _context.ParkedVehicle.Select(v => new OverviewViewModel
            {
                VehicleType = v.VehicleType,
                RegNr = v.RegNr,
                Arrival = v.Arrival
            });

            var model = string.IsNullOrWhiteSpace(regNr) ?
                context :
                context.Where(v => v.RegNr.Contains(regNr));

            model = type is null ?
                model :
                model.Where(v => (int)v.VehicleType == type);

            model = date is null ?
                model :
                model.Where(v => v.Arrival > DateTime.Parse(date + " 00:00:00") && v.Arrival < DateTime.Parse(date + " 23:59:59"));

            return View(nameof(Overview), await model.ToListAsync());
        }

        // GET: ParkedVehicles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.RegNr == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleType,RegNr,Color,Brand,Model,Wheels,Arrival")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkedVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Overview));
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VehicleType,RegNr,Color,Brand,Model,Wheels,Arrival")] ParkedVehicle parkedVehicle)
        {
            if (id != parkedVehicle.RegNr)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkedVehicle);
                    _context.Entry(parkedVehicle).Property(v => v.Arrival).IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkedVehicleExists(parkedVehicle.RegNr))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Overview));
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.RegNr == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle != null)
            {
                _context.ParkedVehicle.Remove(parkedVehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Overview));
        }

        private bool ParkedVehicleExists(string id)
        {
            return _context.ParkedVehicle.Any(e => e.RegNr == id);
        }
    }
}
