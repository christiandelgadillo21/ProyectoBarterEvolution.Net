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
    public class ClientesController : Controller
    {
        private readonly BarterEvolutionDbContext _context;

        public ClientesController(BarterEvolutionDbContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var barterEvolutionDbContext = _context.Clientes.Include(c => c.GeneroNavigation).Include(c => c.IdDocumentoNavigation).Include(c => c.IdLocalidadNavigation).Include(c => c.IdTipoclienteNavigation);
            return View(await barterEvolutionDbContext.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes
                .Include(c => c.GeneroNavigation)
                .Include(c => c.IdDocumentoNavigation)
                .Include(c => c.IdLocalidadNavigation)
                .Include(c => c.IdTipoclienteNavigation)
                .FirstOrDefaultAsync(m => m.CedulaCliente == id);
            if (clientes == null)
            {
                return NotFound();
            }

            return View(clientes);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["Genero"] = new SelectList(_context.Genero, "IdGenero", "IdGenero");
            ViewData["IdDocumento"] = new SelectList(_context.TipoDocumento, "IdTipoDocumento", "IdTipoDocumento");
            ViewData["IdLocalidad"] = new SelectList(_context.Localidad, "IdLocalidad", "NombreLocalidad");
            ViewData["IdTipocliente"] = new SelectList(_context.TipoCliente, "IdTipocliente", "IdTipocliente");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CedulaCliente,IdDocumento,IdTipocliente,Nombrecliente1,Nombrecliente2,Apellidocliente1,Apellidocliente2,Genero,TelefonoMovil,Email,DireccionResidencia,Ciudad,IdLocalidad")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Genero"] = new SelectList(_context.Genero, "IdGenero", "IdGenero", clientes.Genero);
            ViewData["IdDocumento"] = new SelectList(_context.TipoDocumento, "IdTipoDocumento", "IdTipoDocumento", clientes.IdDocumento);
            ViewData["IdLocalidad"] = new SelectList(_context.Localidad, "IdLocalidad", "NombreLocalidad", clientes.IdLocalidad);
            ViewData["IdTipocliente"] = new SelectList(_context.TipoCliente, "IdTipocliente", "IdTipocliente", clientes.IdTipocliente);
            return View(clientes);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes == null)
            {
                return NotFound();
            }
            ViewData["Genero"] = new SelectList(_context.Genero, "IdGenero", "IdGenero", clientes.Genero);
            ViewData["IdDocumento"] = new SelectList(_context.TipoDocumento, "IdTipoDocumento", "IdTipoDocumento", clientes.IdDocumento);
            ViewData["IdLocalidad"] = new SelectList(_context.Localidad, "IdLocalidad", "NombreLocalidad", clientes.IdLocalidad);
            ViewData["IdTipocliente"] = new SelectList(_context.TipoCliente, "IdTipocliente", "IdTipocliente", clientes.IdTipocliente);
            return View(clientes);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CedulaCliente,IdDocumento,IdTipocliente,Nombrecliente1,Nombrecliente2,Apellidocliente1,Apellidocliente2,Genero,TelefonoMovil,Email,DireccionResidencia,Ciudad,IdLocalidad")] Clientes clientes)
        {
            if (id != clientes.CedulaCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientesExists(clientes.CedulaCliente))
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
            ViewData["Genero"] = new SelectList(_context.Genero, "IdGenero", "IdGenero", clientes.Genero);
            ViewData["IdDocumento"] = new SelectList(_context.TipoDocumento, "IdTipoDocumento", "IdTipoDocumento", clientes.IdDocumento);
            ViewData["IdLocalidad"] = new SelectList(_context.Localidad, "IdLocalidad", "NombreLocalidad", clientes.IdLocalidad);
            ViewData["IdTipocliente"] = new SelectList(_context.TipoCliente, "IdTipocliente", "IdTipocliente", clientes.IdTipocliente);
            return View(clientes);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes
                .Include(c => c.GeneroNavigation)
                .Include(c => c.IdDocumentoNavigation)
                .Include(c => c.IdLocalidadNavigation)
                .Include(c => c.IdTipoclienteNavigation)
                .FirstOrDefaultAsync(m => m.CedulaCliente == id);
            if (clientes == null)
            {
                return NotFound();
            }

            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientes = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(clientes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientesExists(int id)
        {
            return _context.Clientes.Any(e => e.CedulaCliente == id);
        }
    }
}
