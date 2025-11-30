using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExerciseEntry
    {
        public int Id { get; set; } // unikalny numer wpisu w bazie
        
        // 1. Informacja co bylo cwiczone
        public int ExerciseTypeId { get; set; }
        [Display(Name = "Typ Ćwiczenia")]
        public virtual ExerciseType ExerciseType { get; set; }

        // 2. Informacja o treningu (do ktorego wpisu treningowego nalezy)
        public int TrainingSessionId { get; set; }
        [Display(Name = "Sesja Treningowa")]
        public virtual TrainingSession TrainingSession { get; set; }

        // DANE
        [Required]
        [Range(0.1, 1000, ErrorMessage = "Ciężar musi być większy od 0")]
        [Display(Name = "Ciężar (kg)")]
        public double Weight { get; set; } // obciazenie uzyte podczas cwiczenia (kg)
        [Required]
        [Range(1, 2000, ErrorMessage = "Podaj poprawną liczbę powtórzeń")]
        [Display(Name = "Powtórzeń w serii")]
        public int SeriesCount { get; set; } // ile serii
        [Required]
        [Range(1, 2000, ErrorMessage = "Podaj poprawną liczbę serii")]
        [Display(Name = "Powtórzeń w serii")]
        public int RepetitionsCount { get; set; } // ile powtorzen w serii

        public string? UserId { get; set; } // identyfikator uzytkownika, ktory wykonuje cwiczenie
        public virtual ApplicationUser? User { get; set; }
    }
}
