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
    public class LegalidadArticulosController : Controller
    {
        private readonly BarterEvolutionDbContext _context;

        public LegalidadArticulosController(BarterEvolutionDbContext context)
        {
            _context = context;
        }

        // GET: LegalidadArticulos
        public async Task<IActionResult> Index()
        {
            var barterEvolutionDbContext = _context.LegalidadArticulos.Include(l => l.CedulaClienteNavigation).Include(l => l.CondLegalidadNavigation).Include(l => l.IdArticuloNavigation);
            return View(await barterEvolutionDbContext.ToListAsync());
        }

        // GET: LegalidadArticulos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var legalidadArticulos = await _context.LegalidadArticulos
                .Include(l => l.CedulaClienteNavigation)
                .Include(l => l.CondLegalidadNavigation)
                .Include(l => l.IdArticuloNavigation)
                .FirstOrDefaultAsync(m => m.IdLegalidad == id);
            if (legalidadArticulos == null)
            {
                return NotFound();
            }

            return View(legalidadArticulos);
        }

        // GET: LegalidadArticulos/Create
        public IActionResult Create()
        {
            ViewData["CedulaCliente"] = new SelectList(_context.Clientes, "CedulaCliente", "Apellidocliente1");
            ViewData["CondLegalidad"] = new SelectList(_context.CondicionLegalidad, "CondicionLegalidad1", "CondicionLegalidad1");
            ViewData["IdArticulo"] = new SelectList(_context.Articulos, "IdArticulo", "IdArticulo");
            return View();
        }

        // POST: LegalidadArticulos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLegalidad,CondLegalidad,FechaLegalidad,CedulaCliente,DescripcionLegalidad,IdArticulo")] LegalidadArticulos legalidadArticulos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(legalidadArticulos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaCliente"] = new SelectList(_context.Clientes, "CedulaCliente", "Apellidocliente1", legalidadArticulos.CedulaCliente);
            ViewData["CondLegalidad"] = new SelectList(_context.CondicionLegalidad, "CondicionLegalidad1", "CondicionLegalidad1", legalidadArticulos.CondLegalidad);
            ViewData["IdArticulo"] = new SelectList(_context.Articulos, "IdArticulo", "IdArticulo", legalidadArticulos.IdArticulo);
            return View(legalidadArticulos);
        }

        // GET: LegalidadArticulos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var legalidadArticulos = await _context.LegalidadArticulos.FindAsync(id);
            if (legalidadArticulos == null)
            {
                return NotFound();
            }
            ViewData["CedulaCliente"] = new SelectList(_context.Clientes, "CedulaCliente", "Apellidocliente1", legalidadArticulos.CedulaCliente);
            ViewData["CondLegalidad"] = new SelectList(_context.CondicionLegalidad, "CondicionLegalidad1", "CondicionLegalidad1", legalidadArticulos.CondLegalidad);
            ViewData["IdArticulo"] = new SelectList(_context.Articulos, "IdArticulo", "IdArticulo", legalidadArticulos.IdArticulo);
            return View(legalidadArticulos);
        }

        // POST: LegalidadArticulos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLegalidad,CondLegalidad,FechaLegalidad,CedulaCliente,DescripcionLegalidad,IdArticulo")] LegalidadArticulos legalidadArticulos)
        {
            if (id != legalidadArticulos.IdLegalidad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(legalidadArticulos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LegalidadArticulosExists(legalidadArticulos.IdLegalidad))
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
            ViewData["CedulaCliente"] = new SelectList(_context.Clientes, "CedulaCliente", "Apellidocliente1", legalidadArticulos.CedulaCliente);
            ViewData["CondLegalidad"] = new SelectList(_context.CondicionLegalidad, "CondicionLegalidad1", "CondicionLegalidad1", legalidadArticulos.CondLegalidad);
            ViewData["IdArticulo"] = new SelectList(_context.Articulos, "IdArticulo", "IdArticulo", legalidadArticulos.IdArticulo);
            return View(legalidadArticulos);
        }

        // GET: LegalidadArticulos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var legalidadArticulos = await _context.LegalidadArticulos
                .Include(l => l.CedulaClienteNavigation)
                .Include(l => l.CondLegalidadNavigation)
                .Include(l => l.IdArticuloNavigation)
                .FirstOrDefaultAsync(m => m.IdLegalidad == id);
            if (legalidadArticulos == null)
            {
                return NotFound();
            }

            return View(legalidadArticulos);
        }

        // POST: LegalidadArticulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var legalidadArticulos = await _context.LegalidadArticulos.FindAsync(id);
            _context.LegalidadArticulos.Remove(legalidadArticulos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LegalidadArticulosExists(int id)
        {
            return _context.LegalidadArticulos.Any(e => e.IdLegalidad == id);
        }
    }
}
