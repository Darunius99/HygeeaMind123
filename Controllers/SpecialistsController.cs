using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HygeeaMind.Data;
using HygeeaMind.Models;
using Microsoft.AspNetCore.Authorization; // Adaugă acest using pentru atributul [Authorize]

namespace HygeeaMind.Controllers
{
    public class SpecialistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpecialistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Specialists
        // Această acțiune rămâne publică (oricine poate vedea lista de specialiști)
        public async Task<IActionResult> Index()
        {
            return View(await _context.Specialists.ToListAsync());
        }

        // GET: Specialists/Details/5
        // Această acțiune rămâne publică (oricine poate vedea detaliile unui specialist)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialist = await _context.Specialists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialist == null)
            {
                return NotFound();
            }

            return View(specialist);
        }

        // GET: Specialists/Create
        [Authorize(Roles = "Admin")] // Doar utilizatorii cu rolul "Admin" pot accesa această acțiune
        public IActionResult Create()
        {
            return View();
        }

        // POST: Specialists/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Doar utilizatorii cu rolul "Admin" pot trimite acest formular
        public async Task<IActionResult> Create([Bind("Id,Name,Specialty,Description,ProfileImageUrl,Email,PhoneNumber,ExperienceYears")] Specialist specialist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specialist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialist);
        }

        // GET: Specialists/Edit/5
        [Authorize(Roles = "Admin")] // Doar utilizatorii cu rolul "Admin" pot accesa această acțiune
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialist = await _context.Specialists.FindAsync(id);
            if (specialist == null)
            {
                return NotFound();
            }
            return View(specialist);
        }

        // POST: Specialists/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Doar utilizatorii cu rolul "Admin" pot trimite acest formular
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Specialty,Description,ProfileImageUrl,Email,PhoneNumber,ExperienceYears")] Specialist specialist)
        {
            if (id != specialist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specialist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialistExists(specialist.Id))
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
            return View(specialist);
        }

        // GET: Specialists/Delete/5
        [Authorize(Roles = "Admin")] // Doar utilizatorii cu rolul "Admin" pot accesa această acțiune
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialist = await _context.Specialists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialist == null)
            {
                return NotFound();
            }

            return View(specialist);
        }

        // POST: Specialists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Doar utilizatorii cu rolul "Admin" pot trimite acest formular
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialist = await _context.Specialists.FindAsync(id);
            if (specialist != null)
            {
                _context.Specialists.Remove(specialist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialistExists(int id)
        {
            return _context.Specialists.Any(e => e.Id == id);
        }
    }
}