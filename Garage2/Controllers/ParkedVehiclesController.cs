﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2.Data;
using Garage2.Models;
using System.Text.RegularExpressions;

namespace Garage2.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly Garage2Context _context;
        [TempData]
        public string Message { get; set; }
        [TempData]
        public string Alert { get; set; }

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
            var context = _context.ParkedVehicle;
            var vehicles = context
            .Select(v => new OverviewViewModel
            {
                VehicleType = v.VehicleType,
                RegNr = v.RegNr,
                Arrival = v.Arrival
            });

            int parkingLength = 0;
            foreach (var item in vehicles.ToList())
            {
                parkingLength += (item.ParkLenght.Days*24);
                parkingLength += item.ParkLenght.Hours;
            }

            var garage = new GarageData
            {
                Vehicles = await vehicles.ToListAsync(),
                Garage = new GarageModel
                {
                    SpacesOccupied = context.Count(),
                    TotalNrCars = context.Where(v => v.VehicleType == 0).Count(),
                    TotalNrTrucks = context.Where(v => (int)v.VehicleType == 1).Count(),
                    TotalNrMotorcycles = context.Where(v => (int)v.VehicleType == 2).Count(),
                    TotalNrBoats = context.Where(v => (int)v.VehicleType == 3).Count(),
                    TotalNrAirplanes = context.Where(v => (int)v.VehicleType == 4).Count(),
                    TotalNrOfWheels = context.Select(v => v.Wheels).Sum(),
                    TotalProfit = parkingLength * 10
                }
            };

            return View(garage);
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

            int parkingLength = 0;
            foreach (var item in model.ToList())
            {
                parkingLength += (item.ParkLenght.Days * 24);
                parkingLength += item.ParkLenght.Hours;
            }

            var garage = new GarageData
            {
                Vehicles = await model.ToListAsync(),
                Garage = new GarageModel
                {
                    SpacesOccupied = context.Count(),
                    TotalNrCars = context.Where(v => v.VehicleType == 0).Count(),
                    TotalNrTrucks = context.Where(v => (int)v.VehicleType == 1).Count(),
                    TotalNrMotorcycles = context.Where(v => (int)v.VehicleType == 2).Count(),
                    TotalNrBoats = context.Where(v => (int)v.VehicleType == 3).Count(),
                    TotalNrAirplanes = context.Where(v => (int)v.VehicleType == 4).Count(),
                    TotalNrOfWheels = _context.ParkedVehicle.Select(v => v.Wheels).Sum(),
                    TotalProfit = parkingLength * 10
                }
            };

            return View(nameof(Overview), garage);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleType,RegNr,Color,Brand,Model,Wheels,Arrival")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkedVehicle);
                await _context.SaveChangesAsync();
                Alert = "success";
                Message = $"Vehicle with license plate {parkedVehicle.RegNr} successfully parked";
                return RedirectToAction(nameof(Overview));
            }
            return View(parkedVehicle);
        }

        // Remote: Check if RegNr exists in DB
        public ActionResult CheckRegNr(string regNr)
        {
            var parkedVehicle = _context.ParkedVehicle
                .FirstOrDefault(m => m.RegNr == regNr);
            if (parkedVehicle != null)
            {
                return Json($"{regNr} is already parked in the garage.");
            }
            return Json(true);
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

            var viewModel = new EditVehicleViewModel
            {
                VehicleType = parkedVehicle.VehicleType,
                RegNr = parkedVehicle.RegNr,
                Color = parkedVehicle.Color,
                Brand = parkedVehicle.Brand,
                Model = parkedVehicle.Model,
                Wheels = parkedVehicle.Wheels
            };

            return View(viewModel);
        }

        // POST: ParkedVehicles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VehicleType,RegNr,Color,Brand,Model,Wheels")] EditVehicleViewModel viewModel)
        {
            if (id != viewModel.RegNr)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Get the existing entity from the database
                var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);

                if (parkedVehicle == null)
                {
                    return NotFound();
                }

                parkedVehicle.VehicleType = viewModel.VehicleType;
                parkedVehicle.Color = viewModel.Color;
                parkedVehicle.Brand = viewModel.Brand;
                parkedVehicle.Model = viewModel.Model;
                parkedVehicle.Wheels = viewModel.Wheels;

                try
                    {
                    _context.Update(parkedVehicle);
                    _context.Entry(parkedVehicle).Property(v => v.Arrival).IsModified = false;
                    await _context.SaveChangesAsync();
                    Alert = "success";
                    Message = $"Vehicle with license plate {parkedVehicle.RegNr} successfully edited";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkedVehicleExists(viewModel.RegNr))
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
            return View(viewModel);
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
                // Prepare receipt data before deleting
                var receiptViewModel = new ReceiptViewModel
                {
                    RegNr = parkedVehicle.RegNr,
                    Arrival = parkedVehicle.Arrival,
                    Departure = DateTime.Now // Set departure to current time
                };

                // Save receipt data in TempData to pass it to the Receipt action
                TempData["ReceiptData"] = Newtonsoft.Json.JsonConvert.SerializeObject(receiptViewModel);
                // Ta bort fordonet
                _context.ParkedVehicle.Remove(parkedVehicle);
                await _context.SaveChangesAsync();

                Alert = "success";
                Message = $"Vehicle with license plate {parkedVehicle.RegNr} has successfully left the garage";
                return RedirectToAction("Receipt", new { regNr = id });
            }
            return RedirectToAction(nameof(Overview));
        }

        // Ny metod för att generera kvitto
        public async Task<IActionResult> Receipt(string regNr)
        {
            // Retrieve receipt data from TempData
            if (TempData["ReceiptData"] is string receiptDataJson)
            {
                var receiptViewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ReceiptViewModel>(receiptDataJson);
                return View(receiptViewModel);
            }

            return RedirectToAction(nameof(Overview));

            //if (string.IsNullOrWhiteSpace(regNr))
            //{
            //    return BadRequest("Registration number is required.");
            //}

            //var parkedVehicle = await _context.ParkedVehicle.FirstOrDefaultAsync(v => v.RegNr == regNr);
            //if (parkedVehicle == null)
            //{
            //    return NotFound(); // Returnerar 404 om fordonet inte hittas
            //}

            //var receiptViewModel = new ReceiptViewModel
            //{
            //    RegNr = parkedVehicle.RegNr,
            //    Arrival = parkedVehicle.Arrival,
            //    Departure = DateTime.Now // Avresetid, sätts till nu
            //};

            //return View(receiptViewModel); // Returnera kvittovyn med modellen
        }

    private bool ParkedVehicleExists(string id)
        {
            return _context.ParkedVehicle.Any(e => e.RegNr == id);
        }
    }
}