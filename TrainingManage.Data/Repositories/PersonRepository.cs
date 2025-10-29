using TrainingManage.Data.Interfaces;
using TrainingManage.Data.Models;

namespace TrainingManage.Data.Repositories
{
    /// <summary>
    /// Implementace repozitáře pro entitu Person, zahrnuje metody pro získání osob podle různých kritérií.
    /// </summary>
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        /// <summary>
        /// Konstruktor repozitáře přijímající databázový kontext.
        /// </summary>
        /// <param name="moviesDbContext">Databázový kontext.</param>
        public PersonRepository(TrainingDbContext moviesDbContext) : base(moviesDbContext)
        {
        }

        /// <summary>
        /// Získá všechny osoby podle hodnoty skrytí.
        /// </summary>
        /// <param name="hidden">True pro skryté osoby, false pro viditelné.</param>
        /// <returns>Seznam osob odpovídajících filtru.</returns>
        public IList<Person> GetAllByHidden(bool hidden)
        {
            return dbSet
                .Where(person => person.Hidden == hidden)
                .ToList();
        }

        /// <summary>
        /// Získá stránkovaný seznam osob.
        /// </summary>
        /// <param name="page">Číslo stránky (0-based).</param>
        /// <param name="pageSize">Počet položek na stránku.</param>
        /// <returns>Seznam osob na dané stránce.</returns>
        public IList<Person> GetAll(int page, int pageSize)
        {
            return dbSet
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /// <summary>
        /// Najde všechny osoby podle seznamu jejich ID.
        /// </summary>
        /// <param name="ids">Seznam ID osob.</param>
        /// <returns>Seznam osob odpovídajících zadaným ID.</returns>
        public IList<Person> FindAllByIds(IEnumerable<int> ids)
        {
            return dbSet.Where(person => ids.Contains(person.Id)).ToList();
        }
    }
}
