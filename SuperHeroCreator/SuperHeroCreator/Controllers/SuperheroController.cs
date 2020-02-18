using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroCreator.Data;
using SuperHeroCreator.Models;

namespace SuperHeroCreator.Controllers
{
    public class SuperheroController : Controller
    {
        readonly ApplicationDbContext _context;

        public SuperheroController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Superhero
        public ActionResult Index()
        {
            return View(_context.Superheroes.ToList());
        }

        // GET: Superhero/Details/5
        public ActionResult Details(int id)
        {
            var heroDetail = _context.Superheroes.Where(s => s.Id == id).FirstOrDefault();
            return View();
        }

        // GET: Superhero/Create
        public ActionResult Create()
        {
            Superhero superhero = new Superhero();
            return View(superhero);
        }

        // POST: Superhero/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Name,AlterEgo,PrimaryAbility,SecondaryAbility,Catchphrase")] Superhero superhero)
        {
            try
            {
                _context.Superheroes.Add(superhero);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Superhero/Edit/5
        public ActionResult Edit(int id)
        {
            var superheroToEdit = _context.Superheroes.Find(id);
            return View();
        }

        // POST: Superhero/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Superhero superhero)
        {
            try
            {
                var superheroToEdit = _context.Superheroes.Where(s => s.Id == superhero.Id).FirstOrDefault();
                superheroToEdit.Name = superhero.Name;
                superheroToEdit.AlterEgo = superhero.AlterEgo;
                superheroToEdit.PrimaryAbility = superhero.PrimaryAbility;
                superheroToEdit.PrimaryAbility = superhero.SecondaryAbility;
                superheroToEdit.Catchphrase = superhero.Catchphrase;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Superhero/Delete/5
        public ActionResult Delete(int id, Superhero superhero)
        {
            return View();
        }

        // POST: Superhero/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var deletedHero = _context.Superheroes.Where(s => s.Id == id).FirstOrDefault();
                _context.Superheroes.Remove(deletedHero);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}