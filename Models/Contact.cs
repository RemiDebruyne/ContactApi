using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ContactApiDTO.Models
{
    [Table("contact")]
    public class Contact
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("firstname")]
        [Required]
        [RegularExpression(@"^[A-Z][A-Za-z\- ]*", ErrorMessage = "FirstName must start with an uppercase letter !")]
        public string? FirstName { get; set; }
        [Column("lastname")]
        [Required]
        [RegularExpression(@"^[A-Z\- ]*", ErrorMessage = "LastName must be in uppercase !")]
        public string? LastName { get; set; }
        public string? FullName => FirstName + " " + LastName; // get => pas d'attribut/variable FullName
        [Column("birth_date")]
        [Required]
        [JsonIgnore] // la prop sera ignorée pour la serialisation de l'objet
        public DateTime BirthDate { get; set; }
        public int Age
        {
            get // get => pas d'attribut/variable age
            {
                int age = DateTime.Now.Year - BirthDate.Year;
                if (BirthDate > DateTime.Now.AddYears(-age)) //ajout de vérification mois/jour
                    age--;
                return age;
            }
        }
        [Column("gender")]
        [Required]
        [RegularExpression(@"[FMN]", ErrorMessage = "Gender must be either F, M, or N.")]
        [StringLength(1)]
        public string? Gender { get; set; }
    }
}
