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
    public class UsuariosSistemasController : Controller
    {
        private readonly BarterEvolutionDbContext _context;

        public UsuariosSistemasController(BarterEvolutionDbContext context)
        {
            _context = context;
        }

        // GET: UsuariosSistemas
        public async Task<IActionResult> Index()
        {
            var barterEvolutionDbContext = _context.UsuariosSistema.Include(u => u.IdDocumentoNavigation).Include(u => u.IdUsuarioNavigation);
            return View(await barterEvolutionDbContext.ToListAsync());
        }

        // GET: UsuariosSistemas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosSistema = await _context.UsuariosSistema
                .Include(u => u.IdDocumentoNavigation)
                .Include(u => u.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.CedulaUsuario == id);
            if (usuariosSistema == null)
            {
                return NotFound();
            }

            return View(usuariosSistema);
        }

        // GET: UsuariosSistemas/Create
        public IActionResult Create()
        {
            ViewData["IdDocumento"] = new SelectList(_context.TipoDocumento, "IdTipoDocumento", "IdTipoDocumento");
            ViewData["IdUsuario"] = new SelectList(_context.TipoUsuario, "IdTipoUsuario", "IdTipoUsuario");
            return View();
        }

        // POST: UsuariosSistemas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CedulaUsuario,IdDocumento,IdUsuario,Nombreusuario1,Nombreusuario2,Apellidousuario1,Apellidousuario2,Email,Usuario,Clave")] UsuariosSistema usuariosSistema)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuariosSistema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDocumento"] = new SelectList(_context.TipoDocumento, "IdTipoDocumento", "IdTipoDocumento", usuariosSistema.IdDocumento);
            ViewData["IdUsuario"] = new SelectList(_context.TipoUsuario, "IdTipoUsuario", "IdTipoUsuario", usuariosSistema.IdUsuario);
            return View(usuariosSistema);
        }

        // GET: UsuariosSistemas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosSistema = await _context.UsuariosSistema.FindAsync(id);
            if (usuariosSistema == null)
            {
                return NotFound();
            }
            ViewData["IdDocumento"] = new SelectList(_context.TipoDocumento, "IdTipoDocumento", "IdTipoDocumento", usuariosSistema.IdDocumento);
            ViewData["IdUsuario"] = new SelectList(_context.TipoUsuario, "IdTipoUsuario", "IdTipoUsuario", usuariosSistema.IdUsuario);
            return View(usuariosSistema);
        }

        // POST: UsuariosSistemas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CedulaUsuario,IdDocumento,IdUsuario,Nombreusuario1,Nombreusuario2,Apellidousuario1,Apellidousuario2,Email,Usuario,Clave")] UsuariosSistema usuariosSistema)
        {
            if (id != usuariosSistema.CedulaUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuariosSistema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosSistemaExists(usuariosSistema.CedulaUsuario))
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
            ViewData["IdDocumento"] = new SelectList(_context.TipoDocumento, "IdTipoDocumento", "IdTipoDocumento", usuariosSistema.IdDocumento);
            ViewData["IdUsuario"] = new SelectList(_context.TipoUsuario, "IdTipoUsuario", "IdTipoUsuario", usuariosSistema.IdUsuario);
            return View(usuariosSistema);
        }

        // GET: UsuariosSistemas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosSistema = await _context.UsuariosSistema
                .Include(u => u.IdDocumentoNavigation)
                .Include(u => u.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.CedulaUsuario == id);
            if (usuariosSistema == null)
            {
                return NotFound();
            }

            return View(usuariosSistema);
        }

        // POST: UsuariosSistemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuariosSistema = await _context.UsuariosSistema.FindAsync(id);
            _context.UsuariosSistema.Remove(usuariosSistema);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosSistemaExists(int id)
        {
            return _context.UsuariosSistema.Any(e => e.CedulaUsuario == id);
        }
    }
}
