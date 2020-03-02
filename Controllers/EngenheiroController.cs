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
    public class EngenheiroController : Controller
    {
        private readonly GestaoObraDataContexto _context;

        public EngenheiroController(GestaoObraDataContexto context)
        {
            _context = context;
        }

        // GET: Engenheiro
        public async Task<IActionResult> Listar()
        {
            return View(await _context.Engenheiro.ToListAsync());
        }

        // GET: Engenheiro/VerDetalhes/5
        public async Task<IActionResult> VerDetalhes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var engenheiro = await _context.Engenheiro
                .FirstOrDefaultAsync(m => m.EngenheiroId == id);
            if (engenheiro == null)
            {
                return NotFound();
            }

            return View(engenheiro);
        }

        // GET: Engenheiro/Criar
        public IActionResult Criar()
        {
            return View();
        }

        // POST: Engenheiro/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar([Bind("EngenheiroId,Nome,Sexo,Telefone,Endereco")] Engenheiro engenheiro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(engenheiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Listar));
            }
            return View(engenheiro);
        }

        // GET: Engenheiro/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var engenheiro = await _context.Engenheiro.FindAsync(id);
            if (engenheiro == null)
            {
                return NotFound();
            }
            return View(engenheiro);
        }

        // POST: Engenheiro/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("EngenheiroId,Nome,Sexo,Telefone,Endereco")] Engenheiro engenheiro)
        {
            if (id != engenheiro.EngenheiroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(engenheiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EngenheiroExists(engenheiro.EngenheiroId))
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
            return View(engenheiro);
        }

        // GET: Engenheiro/Apagar/5
        public async Task<IActionResult> Apagar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var engenheiro = await _context.Engenheiro
                .FirstOrDefaultAsync(m => m.EngenheiroId == id);
            if (engenheiro == null)
            {
                return NotFound();
            }

            return View(engenheiro);
        }

        // POST: Engenheiro/rApagar/5
        [HttpPost, ActionName("Apagar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ComfirmarApagar(int id)
        {
            var engenheiro = await _context.Engenheiro.FindAsync(id);
            _context.Engenheiro.Remove(engenheiro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Listar));
        }

        private bool EngenheiroExists(int id)
        {
            return _context.Engenheiro.Any(e => e.EngenheiroId == id);
        }
    }
}
