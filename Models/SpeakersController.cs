using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentPlanner.Data;

namespace SacramentPlanner.Models
{
    public class SpeakersController : Controller
    {
        private readonly SacramentPlannerContext _context;

        public SpeakersController(SacramentPlannerContext context)
        {
            _context = context;
        }

        // GET: Speakers
        public async Task<IActionResult> Index()
        {
            var sacramentPlannerContext = _context.Speaker.Include(s => s.Meeting);
            return View(await sacramentPlannerContext.ToListAsync());
        }

        // GET: Speakers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Speaker == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speaker
                .Include(s => s.Meeting)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // GET: Speakers/Create
        public IActionResult Create()
        {
            ViewData["MeetingId"] = new SelectList(_context.Meeting, "Id", "Id");
            return View();
        }

        // POST: Speakers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Subject,MeetingId")] Talk speaker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(speaker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MeetingId"] = new SelectList(_context.Meeting, "Id", "Id", speaker.MeetingId);
            return View(speaker);
        }

        // GET: Speakers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Speaker == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speaker.FindAsync(id);
            if (speaker == null)
            {
                return NotFound();
            }
            ViewData["MeetingId"] = new SelectList(_context.Meeting, "Id", "Id", speaker.MeetingId);
            return View(speaker);
        }

        // POST: Speakers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Subject,MeetingId")] Talk speaker)
        {
            if (id != speaker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speaker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeakerExists(speaker.Id))
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
            ViewData["MeetingId"] = new SelectList(_context.Meeting, "Id", "Id", speaker.MeetingId);
            return View(speaker);
        }

        // GET: Speakers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Speaker == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speaker
                .Include(s => s.Meeting)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // POST: Speakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Speaker == null)
            {
                return Problem("Entity set 'SacramentPlannerContext.Talk'  is null.");
            }
            var speaker = await _context.Speaker.FindAsync(id);
            if (speaker != null)
            {
                _context.Speaker.Remove(speaker);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpeakerExists(int id)
        {
          return (_context.Speaker?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
