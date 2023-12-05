using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentPlanner.Data;
using SacramentPlanner.Models;
using System.Diagnostics;

namespace SacramentPlanner.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly SacramentPlannerContext _context;

        public MeetingsController(SacramentPlannerContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var meetings = await _context.Meeting
                .Include(m => m.Talks)
                    .ThenInclude(t => t.Member)  
                .ToListAsync();

            return View(meetings);


        }

        // GET: Meetings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Meeting == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // GET: Meetings/Create
        public IActionResult Create()
        {
            var meeting = new Meeting();
            meeting.Talks = new List<Talk>();

            // Get all the members
            var members = _context.Member.ToList();

            if (members == null || members.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "No members available.");
                return View(meeting);
            }

            // Put members in a dropdown so we can choose them to speak
            ViewBag.Members = new SelectList(members, "Id", "FullName");

            return View(meeting);
        }

        // POST: Meetings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,ConductingLeader,OpeningHymn,SacramentHymn,ClosingHymn,IntermediateHymn,MusicalNumber,OpeningPrayer,ClosingPrayer")] Meeting meeting, List<Talk>Talks)
        {
            if (ModelState.IsValid)
            {
                //here is where we add the talks to the Meeting
                meeting.Talks = Talks;


                _context.Add(meeting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meeting);
        }


        public IActionResult CreateTalk()
        {
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTalk([Bind("Subject,MemberId")] Talk talk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(talk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FullName", talk.MemberId);
            return View(talk);
        }




        // GET: Meetings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Meeting == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting.FindAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return View(meeting);
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,ConductingLeader,OpeningHymn,SacramentHymn,ClosingHymn,IntermediateHymn,MusicalNumber,OpeningPrayer,ClosingPrayer")] Meeting meeting)
        {
            if (id != meeting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meeting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingExists(meeting.Id))
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
            return View(meeting);
        }

        // GET: Meetings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Meeting == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Meeting == null)
            {
                return Problem("Entity set 'SacramentPlannerContext.Meeting'  is null.");
            }
            var meeting = await _context.Meeting.FindAsync(id);
            if (meeting != null)
            {
                _context.Meeting.Remove(meeting);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingExists(int id)
        {
          return (_context.Meeting?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
