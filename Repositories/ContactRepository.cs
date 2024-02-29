//using ContactApiDTO.Data;
//using ContactApiDTO.Models;
//using System.Linq.Expressions;

//namespace ContactApiDTO.Repositories
//{
//    public class ContactRepository : IRepository<Contact>
//    {
//        private readonly AppDbContext _db;

//        public ContactRepository(AppDbContext db)
//        {
//            _db = db;
//        }

//        // CREATE
//        public Contact? Add(Contact contact)
//        {
//            var addEntry = _db.Contacts.Add(contact); // retourne un EntityEntry<Contact> qui enveloppe le nouveau contact créé
//            _db.SaveChanges();

//            if (addEntry.Entity.Id > 0) // si l'entité est bien ajoutée l'id est > 0
//                    return addEntry.Entity;

//            return null; // erreur lors de l'ajout
//        }


//        // READ
//        public Contact? Get(int id)
//        {
//            //return _db.Contacts.Find(id); // ne fonctionne que sur un DbSet<> (EFCore)
//            return _db.Contacts.FirstOrDefault(c => c.Id == id);
//        }

//        public Contact? Get(Expression<Func<Contact, bool>> predicate)
//        {
//            return _db.Contacts.FirstOrDefault(predicate);
//        }

//        public IEnumerable<Contact> GetAll()
//        {
//            return _db.Contacts; 
//            // DbSet<> implémente l'interface IEnumerable
//            // en ne faisant pas le .ToList() tout de suite, on repousse l'exécution de la requête LINQ
//            // cela est plus otpimisé/pratique
//        }

//        public IEnumerable<Contact> GetAll(Expression<Func<Contact, bool>> predicate)
//        {
//            return _db.Contacts.Where(predicate);
//        }


//        // UPDATE
//        public Contact? Update(Contact contact)
//        {
//            var contactFromDb = Get(contact.Id); // entitée récupérée donc TRAQUEE par l'ORM (EFCore)

//            if (contactFromDb == null)
//                return null; // erreur lors de la modification => contact non trouvé

//            if (contactFromDb.FirstName != contact.FirstName)
//                contactFromDb.FirstName = contact.FirstName;
//            if (contactFromDb.LastName != contact.LastName)
//                contactFromDb.LastName = contact.LastName;
//            if (contactFromDb.BirthDate != contact.BirthDate)
//                contactFromDb.BirthDate = contact.BirthDate;
//            if (contactFromDb.Gender != contact.Gender)
//                contactFromDb.Gender = contact.Gender;

//            if (_db.SaveChanges() == 0)
//                return null; // erreur lors de la modification

//            return contactFromDb;
//        }


//        // DELETE
//        public bool Delete(int id)
//        {
//            var contactFromDb = Get(id); // entitée récupérée donc TRAQUEE par l'ORM (EFCore)

//            if (contactFromDb == null)
//                return false; // erreur lors de la suppression => contact non trouvé

//            _db.Contacts.Remove(contactFromDb);

//            return _db.SaveChanges() > 0;
//        }
//    }
//}
