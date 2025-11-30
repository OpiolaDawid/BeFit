using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.DTO
{
    public class TrainingSessionDto
    {
        [Required]
        [Display(Name = "Data i czas rozpoczęcia")]
        public DateTime StartDateTime { get; set; }
        [Required]
        [Display(Name = "Data i czas zakończenia")]
        public DateTime EndDateTime { get; set; }
    }
}
