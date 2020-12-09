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
    public class InventariosController : Controller
    {
        private readonly BarterEvolutionDbContext _context;

        public InventariosController(BarterEvolutionDbContext context)
        {
            _context = context;
        }

        // GET: Inventarios
        public async Task<IActionResult> Index()
        {
            var barterEvolutionDbContext = _context.Inventario.Include(i => i.IdArticuloNavigation).Include(i => i.IdCategoriaNavigation).Include(i => i.IdCondicionArticuloNavigation).Include(i => i.IdEstadoArticuloNavigation);
            return View(await barterEvolutionDbContext.ToListAsync());
        }

        // GET: Inventarios/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventario
                .Include(i => i.IdArticuloNavigation)
                .Include(i => i.IdCategoriaNavigation)
                .Include(i => i.IdCondicionArticuloNavigation)
                .Include(i => i.IdEstadoArticuloNavigation)
                .FirstOrDefaultAsync(m => m.IdInventario == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // GET: Inventarios/Create
        public IActionResult Create()
        {
            ViewData["IdArticulo"] = new SelectList(_context.Articulos, "IdArticulo", "IdArticulo");
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "IdCategoria");
            ViewData["IdCondicionArticulo"] = new SelectList(_context.CondicionArticulos, "IdCondicionArticulo", "IdCondicionArticulo");
            ViewData["IdEstadoArticulo"] = new SelectList(_context.EstadoArticulos, "IdEstadoArticulo", "IdEstadoArticulo");
            return View();
        }

        // POST: Inventarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInventario,Cantidad,IdArticulo,IdCondicionArticulo,IdCategoria,IdEstadoArticulo")] Inventario inventario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdArticulo"] = new SelectList(_context.Articulos, "IdArticulo", "IdArticulo", inventario.IdArticulo);
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "IdCategoria", inventario.IdCategoria);
            ViewData["IdCondicionArticulo"] = new SelectList(_context.CondicionArticulos, "IdCondicionArticulo", "IdCondicionArticulo", inventario.IdCondicionArticulo);
            ViewData["IdEstadoArticulo"] = new SelectList(_context.EstadoArticulos, "IdEstadoArticulo", "IdEstadoArticulo", inventario.IdEstadoArticulo);
            return View(inventario);
        }

        // GET: Inventarios/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventario.FindAsync(id);
            if (inventario == null)
            {
                return NotFound();
            }
            ViewData["IdArticulo"] = new SelectList(_context.Articulos, "IdArticulo", "IdArticulo", inventario.IdArticulo);
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "IdCategoria", inventario.IdCategoria);
            ViewData["IdCondicionArticulo"] = new SelectList(_context.CondicionArticulos, "IdCondicionArticulo", "IdCondicionArticulo", inventario.IdCondicionArticulo);
            ViewData["IdEstadoArticulo"] = new SelectList(_context.EstadoArticulos, "IdEstadoArticulo", "IdEstadoArticulo", inventario.IdEstadoArticulo);
            return View(inventario);
        }

        // POST: Inventarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdInventario,Cantidad,IdArticulo,IdCondicionArticulo,IdCategoria,IdEstadoArticulo")] Inventario inventario)
        {
            if (id != inventario.IdInventario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventarioExists(inventario.IdInventario))
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
            ViewData["IdArticulo"] = new SelectList(_context.Articulos, "IdArticulo", "IdArticulo", inventario.IdArticulo);
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "IdCategoria", inventario.IdCategoria);
            ViewData["IdCondicionArticulo"] = new SelectList(_context.CondicionArticulos, "IdCondicionArticulo", "IdCondicionArticulo", inventario.IdCondicionArticulo);
            ViewData["IdEstadoArticulo"] = new SelectList(_context.EstadoArticulos, "IdEstadoArticulo", "IdEstadoArticulo", inventario.IdEstadoArticulo);
            return View(inventario);
        }

        // GET: Inventarios/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventario
                .Include(i => i.IdArticuloNavigation)
                .Include(i => i.IdCategoriaNavigation)
                .Include(i => i.IdCondicionArticuloNavigation)
                .Include(i => i.IdEstadoArticuloNavigation)
                .FirstOrDefaultAsync(m => m.IdInventario == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // POST: Inventarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var inventario = await _context.Inventario.FindAsync(id);
            _context.Inventario.Remove(inventario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventarioExists(string id)
        {
            return _context.Inventario.Any(e => e.IdInventario == id);
        }
    }
}
