using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarterEvolution.Infrastructure;
using BarterEvolution.Infrastructure.Data;

namespace BarterEvolution.Web.Controllers
{
    public class ProrrogasController : Controller
    {
        private readonly BarterEvolutionDbContext _context;

        public ProrrogasController(BarterEvolutionDbContext context)
        {
            _context = context;
        }

        // GET: Prorrogas
        public async Task<IActionResult> Index()
        {
            var barterEvolutionDbContext = _context.Prorrogas.Include(p => p.NoContratoNavigation);
            return View(await barterEvolutionDbContext.ToListAsync());
        }

        // GET: Prorrogas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prorrogas = await _context.Prorrogas
                .Include(p => p.NoContratoNavigation)
                .FirstOrDefaultAsync(m => m.NoProrroga == id);
            if (prorrogas == null)
            {
                return NotFound();
            }

            return View(prorrogas);
        }

        // GET: Prorrogas/Create
        public IActionResult Create()
        {
            ViewData["NoContrato"] = new SelectList(_context.Contratos, "NoContrato", "NoContrato");
            return View();
        }

        // POST: Prorrogas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoProrroga,NoContrato,FechaInicioProrroga,FechaVencimientoProrroga,MesesAPagar,ValorMes,DiasVencidos")] Prorrogas prorrogas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prorrogas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NoContrato"] = new SelectList(_context.Contratos, "NoContrato", "NoContrato", prorrogas.NoContrato);
            return View(prorrogas);
        }

        // GET: Prorrogas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prorrogas = await _context.Prorrogas.FindAsync(id);
            if (prorrogas == null)
            {
                return NotFound();
            }
            ViewData["NoContrato"] = new SelectList(_context.Contratos, "NoContrato", "NoContrato", prorrogas.NoContrato);
            return View(prorrogas);
        }

        // POST: Prorrogas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NoProrroga,NoContrato,FechaInicioProrroga,FechaVencimientoProrroga,MesesAPagar,ValorMes,DiasVencidos")] Prorrogas prorrogas)
        {
            if (id != prorrogas.NoProrroga)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prorrogas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProrrogasExists(prorrogas.NoProrroga))
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
            ViewData["NoContrato"] = new SelectList(_context.Contratos, "NoContrato", "NoContrato", prorrogas.NoContrato);
            return View(prorrogas);
        }

        // GET: Prorrogas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prorrogas = await _context.Prorrogas
                .Include(p => p.NoContratoNavigation)
                .FirstOrDefaultAsync(m => m.NoProrroga == id);
            if (prorrogas == null)
            {
                return NotFound();
            }

            return View(prorrogas);
        }

        // POST: Prorrogas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var prorrogas = await _context.Prorrogas.FindAsync(id);
            _context.Prorrogas.Remove(prorrogas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProrrogasExists(string id)
        {
            return _context.Prorrogas.Any(e => e.NoProrroga == id);
        }
    }
}
