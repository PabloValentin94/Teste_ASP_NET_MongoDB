using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Revisao_ASP_NET_MongoDB.Models;

namespace Revisao_ASP_NET_MongoDB.Controllers
{
    public class AvaliacoesController : Controller
    {
        private readonly ContextMongoDB _context;

        public AvaliacoesController()
        {
            _context = new ContextMongoDB();
        }

        // GET: Avaliacoes
        public async Task<IActionResult> Index()
        {
            List<Avaliacao>? avaliacoes = await _context.Avaliacoes.Find(a => a.Id != null).ToListAsync();

            foreach (Avaliacao avaliacao in avaliacoes)
            {
                avaliacao.Carro = await _context.Carros.Find(c => c.Id == avaliacao.Id_Carro).FirstOrDefaultAsync();
            }

            return View(avaliacoes);
        }

        // GET: Avaliacoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes.Find(a => a.Id == id).FirstOrDefaultAsync();

            avaliacao.Carro = await _context.Carros.Find(c => c.Id == avaliacao.Id_Carro).FirstOrDefaultAsync();

            if (avaliacao == null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        // GET: Avaliacoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Avaliacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data_Avaliacao,Nota,Id_Carro")] Avaliacao avaliacao)
        {
            if (ModelState.IsValid)
            {
                avaliacao.Id = Guid.NewGuid();

                await _context.Avaliacoes.InsertOneAsync(avaliacao);
                
                return RedirectToAction(nameof(Index));
            }

            return View(avaliacao);
        }

        // GET: Avaliacoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes.Find(a => a.Id == id).FirstOrDefaultAsync();

            if (avaliacao == null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        // POST: Avaliacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Data_Avaliacao,Nota,Id_Carro")] Avaliacao avaliacao)
        {
            if (id != avaliacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Avaliacoes.ReplaceOneAsync(a => a.Id == id, avaliacao);
                }

                catch (Exception)
                {
                    if (!AvaliacaoExists(avaliacao.Id))
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

            return View(avaliacao);
        }

        // GET: Avaliacoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes.Find(a => a.Id == id).FirstOrDefaultAsync();

            avaliacao.Carro = await _context.Carros.Find(c => c.Id == avaliacao.Id_Carro).FirstOrDefaultAsync();

            if (avaliacao == null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        // POST: Avaliacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _context.Avaliacoes.DeleteOneAsync(a => a.Id == id);

            return RedirectToAction(nameof(Index));
        }

        private bool AvaliacaoExists(Guid id)
        {
            return _context.Avaliacoes.Find(a => a.Id == id).Any();
        }
    }
}
