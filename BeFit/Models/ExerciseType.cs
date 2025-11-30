using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExerciseType
    {
        public int Id { get; set; } // numer wpisu w bazie (unikalny numer)

        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [MaxLength(50, ErrorMessage = "Nazwa jest za długa")] // ograniczenie nazwy do 50 znakow
        [Display(Name = "Nazwa Ćwiczenia")]
        public string Name { get; set; } // nazwa cwiczenia
    }

}
