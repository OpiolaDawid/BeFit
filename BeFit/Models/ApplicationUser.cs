using Microsoft.AspNetCore.Identity;
namespace BeFit.Models

{
    public class ApplicationUser : IdentityUser // uzytkownik dziedziczacy cechy standardowego uzytkownika
    {
        // tu mozna dodac dodatkowe wlasciwosci uzytkownika np. waga wzrost itp.
    }
}
