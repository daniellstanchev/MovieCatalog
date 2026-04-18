using Microsoft.AspNetCore.Identity;

namespace MoviesCatalog.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
}