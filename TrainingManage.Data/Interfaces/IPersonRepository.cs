using TrainingManage.Data.Models;

namespace TrainingManage.Data.Interfaces
{
    /// <summary>
    /// Rozhraní pro práci s entitou Person, rozšiřuje základní CRUD operace.
    /// </summary>
    public interface IPersonRepository : IBaseRepository<Person>
    {
        /// <summary>
        /// Získá seznam osob podle stavu skrytí.
        /// </summary>
        /// <param name="hidden">True pro skryté osoby, false pro viditelné.</param>
        IList<Person> GetAllByHidden(bool hidden);

        /// <summary>
        /// Získá stránkovaný seznam osob.
        /// </summary>
        /// <param name="page">Číslo stránky (1-based).</param>
        /// <param name="pageSize">Počet položek na stránku.</param>
        IList<Person> GetAll(int page, int pageSize);

        /// <summary>
        /// Najde všechny osoby podle seznamu jejich ID.
        /// </summary>
        /// <param name="ids">Seznam ID osob.</param>
        IList<Person> FindAllByIds(IEnumerable<int> ids);
    }
}
