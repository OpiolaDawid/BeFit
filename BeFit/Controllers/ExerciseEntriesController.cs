using BeFit.Data;
using BeFit.Models;
using BeFit.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Controllers
{
    [Authorize]
    public class ExerciseEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExerciseEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExerciseEntries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ExerciseEntries
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .Where(e => e.UserId == GetUserId());
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ExerciseEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseEntry = await _context.ExerciseEntries
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .Where(e => e.UserId == GetUserId())
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exerciseEntry == null)
            {
                return NotFound();
            }

            return View(exerciseEntry);
        }

        // GET: ExerciseEntries/Create
        public IActionResult Create()
        {
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name");
            ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions.Where(s => s.UserId == GetUserId()), "Id", "StartDateTime");
            return View();
        }

        // POST: ExerciseEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExerciseEntryDto entryDto)
        {
            if (ModelState.IsValid)
            {
                var exerciseEntry = new ExerciseEntry
                {
                    ExerciseTypeId = entryDto.ExerciseTypeId,
                    TrainingSessionId = entryDto.TrainingSessionId,
                    Weight = entryDto.Weight,
                    SeriesCount = entryDto.SeriesCount,
                    RepetitionsCount = entryDto.RepetitionsCount,
                    UserId = GetUserId()
                };
                _context.Add(exerciseEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name", entryDto.ExerciseTypeId);
            ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions.Where(s => s.UserId == GetUserId()), "Id", "StartDateTime");
            return View(entryDto);
        }

        // GET: ExerciseEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseEntry = await _context.ExerciseEntries.FirstOrDefaultAsync(m => m.Id == id && m.UserId == GetUserId());
            if (exerciseEntry == null)
            {
                return NotFound();
            }
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name", exerciseEntry.ExerciseTypeId);
            ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions.Where(s => s.UserId == GetUserId()), "Id", "StartDateTime");
            return View(exerciseEntry);
        }

        // POST: ExerciseEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ExerciseTypeId,TrainingSessionId,Weight,SeriesCount,RepetitionsCount")] ExerciseEntry exerciseEntry)
        {
            if (id != exerciseEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exerciseEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseEntryExists(exerciseEntry.Id))
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
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name", exerciseEntry.ExerciseTypeId);
            ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions.Where(s => s.UserId == GetUserId()),"Id","StartDateTime");
            return View(exerciseEntry);
        }

        // GET: ExerciseEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseEntry = await _context.ExerciseEntries
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .Where(e => e.UserId == GetUserId())
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exerciseEntry == null)
            {
                return NotFound();
            }

            return View(exerciseEntry);
        }

        // POST: ExerciseEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exerciseEntry = await _context.ExerciseEntries.FirstOrDefaultAsync(m => m.Id == id && m.UserId == GetUserId());
            if (exerciseEntry != null)
            {
                _context.ExerciseEntries.Remove(exerciseEntry);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseEntryExists(int id)
        {
            return _context.ExerciseEntries.Any(e => e.Id == id);
        }
        string GetUserId()
        {
            return User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
