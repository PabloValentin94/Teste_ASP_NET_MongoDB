using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Driver;
using Revisao_ASP_NET_MongoDB.Models;

namespace Revisao_ASP_NET_MongoDB.Controllers
{
    public class CarrosController : Controller
    {
        private readonly ContextMongoDB _context;

        public CarrosController()
        {
            _context = new ContextMongoDB();
        }

        // GET: Carros
        public async Task<IActionResult> Index()
        {
            return View(await _context.Carros.Find(c => c.Id != null).ToListAsync());
        }

        // GET: Carros/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carro = await _context.Carros.Find(c => c.Id == id).FirstOrDefaultAsync();

            if (carro == null)
            {
                return NotFound();
            }

            return View(carro);
        }

        // GET: Carros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Marca")] Carro carro)
        {
            if (ModelState.IsValid)
            {
                carro.Id = Guid.NewGuid();

                await _context.Carros.InsertOneAsync(carro);

                return RedirectToAction(nameof(Index));
            }

            return View(carro);
        }

        // GET: Carros/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carro = await _context.Carros.Find(c => c.Id == id).FirstOrDefaultAsync();

            if (carro == null)
            {
                return NotFound();
            }

            return View(carro);
        }

        // POST: Carros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Marca")] Carro carro)
        {
            if (id != carro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Carros.ReplaceOneAsync(c => c.Id == id, carro);
                }

                catch (Exception)
                {
                    if (!CarroExists(carro.Id))
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

            return View(carro);
        }

        // GET: Carros/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carro = await _context.Carros.Find(c => c.Id == id).FirstOrDefaultAsync();

            if (carro == null)
            {
                return NotFound();
            }

            return View(carro);
        }

        // POST: Carros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _context.Carros.DeleteOneAsync(c => c.Id == id);

            return RedirectToAction(nameof(Index));
        }

        private bool CarroExists(Guid id)
        {
            return _context.Carros.Find(c => c.Id == id).Any();
        }
    }
}
