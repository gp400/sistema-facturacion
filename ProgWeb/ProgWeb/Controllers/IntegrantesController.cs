using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgWeb.Models;

namespace ProgWeb.Controllers
{
    public class IntegrantesController : Controller
    {
        private readonly ProgWebContext _context;

        public IntegrantesController(ProgWebContext context)
        {
            _context = context;
        }

        // GET: Integrantes
        public async Task<IActionResult> Index()
        {
              return _context.Integrantes != null ? 
                          View(await _context.Integrantes.ToListAsync()) :
                          Problem("Entity set 'ProgWebContext.Integrantes'  is null.");
        }

        // GET: Integrantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Integrantes == null)
            {
                return NotFound();
            }

            var integrante = await _context.Integrantes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (integrante == null)
            {
                return NotFound();
            }

            return View(integrante);
        }

        // GET: Integrantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Integrantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Documento,Foto,Nombre,Apellido,Sexo,FechaNacimiento,Direccion,Telefono,Email,EstadoCivil,TipoSangre,Alergias,Comentario,Estado")] Integrante integrante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(integrante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(integrante);
        }

        // GET: Integrantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Integrantes == null)
            {
                return NotFound();
            }

            var integrante = await _context.Integrantes.FindAsync(id);
            if (integrante == null)
            {
                return NotFound();
            }
            return View(integrante);
        }

        // POST: Integrantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Documento,Foto,Nombre,Apellido,Sexo,FechaNacimiento,Direccion,Telefono,Email,EstadoCivil,TipoSangre,Alergias,Comentario,Estado")] Integrante integrante)
        {
            if (id != integrante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(integrante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntegranteExists(integrante.Id))
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
            return View(integrante);
        }

        // GET: Integrantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Integrantes == null)
            {
                return NotFound();
            }

            var integrante = await _context.Integrantes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (integrante == null)
            {
                return NotFound();
            }

            return View(integrante);
        }

        // POST: Integrantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Integrantes == null)
            {
                return Problem("Entity set 'ProgWebContext.Integrantes'  is null.");
            }
            var integrante = await _context.Integrantes.FindAsync(id);
            if (integrante != null)
            {
                _context.Integrantes.Remove(integrante);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntegranteExists(int id)
        {
          return (_context.Integrantes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
