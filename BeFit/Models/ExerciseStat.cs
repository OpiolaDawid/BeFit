using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExerciseStat
    {
        [Display(Name = "Nazwa Ćwiczenia")]
        public string ExerciseName { get; set; }

        [Display(Name = "Liczba Treningów")]
        public int TrainingCount { get; set; }

        [Display(Name = "Łącznie Powtórzeń")]
        public int TotalRepetitions { get; set; } // Serie * Powtórzenia

        [Display(Name = "Maks. Ciężar")]
        public double MaxWeight { get; set; }

        [Display(Name = "Średni Ciężar")]
        public double AvgWeight { get; set; }
    }
}