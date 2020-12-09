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
    public class VentasController : Controller
    {
        private readonly BarterEvolutionDbContext _context;

        public VentasController(BarterEvolutionDbContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var barterEvolutionDbContext = _context.Ventas.Include(v => v.CedulaClienteNavigation).Include(v => v.CedulaUsuarioNavigation).Include(v => v.IdArticuloNavigation).Include(v => v.IdEstadoArticuloNavigation);
            return View(await barterEvolutionDbContext.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventas = await _context.Ventas
                .Include(v => v.CedulaClienteNavigation)
                .Include(v => v.CedulaUsuarioNavigation)
                .Include(v => v.IdArticuloNavigation)
                .Include(v => v.IdEstadoArticuloNavigation)
                .FirstOrDefaultAsync(m => m.NoFactura == id);
            if (ventas == null)
            {
                return NotFound();
            }

            return View(ventas);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            ViewData["CedulaCliente"] = new SelectList(_context.Clientes, "CedulaCliente", "Apellidocliente1");
            ViewData["CedulaUsuario"] = new SelectList(_context.UsuariosSistema, "CedulaUsuario", "Apellidousuario1");
            ViewData["IdArticulo"] = new SelectList(_context.Articulos, "IdArticulo", "IdArticulo");
            ViewData["IdEstadoArticulo"] = new SelectList(_context.EstadoArticulos, "IdEstadoArticulo", "IdEstadoArticulo");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoFactura,FechaFactura,PrecioUnitario,Cantidad,SubTotal,Iva,ValorTotal,CedulaCliente,CedulaUsuario,IdArticulo,IdEstadoArticulo")] Ventas ventas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ventas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaCliente"] = new SelectList(_context.Clientes, "CedulaCliente", "Apellidocliente1", ventas.CedulaCliente);
            ViewData["CedulaUsuario"] = new SelectList(_context.UsuariosSistema, "CedulaUsuario", "Apellidousuario1", ventas.CedulaUsuario);
            ViewData["IdArticulo"] = new SelectList(_context.Articulos, "IdArticulo", "IdArticulo", ventas.IdArticulo);
            ViewData["IdEstadoArticulo"] = new SelectList(_context.EstadoArticulos, "IdEstadoArticulo", "IdEstadoArticulo", ventas.IdEstadoArticulo);
            return View(ventas);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventas = await _context.Ventas.FindAsync(id);
            if (ventas == null)
            {
                return NotFound();
            }
            ViewData["CedulaCliente"] = new SelectList(_context.Clientes, "CedulaCliente", "Apellidocliente1", ventas.CedulaCliente);
            ViewData["CedulaUsuario"] = new SelectList(_context.UsuariosSistema, "CedulaUsuario", "Apellidousuario1", ventas.CedulaUsuario);
            ViewData["IdArticulo"] = new SelectList(_context.Articulos, "IdArticulo", "IdArticulo", ventas.IdArticulo);
            ViewData["IdEstadoArticulo"] = new SelectList(_context.EstadoArticulos, "IdEstadoArticulo", "IdEstadoArticulo", ventas.IdEstadoArticulo);
            return View(ventas);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NoFactura,FechaFactura,PrecioUnitario,Cantidad,SubTotal,Iva,ValorTotal,CedulaCliente,CedulaUsuario,IdArticulo,IdEstadoArticulo")] Ventas ventas)
        {
            if (id != ventas.NoFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentasExists(ventas.NoFactura))
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
            ViewData["CedulaCliente"] = new SelectList(_context.Clientes, "CedulaCliente", "Apellidocliente1", ventas.CedulaCliente);
            ViewData["CedulaUsuario"] = new SelectList(_context.UsuariosSistema, "CedulaUsuario", "Apellidousuario1", ventas.CedulaUsuario);
            ViewData["IdArticulo"] = new SelectList(_context.Articulos, "IdArticulo", "IdArticulo", ventas.IdArticulo);
            ViewData["IdEstadoArticulo"] = new SelectList(_context.EstadoArticulos, "IdEstadoArticulo", "IdEstadoArticulo", ventas.IdEstadoArticulo);
            return View(ventas);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventas = await _context.Ventas
                .Include(v => v.CedulaClienteNavigation)
                .Include(v => v.CedulaUsuarioNavigation)
                .Include(v => v.IdArticuloNavigation)
                .Include(v => v.IdEstadoArticuloNavigation)
                .FirstOrDefaultAsync(m => m.NoFactura == id);
            if (ventas == null)
            {
                return NotFound();
            }

            return View(ventas);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var ventas = await _context.Ventas.FindAsync(id);
            _context.Ventas.Remove(ventas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentasExists(string id)
        {
            return _context.Ventas.Any(e => e.NoFactura == id);
        }
    }
}
