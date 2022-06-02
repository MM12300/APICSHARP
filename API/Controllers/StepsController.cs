using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    public class StepsController : Controller
    {
        private readonly StepContext _context;

        public StepsController(StepContext context)
        {
            _context = context;
        }

        // GET: Steps
        public async Task<IActionResult> Index()
        {
            return View(await _context.Step.ToListAsync());
        }

        // GET: Steps/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var step = await _context.Step
                .FirstOrDefaultAsync(m => m.Id == id);
            if (step == null)
            {
                return NotFound();
            }

            return View(step);
        }

        // GET: Steps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Steps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StepId,Instruction")] Step step)
        {
            if (ModelState.IsValid)
            {
                _context.Add(step);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(step);
        }

        // GET: Steps/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var step = await _context.Step.FindAsync(id);
            if (step == null)
            {
                return NotFound();
            }
            return View(step);
        }

        // POST: Steps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,StepId,Instruction")] Step step)
        {
            if (id != step.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(step);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StepExists(step.Id))
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
            return View(step);
        }

        // GET: Steps/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var step = await _context.Step
                .FirstOrDefaultAsync(m => m.Id == id);
            if (step == null)
            {
                return NotFound();
            }

            return View(step);
        }

        // POST: Steps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var step = await _context.Step.FindAsync(id);
            _context.Step.Remove(step);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StepExists(long id)
        {
            return _context.Step.Any(e => e.Id == id);
        }
        private static Step Step(Step step) => new Step
        {
            Id = step.Id,
            StepId = step.StepId,
            Instruction = step.Instruction
        };
    }
}
