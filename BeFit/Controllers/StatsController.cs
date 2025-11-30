using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Controllers
{
    [Authorize]
    public class StatsController : Controller
    {
        // dostep baza danych
        private readonly ApplicationDbContext _context; 
        public StatsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            // data, 4 tygodnie temy
            var dataGraniczna = DateTime.Now.AddDays(-28);

            // pobieranie wpisow z bazy i grupowanie
            var stats = await _context.ExerciseEntries
                .Where(x => x.UserId == GetUserId()) // wpisy zalogowanego uzytkownika
                // wpis gdzie sesja byla pozniej niz 4 tyg temu
                .Where(x => x.TrainingSession.StartDateTime >= dataGraniczna)
                // dane o typie cwiczenia
                .Include(x => x.ExerciseType)
                // grupowanie po nazwie cwiczenia
                .GroupBy(x => x.ExerciseType.Name)
                // dla kazdej grupy nowy raport
                .Select(g => new ExerciseStat
                {
                    ExerciseName = g.Key, // nazwa grupy/cwiczenia
                    TrainingCount = g.Count(), // ile razy wystapilo
                    TotalRepetitions = g.Sum(x => x.SeriesCount * x.RepetitionsCount), // suma powtorzen
                    MaxWeight = g.Max(x => x.Weight), // max waga
                    AvgWeight = g.Average(x => x.Weight) // srednia waga
                })
               .ToListAsync();
            return View(stats); // przekazanie listy do widoku

        }
        private string GetUserId()
        {
            return User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
