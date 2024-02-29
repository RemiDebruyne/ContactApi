using ContactApiDTO.Models;

namespace ContactApiDTO.Data
{
    public class AppDbContext
    {
        public List<Contact> Contacts { get; set; } = new()
        {
                       new Contact { Id = 1, FirstName = "John", LastName = "Doe", BirthDate = new DateTime(1985, 7, 15), Gender = "M" },
            new Contact { Id = 2, FirstName = "Jane", LastName = "Smith", BirthDate = new DateTime(1990, 5, 25), Gender = "F" },
            new Contact { Id = 3, FirstName = "Alice", LastName = "Johnson", BirthDate = new DateTime(1978, 11, 3), Gender = "F" },
            new Contact { Id = 4, FirstName = "Bob", LastName = "Brown", BirthDate = new DateTime(2000, 9, 10), Gender = "M" }
        };
        public AppDbContext()
        {
        }
#nullable disable
        
#nullable enable
    }
}
