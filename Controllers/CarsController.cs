using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Apetrei_Alexandru_Proiect.Data;
using Apetrei_Alexandru_Proiect.Models;

namespace Apetrei_Alexandru_Proiect.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index(string sortOrder, int? brandId, int? modelId)
        {
            // Parametrii pentru linkurile din view
            ViewData["MileageSortParm"] = String.IsNullOrEmpty(sortOrder) ? "mileage_desc" :
                                           (sortOrder == "Mileage" ? "mileage_desc" : "Mileage");
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            // Selectăm mașinile și includem relațiile
            var cars = _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Condition)
                .Include(c => c.FuelType)
                .Include(c => c.Model)
                .Include(c => c.Transmission)
                .AsQueryable();  // foarte important pentru a aplica OrderBy mai jos

            // Filtrare după Brand și Model
            if (brandId.HasValue && brandId.Value != 0)
            {
                cars = cars.Where(c => c.BrandId == brandId.Value);
            }

            if (modelId.HasValue && modelId.Value != 0)
            {
                cars = cars.Where(c => c.ModelId == modelId.Value);
            }

            // Pregătim dropdown-urile
            ViewData["BrandList"] = new SelectList(_context.Brands.OrderBy(b => b.Name), "BrandId", "Name", brandId);

            // Pentru Model, afișăm doar modelele filtrate după brand dacă s-a selectat unul
            if (brandId.HasValue && brandId.Value != 0)
            {
                ViewData["ModelList"] = new SelectList(
                    _context.Models.Where(m => m.BrandId == brandId.Value).OrderBy(m => m.Name),
                    "ModelId", "Name", modelId);
            }
            else
            {
                ViewData["ModelList"] = new SelectList(_context.Models.OrderBy(m => m.Name), "ModelId", "Name", modelId);
            }


            // Aplicăm sortarea
            switch (sortOrder)
            {
                case "mileage_desc":
                    cars = cars.OrderByDescending(c => c.Mileage);
                    break;
                case "Mileage":
                    cars = cars.OrderBy(c => c.Mileage);
                    break;
                case "Price":
                    cars = cars.OrderBy(c => c.Price);
                    break;
                case "price_desc":
                    cars = cars.OrderByDescending(c => c.Price);
                    break;
                default:
                    cars = cars.OrderBy(c => c.CarId); // ordinea implicită
                    break;
            }

            return View(await cars.AsNoTracking().ToListAsync());
        }


        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Condition)
                .Include(c => c.FuelType)
                .Include(c => c.Model)
                .Include(c => c.Transmission)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "Name");
            ViewData["ConditionId"] = new SelectList(_context.Conditions, "ConditionId", "Name");
            ViewData["FuelTypeId"] = new SelectList(_context.FuelTypes, "FuelTypeId", "Name");
            ViewData["ModelId"] = new SelectList(_context.Models, "ModelId", "Name");
            ViewData["TransmissionId"] = new SelectList(_context.Transmissions, "TransmissionId", "Name");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,Year,EngineSize,Mileage,Price,BrandId,ModelId,FuelTypeId,TransmissionId,ConditionId")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "Name", car.BrandId);
            ViewData["ConditionId"] = new SelectList(_context.Conditions, "ConditionId", "Name", car.ConditionId);
            ViewData["FuelTypeId"] = new SelectList(_context.FuelTypes, "FuelTypeId", "Name", car.FuelTypeId);
            ViewData["ModelId"] = new SelectList(_context.Models, "ModelId", "Name", car.ModelId);
            ViewData["TransmissionId"] = new SelectList(_context.Transmissions, "TransmissionId", "Name", car.TransmissionId);
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "Name", car.BrandId);
            ViewData["ConditionId"] = new SelectList(_context.Conditions, "ConditionId", "Name", car.ConditionId);
            ViewData["FuelTypeId"] = new SelectList(_context.FuelTypes, "FuelTypeId", "Name", car.FuelTypeId);
            ViewData["ModelId"] = new SelectList(_context.Models, "ModelId", "Name", car.ModelId);
            ViewData["TransmissionId"] = new SelectList(_context.Transmissions, "TransmissionId", "Name", car.TransmissionId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,Year,EngineSize,Mileage,Price,BrandId,ModelId,FuelTypeId,TransmissionId,ConditionId")] Car car)
        {
            if (id != car.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarId))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "Name", car.BrandId);
            ViewData["ConditionId"] = new SelectList(_context.Conditions, "ConditionId", "Name", car.ConditionId);
            ViewData["FuelTypeId"] = new SelectList(_context.FuelTypes, "FuelTypeId", "Name", car.FuelTypeId);
            ViewData["ModelId"] = new SelectList(_context.Models, "ModelId", "Name", car.ModelId);
            ViewData["TransmissionId"] = new SelectList(_context.Transmissions, "TransmissionId", "Name", car.TransmissionId);
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Condition)
                .Include(c => c.FuelType)
                .Include(c => c.Model)
                .Include(c => c.Transmission)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.CarId == id);
        }


        [HttpGet]
        public JsonResult GetModelsByBrand(int brandId)
        {
            var models = _context.Models
                .Where(m => m.BrandId == brandId)
                .Select(m => new { m.ModelId, m.Name })
                .ToList();

            return Json(models);
        }

    }
}
