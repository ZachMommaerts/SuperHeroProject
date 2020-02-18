using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperHeroCreator.Data;
using SuperHeroCreator.Models;

namespace SuperHeroCreator.Controllers
{
    public class SuperheroesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuperheroesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Superheroes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Superheroes.ToListAsync());
        }

        // GET: Superheroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superhero = await _context.Superheroes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (superhero == null)
            {
                return NotFound();
            }

            return View(superhero);
        }

        // GET: Superheroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Superheroes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AlterEgo,PrimaryAbility,SecondaryAbility,Catchphrase")] Superhero superhero)
        {
            if (ModelState.IsValid)
            {
                _context.Add(superhero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(superhero);
        }

        // GET: Superheroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superhero = await _context.Superheroes.FindAsync(id);
            if (superhero == null)
            {
                return NotFound();
            }
            return View(superhero);
        }

        // POST: Superheroes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AlterEgo,PrimaryAbility,SecondaryAbility,Catchphrase")] Superhero superhero)
        {
            if (id != superhero.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(superhero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuperheroExists(superhero.Id))
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
            return View(superhero);
        }

        // GET: Superheroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superhero = await _context.Superheroes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (superhero == null)
            {
                return NotFound();
            }

            return View(superhero);
        }

        // POST: Superheroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var superhero = await _context.Superheroes.FindAsync(id);
            _context.Superheroes.Remove(superhero);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuperheroExists(int id)
        {
            return _context.Superheroes.Any(e => e.Id == id);
        }
    }
}
