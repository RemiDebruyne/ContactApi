using System.Linq.Expressions;

namespace ContactApiDTO.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // CREATE
        //bool Add(TEntity entity); // bool => si ça s'est bien passé
        //int Add(TEntity entity); // int => id de l'entité
        TEntity? Add(TEntity entity); // TEntity => on retourne l'entité qui vient d'être ajoutée

        // READ
        TEntity? Get(int id); // uniquement l'id
        TEntity? Get(Expression<Func<TEntity, bool>> predicate); // avec un délégué/fonction pour filtrer
        IEnumerable<TEntity> GetAll(); // toutes les entités
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate); // avec un délégué/fonction pour filtrer

        // UPDATE
        //bool Update(TEntity contact); // bool => si ça s'est bien passé
        TEntity? Update(TEntity entity); // TEntity => on retourne l'entité qui vient d'être modifiée

        // DELETE
        bool Delete(int id);

    }
}
