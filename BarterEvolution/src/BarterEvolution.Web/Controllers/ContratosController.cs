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
    public class ContratosController : Controller
    {
        private readonly BarterEvolutionDbContext _context;

        public ContratosController(BarterEvolutionDbContext context)
        {
            _context = context;
        }

        // GET: Contratos
        public async Task<IActionResult> Index()
        {
            var barterEvolutionDbContext = _context.Contratos.Include(c => c.CedulaClienteNavigation).Include(c => c.CedulaUsuarioNavigation).Include(c => c.IdArticuloNavigation).Include(c => c.IdCondicionContratoNavigation).Include(c => c.NoProrrogaNavigation);
            return View(await barterEvolutionDbContext.ToListAsync());
        }

        // GET: Contratos/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contratos = await _context.Contratos
                .Include(c => c.CedulaClienteNavigation)
                .Include(c => c.CedulaUsuarioNavigation)
                .Include(c => c.IdArticuloNavigation)
                .Include(c => c.IdCondicionContratoNavigation)
                .Include(c => c.NoProrrogaNavigation)
                .FirstOrDefaultAsync(m => m.NoContrato == id);
            if (contratos == null)
            {
                return NotFound();
            }

            return View(contratos);
        }

        // GET: Contratos/Create
        public IActionResult Create()
        {
            ViewData["CedulaCliente"] = new SelectList(_context.Clientes, "CedulaCliente", "Apellidocliente1");
            ViewData["CedulaUsuario"] = new SelectList(_context.UsuariosSistema, "CedulaUsuario", "Apellidousuario1");
            ViewData["IdArticulo"] = new SelectList(_context.Articulos, "IdArticulo", "IdArticulo");
            ViewData["IdCondicionContrato"] = new SelectList(_context.CondicionContratos, "IdCondicionContrato", "IdCondicionContrato");
            ViewData["NoProrroga"] = new SelectList(_context.Prorrogas, "NoProrroga", "NoProrroga");
            return View();
        }

        // POST: Contratos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoContrato,CedulaCliente,CedulaUsuario,FechaInicio,FechaVencimiento,FechaPago,PlazoEstipulado,IdArticulo,IdCondicionContrato,NoProrroga,ValorContrato")] Contratos contratos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contratos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaCliente"] = new SelectList(_context.Clientes, "CedulaCliente", "Apellidocliente1", contratos.CedulaCliente);
            ViewData["CedulaUsuario"] = new SelectList(_context.UsuariosSistema, "CedulaUsuario", "Apellidousuario1", contratos.CedulaUsuario);
            ViewData["IdArticulo"] = new SelectList(_context.Articulos, "IdArticulo", "IdArticulo", contratos.IdArticulo);
            ViewData["IdCondicionContrato"] = new SelectList(_context.CondicionContratos, "IdCondicionContrato", "IdCondicionContrato", contratos.IdCondicionContrato);
            ViewData["NoProrroga"] = new SelectList(_context.Prorrogas, "NoProrroga", "NoProrroga", contratos.NoProrroga);
            return View(contratos);
        }

        // GET: Contratos/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contratos = await _context.Contratos.FindAsync(id);
            if (contratos == null)
            {
                return NotFound();
            }
            ViewData["CedulaCliente"] = new SelectList(_context.Clientes, "CedulaCliente", "Apellidocliente1", contratos.CedulaCliente);
            ViewData["CedulaUsuario"] = new SelectList(_context.UsuariosSistema, "CedulaUsuario", "Apellidousuario1", contratos.CedulaUsuario);
            ViewData["IdArticulo"] = new SelectList(_context.Articulos, "IdArticulo", "IdArticulo", contratos.IdArticulo);
            ViewData["IdCondicionContrato"] = new SelectList(_context.CondicionContratos, "IdCondicionContrato", "IdCondicionContrato", contratos.IdCondicionContrato);
            ViewData["NoProrroga"] = new SelectList(_context.Prorrogas, "NoProrroga", "NoProrroga", contratos.NoProrroga);
            return View(contratos);
        }

        // POST: Contratos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NoContrato,CedulaCliente,CedulaUsuario,FechaInicio,FechaVencimiento,FechaPago,PlazoEstipulado,IdArticulo,IdCondicionContrato,NoProrroga,ValorContrato")] Contratos contratos)
        {
            if (id != contratos.NoContrato)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contratos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContratosExists(contratos.NoContrato))
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
            ViewData["CedulaCliente"] = new SelectList(_context.Clientes, "CedulaCliente", "Apellidocliente1", contratos.CedulaCliente);
            ViewData["CedulaUsuario"] = new SelectList(_context.UsuariosSistema, "CedulaUsuario", "Apellidousuario1", contratos.CedulaUsuario);
            ViewData["IdArticulo"] = new SelectList(_context.Articulos, "IdArticulo", "IdArticulo", contratos.IdArticulo);
            ViewData["IdCondicionContrato"] = new SelectList(_context.CondicionContratos, "IdCondicionContrato", "IdCondicionContrato", contratos.IdCondicionContrato);
            ViewData["NoProrroga"] = new SelectList(_context.Prorrogas, "NoProrroga", "NoProrroga", contratos.NoProrroga);
            return View(contratos);
        }

        // GET: Contratos/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contratos = await _context.Contratos
                .Include(c => c.CedulaClienteNavigation)
                .Include(c => c.CedulaUsuarioNavigation)
                .Include(c => c.IdArticuloNavigation)
                .Include(c => c.IdCondicionContratoNavigation)
                .Include(c => c.NoProrrogaNavigation)
                .FirstOrDefaultAsync(m => m.NoContrato == id);
            if (contratos == null)
            {
                return NotFound();
            }

            return View(contratos);
        }

        // POST: Contratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var contratos = await _context.Contratos.FindAsync(id);
            _context.Contratos.Remove(contratos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContratosExists(string id)
        {
            return _context.Contratos.Any(e => e.NoContrato == id);
        }
    }
}
