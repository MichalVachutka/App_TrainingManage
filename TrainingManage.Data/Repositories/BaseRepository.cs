using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TrainingManage.Data.Interfaces;

namespace TrainingManage.Data.Repositories
{
    /// <summary>
    /// Poskytuje základní CRUD operace pro entitu daného typu TEntity.
    /// </summary>
    /// <typeparam name="TEntity">Typ entity, pro kterou je repozitář určen.</typeparam>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Kontext databáze použitý pro operace nad entitami.
        /// </summary>
        protected readonly TrainingDbContext trainingDbContext;

        /// <summary>
        /// DbSet odpovídající entitě TEntity v rámci databázového kontextu.
        /// </summary>
        protected readonly DbSet<TEntity> dbSet;

        /// <summary>
        /// Inicializuje novou instanci BaseRepository s daným databázovým kontextem.
        /// </summary>
        /// <param name="trainingDbContext">Instanci TrainingDbContext, ve které se budou provádět operace.</param>
        public BaseRepository(TrainingDbContext trainingDbContext)
        {
            this.trainingDbContext = trainingDbContext;
            dbSet = trainingDbContext.Set<TEntity>();
        }

        /// <summary>
        /// Vyhledá entitu podle jejího primárního klíče (Id).
        /// </summary>
        /// <param name="id">Hodnota primárního klíče entity.</param>
        /// <returns>Entita, pokud existuje; jinak null.</returns>
        public TEntity? FindById(int id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// Ověří, zda entita s daným Id existuje, přičemž se neaktivuje sledování změn.
        /// </summary>
        /// <param name="id">Hodnota primárního klíče entity.</param>
        /// <returns>True, pokud entita existuje; jinak False.</returns>
        public bool ExistWithId(int id)
        {
            return dbSet
                .AsNoTracking()
                .Any(exist => EF.Property<int>(exist, "Id") == id);
        }

        /// <summary>
        /// Vrátí seznam všech entit daného typu z databáze.
        /// </summary>
        /// <returns>Seznam všech entit typu TEntity.</returns>
        public IList<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        /// <summary>
        /// Vloží novou entitu do databáze a uloží změny.
        /// </summary>
        /// <param name="entity">Entita k vložení.</param>
        /// <returns>Právě vložená entita (včetně hodnot generovaných databází).</returns>
        public TEntity Insert(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = dbSet.Add(entity);
            trainingDbContext.SaveChanges();
            return entityEntry.Entity;
        }

        /// <summary>
        /// Aktualizuje existující entitu v databázi a uloží změny.
        /// </summary>
        /// <param name="entity">Entita s aktualizovanými hodnotami.</param>
        /// <returns>Aktualizovaná entita.</returns>
        public TEntity Update(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = dbSet.Update(entity);
            trainingDbContext.SaveChanges();
            return entityEntry.Entity;
        }

        /// <summary>
        /// Odstraní entitu s daným Id z databáze.
        /// </summary>
        /// <param name="id">Hodnota primárního klíče entity, která se má odstranit.</param>
        public void Delete(int id)
        {
            TEntity? entity = dbSet.Find(id);

            if (entity is null)
                return;

            try
            {
                dbSet.Remove(entity);
                trainingDbContext.SaveChanges();
            }
            catch
            {
                trainingDbContext.Entry(entity).State = EntityState.Unchanged;
                throw;
            }
        }
    }
}

