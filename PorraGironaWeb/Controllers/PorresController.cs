using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PorraGironaWeb.Models.Entity;

namespace PorraGironaWeb.Controllers
{
    public class PorresController : Controller
    {
        //private readonly PostDbContext _context;

        //public PorresController(PostDbContext context)
        //{
        // _context = context;
        //}

        private readonly PostDbContext _context;

        public PorresController()
        {
            _context = new PostDbContext();
        }

        // GET: Porres
        public async Task<IActionResult> Index()
        {
            var postDbContext = _context.Porres.Include(p => p.IdpartitNavigation).Include(p => p.IdpenyistaNavigation);
            return View(await postDbContext.ToListAsync());
        }

        // GET: Porres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var porre = await _context.Porres
                .Include(p => p.IdpartitNavigation)
                .Include(p => p.IdpenyistaNavigation)
                .FirstOrDefaultAsync(m => m.Idporra == id);
            if (porre == null)
            {
                return NotFound();
            }

            return View(porre);
        }

        // GET: Porres/Create
        public IActionResult Create()
        {
            ViewData["Idpartit"] = new SelectList(_context.Partits, "Idpartit", "Idpartit");
            ViewData["Idpenyista"] = new SelectList(_context.Penyistes, "Idpenyista", "Idpenyista");
            return View();
        }

        // POST: Porres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idporra,Golslocal,Golsvisitant,Data,Idsgolejadorslocal,Idsgolejadorsvisitant,Idpenyista,Idpartit")] Porre porre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(porre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idpartit"] = new SelectList(_context.Partits, "Idpartit", "Idpartit", porre.Idpartit);
            ViewData["Idpenyista"] = new SelectList(_context.Penyistes, "Idpenyista", "Idpenyista", porre.Idpenyista);
            return View(porre);
        }

        // GET: Porres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var porre = await _context.Porres.FindAsync(id);
            if (porre == null)
            {
                return NotFound();
            }
            ViewData["Idpartit"] = new SelectList(_context.Partits, "Idpartit", "Idpartit", porre.Idpartit);
            ViewData["Idpenyista"] = new SelectList(_context.Penyistes, "Idpenyista", "Idpenyista", porre.Idpenyista);
            return View(porre);
        }

        // POST: Porres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idporra,Golslocal,Golsvisitant,Data,Idsgolejadorslocal,Idsgolejadorsvisitant,Idpenyista,Idpartit")] Porre porre)
        {
            if (id != porre.Idporra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(porre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PorreExists(porre.Idporra))
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
            ViewData["Idpartit"] = new SelectList(_context.Partits, "Idpartit", "Idpartit", porre.Idpartit);
            ViewData["Idpenyista"] = new SelectList(_context.Penyistes, "Idpenyista", "Idpenyista", porre.Idpenyista);
            return View(porre);
        }

        // GET: Porres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var porre = await _context.Porres
                .Include(p => p.IdpartitNavigation)
                .Include(p => p.IdpenyistaNavigation)
                .FirstOrDefaultAsync(m => m.Idporra == id);
            if (porre == null)
            {
                return NotFound();
            }

            return View(porre);
        }

        // POST: Porres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var porre = await _context.Porres.FindAsync(id);
            _context.Porres.Remove(porre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PorreExists(int id)
        {
            return _context.Porres.Any(e => e.Idporra == id);
        }
    }
}
