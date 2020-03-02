using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoObras;
using GestaoObras.Models;

namespace GestaoObras.Controllers
{
    public class ObraController : Controller
    {
        private readonly GestaoObraDataContexto _context;

        public ObraController(GestaoObraDataContexto context)
        {
            _context = context;
        }

        // GET: Obra
        public async Task<IActionResult> Listar()
        {
            var gestaoObraDataContexto = _context.Obra.Include(o => o.Cliente).Include(o => o.Engenheiro);
            return View(await gestaoObraDataContexto.ToListAsync());
        }

        // GET: Obra/VerDetalhes/5
        public async Task<IActionResult> VerDetalhes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obra = await _context.Obra
                .Include(o => o.Cliente)
                .Include(o => o.Engenheiro)
                .FirstOrDefaultAsync(m => m.ObraId == id);
            if (obra == null)
            {
                return NotFound();
            }

            return View(obra);
        }

        // GET: Obra/Criar
        public IActionResult Criar()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId");
            ViewData["EngenheiroId"] = new SelectList(_context.Engenheiro, "EngenheiroId", "EngenheiroId");
            return View();
        }

        // POST: Obra/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar([Bind("ObraId,ClienteId,EngenheiroId,Nome,DataInicio,DataFim,Valor")] Obra obra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(obra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Listar));
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", obra.ClienteId);
            ViewData["EngenheiroId"] = new SelectList(_context.Engenheiro, "EngenheiroId", "EngenheiroId", obra.EngenheiroId);
            return View(obra);
        }

        // GET: Obra/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obra = await _context.Obra.FindAsync(id);
            if (obra == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", obra.ClienteId);
            ViewData["EngenheiroId"] = new SelectList(_context.Engenheiro, "EngenheiroId", "EngenheiroId", obra.EngenheiroId);
            return View(obra);
        }

        // POST: Obra/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("ObraId,ClienteId,EngenheiroId,Nome,DataInicio,DataFim,Valor")] Obra obra)
        {
            if (id != obra.ObraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(obra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObraExists(obra.ObraId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Listar));
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", obra.ClienteId);
            ViewData["EngenheiroId"] = new SelectList(_context.Engenheiro, "EngenheiroId", "EngenheiroId", obra.EngenheiroId);
            return View(obra);
        }

        // GET: Obra/Apagar/5
        public async Task<IActionResult> Apagar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obra = await _context.Obra
                .Include(o => o.Cliente)
                .Include(o => o.Engenheiro)
                .FirstOrDefaultAsync(m => m.ObraId == id);
            if (obra == null)
            {
                return NotFound();
            }

            return View(obra);
        }

        // POST: Obra/Apagar/5
        [HttpPost, ActionName("Apagar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ComfirmarApagar(int id)
        {
            var obra = await _context.Obra.FindAsync(id);
            _context.Obra.Remove(obra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Listar));
        }

        private bool ObraExists(int id)
        {
            return _context.Obra.Any(e => e.ObraId == id);
        }
    }
}
