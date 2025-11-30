using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.DTO
{
    public class ExerciseEntryDto
    {
        [Display(Name = "Typ Ćwiczenia")]
        public int ExerciseTypeId { get; set; }
        [Display(Name = "Sesja Treningowa")]
        public int TrainingSessionId { get; set; }
        [Display(Name = "Ciężar (kg)")]
        public double Weight { get; set; }
        [Display(Name = "Liczba Serii")]
        public int SeriesCount { get; set; }
        [Display(Name = "Powtórzeń w serii")]
        public int RepetitionsCount { get; set; }
    }
}
