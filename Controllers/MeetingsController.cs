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
                .Include(meeting => meeting.Talks)
                    .ThenInclude(talk => talk.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meeting == null)
            {
                return NotFound();
            }

            // Set up two list of speakers, one for before the intermediate hymn and one for after.
            int talksCount = meeting.Talks.Count;
            int talksBefore = (int)Math.Ceiling(talksCount / 2f); // The larger number speaks before the hymn
            int talksAfter = talksCount - talksBefore;

            //System.Diagnostics.Debug.WriteLine($"{talksCount} {talksBefore} {talksAfter}");

            List<Talk> beforeList = meeting.Talks.Take(talksBefore).ToList<Talk>();  //Split the speakers list in half
            List<Talk> afterList = meeting.Talks.Skip(talksBefore).Take(talksAfter).ToList<Talk>();

            ViewData.Add("before", beforeList);
            ViewData.Add("after", afterList);


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
            ViewBag.Members = new SelectList(members, "FullName", "FullName");

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
            //The LAST PASTED CODE
            var meeting = await _context.Meeting
                .Include(meeting => meeting.Talks)
                    .ThenInclude(talk => talk.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meeting == null)
            {
                return NotFound();
            }

            // Set up two list of speakers, one for before the intermediate hymn and one for after.
            int talksCount = meeting.Talks.Count;
            int talksBefore = (int)Math.Ceiling(talksCount / 2f); // The larger number speaks before the hymn
            int talksAfter = talksCount - talksBefore;

            //System.Diagnostics.Debug.WriteLine($"{talksCount} {talksBefore} {talksAfter}");

            List<Talk> beforeList = meeting.Talks.Take(talksBefore).ToList<Talk>();  //Split the speakers list in half
            List<Talk> afterList = meeting.Talks.Skip(talksBefore).Take(talksAfter).ToList<Talk>();

            ViewData.Add("before", beforeList);
            ViewData.Add("after", afterList);

            var members = _context.Member.ToList();
            // Put members in a dropdown so we can choose them to speak
            ViewBag.Members = new SelectList(members, "FullName", "FullName");

            //var meeting1 = await _context.Meeting.FindAsync(id);
            
            return View(meeting);
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,ConductingLeader,OpeningHymn,Talks,SacramentHymn,ClosingHymn,IntermediateHymn,MusicalNumber,OpeningPrayer,ClosingPrayer")] Meeting meeting)
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

        //this I added 
        public async Task<IActionResult> DeleteTalk(int? id, int? talkId)
        {
            if (id == null || talkId == null || _context.Meeting == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting
                .Include(m => m.Talks)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (meeting == null)
            {
                return NotFound();
            }

            var talk = meeting.Talks.FirstOrDefault(t => t.Id == talkId);

            if (talk == null)
            {
                return NotFound();
            }

            return View(talk);
        }

        // POST: Meetings/DeleteTalk/5
        [HttpPost, ActionName("DeleteTalk")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTalkConfirmed(int id, int talkId)
        {
            if (_context.Meeting == null)
            {
                return Problem("Entity set 'SacramentPlannerContext.Meeting' is null.");
            }

            var meeting = await _context.Meeting
                .Include(m => m.Talks)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (meeting != null)
            {
                var talk = meeting.Talks.FirstOrDefault(t => t.Id == talkId);

                if (talk != null)
                {
                    _context.Speaker.Remove(talk);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Edit), new { id = id }); // Redirect back to the Edit view
        }

        private bool MeetingExists(int id)
        {
          return (_context.Meeting?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
