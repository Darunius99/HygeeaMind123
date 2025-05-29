using System.ComponentModel.DataAnnotations;

namespace HygeeaMind.Models
{
    public class Article
    {
        public int Id { get; set; }

        // Adaugă 'required' la proprietățile non-nullable care sunt obligatorii
        [Required(ErrorMessage = "Titlul este obligatoriu.")]
        [StringLength(200, ErrorMessage = "Titlul nu poate depăși 200 de caractere.")]
        [Display(Name = "Titlu Articol")]
        public required string Title { get; set; } // Modificat: adăugat 'required'

        [Required(ErrorMessage = "Conținutul este obligatoriu.")]
        [Display(Name = "Conținut Articol")]
        public required string Content { get; set; } // Modificat: adăugat 'required'

        [Display(Name = "Imagine (URL)")]
        [Url(ErrorMessage = "Adresa URL a imaginii nu este validă.")]
        public string? ImageUrl { get; set; } // Modificat: făcut nullable (string?) dacă e opțional.
                                              // Dacă dorești să fie obligatoriu, adaugă 'required' și [Required].

        [Display(Name = "Data Publicării")]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; } = DateTime.Now;
    }
}