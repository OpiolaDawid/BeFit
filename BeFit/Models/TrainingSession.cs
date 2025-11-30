using System; // wymagane do obslugi daty
using System.ComponentModel.DataAnnotations; // do etykiet
namespace BeFit.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Data i godzina rozpoczęcia")] // etykieta do wyswietlania w widokach
        public DateTime StartDateTime { get; set; } // kiedy rozpoczeto cwiczenie
        [Required]
        [Display(Name = "Data i godzina zakończenia")]
        public DateTime EndDateTime { get; set; } // kiedy zakonczono cwiczenie

        public string? UserId { get; set; } // identyfikator uzytkownika, ktory wykonuje trening
        public virtual ApplicationUser? User { get; set; }
    }
}
