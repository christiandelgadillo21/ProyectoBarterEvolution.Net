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
    public class ArticulosController : Controller
    {
        private readonly BarterEvolutionDbContext _context;

        public ArticulosController(BarterEvolutionDbContext context)
        {
            _context = context;
        }

        // GET: Articulos
        public async Task<IActionResult> Index()
        {
            var barterEvolutionDbContext = _context.Articulos.Include(a => a.GeneroNavigation).Include(a => a.IdCategoriaNavigation).Include(a => a.IdEstadoArticuloNavigation);
            return View(await barterEvolutionDbContext.ToListAsync());
        }

        // GET: Articulos/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulos = await _context.Articulos
                .Include(a => a.GeneroNavigation)
                .Include(a => a.IdCategoriaNavigation)
                .Include(a => a.IdEstadoArticuloNavigation)
                .FirstOrDefaultAsync(m => m.IdArticulo == id);
            if (articulos == null)
            {
                return NotFound();
            }

            return View(articulos);
        }

        // GET: Articulos/Create
        public IActionResult Create()
        {
            ViewData["Genero"] = new SelectList(_context.Genero, "IdGenero", "IdGenero");
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "IdCategoria");
            ViewData["IdEstadoArticulo"] = new SelectList(_context.EstadoArticulos, "IdEstadoArticulo", "IdEstadoArticulo");
            return View();
        }

        // POST: Articulos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdArticulo,NombreArticulo,Serie,Modelo,Marca,PrecioUnitario,Descripcion,Genero,IdCategoria,IdEstadoArticulo")] Articulos articulos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articulos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Genero"] = new SelectList(_context.Genero, "IdGenero", "IdGenero", articulos.Genero);
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "IdCategoria", articulos.IdCategoria);
            ViewData["IdEstadoArticulo"] = new SelectList(_context.EstadoArticulos, "IdEstadoArticulo", "IdEstadoArticulo", articulos.IdEstadoArticulo);
            return View(articulos);
        }

        // GET: Articulos/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulos = await _context.Articulos.FindAsync(id);
            if (articulos == null)
            {
                return NotFound();
            }
            ViewData["Genero"] = new SelectList(_context.Genero, "IdGenero", "IdGenero", articulos.Genero);
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "IdCategoria", articulos.IdCategoria);
            ViewData["IdEstadoArticulo"] = new SelectList(_context.EstadoArticulos, "IdEstadoArticulo", "IdEstadoArticulo", articulos.IdEstadoArticulo);
            return View(articulos);
        }

        // POST: Articulos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdArticulo,NombreArticulo,Serie,Modelo,Marca,PrecioUnitario,Descripcion,Genero,IdCategoria,IdEstadoArticulo")] Articulos articulos)
        {
            if (id != articulos.IdArticulo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articulos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticulosExists(articulos.IdArticulo))
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
            ViewData["Genero"] = new SelectList(_context.Genero, "IdGenero", "IdGenero", articulos.Genero);
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "IdCategoria", articulos.IdCategoria);
            ViewData["IdEstadoArticulo"] = new SelectList(_context.EstadoArticulos, "IdEstadoArticulo", "IdEstadoArticulo", articulos.IdEstadoArticulo);
            return View(articulos);
        }

        // GET: Articulos/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulos = await _context.Articulos
                .Include(a => a.GeneroNavigation)
                .Include(a => a.IdCategoriaNavigation)
                .Include(a => a.IdEstadoArticuloNavigation)
                .FirstOrDefaultAsync(m => m.IdArticulo == id);
            if (articulos == null)
            {
                return NotFound();
            }

            return View(articulos);
        }

        // POST: Articulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var articulos = await _context.Articulos.FindAsync(id);
            _context.Articulos.Remove(articulos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticulosExists(string id)
        {
            return _context.Articulos.Any(e => e.IdArticulo == id);
        }
    }
}
