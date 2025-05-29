using System.ComponentModel.DataAnnotations;

namespace HygeeaMind.Models
{
    public class Specialist
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu.")]
        [StringLength(100, ErrorMessage = "Numele nu poate depăși 100 de caractere.")]
        [Display(Name = "Nume Complet")]
        public required string Name { get; set; } // Modificat: adăugat 'required'

        [Required(ErrorMessage = "Specializarea este obligatorie.")]
        [StringLength(100, ErrorMessage = "Specializarea nu poate depăși 100 de caractere.")]
        [Display(Name = "Specializare")]
        public required string Specialty { get; set; } // Modificat: adăugat 'required'

        [Required(ErrorMessage = "Descrierea este obligatorie.")]
        [Display(Name = "Descriere")]
        public required string Description { get; set; } // Modificat: adăugat 'required'

        [Display(Name = "Imagine Profil (URL)")]
        [Url(ErrorMessage = "Adresa URL a imaginii nu este validă.")]
        public string? ProfileImageUrl { get; set; } // Modificat: făcut nullable (string?) dacă e opțional.

        [EmailAddress(ErrorMessage = "Adresa de email nu este validă.")]
        [Display(Name = "Email")]
        public string? Email { get; set; } // Modificat: făcut nullable (string?) dacă e opțional.

        [Phone(ErrorMessage = "Numărul de telefon nu este valid.")]
        [Display(Name = "Telefon")]
        public string? PhoneNumber { get; set; } // Modificat: făcut nullable (string?) dacă e opțional.

        [Display(Name = "Experiență (ani)")]
        [Range(0, 100, ErrorMessage = "Experiența trebuie să fie între 0 și 100 de ani.")]
        public int ExperienceYears { get; set; }
    }
}