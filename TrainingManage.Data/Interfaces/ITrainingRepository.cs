using TrainingManage.Data.Models;

namespace TrainingManage.Data.Interfaces
{
    /// <summary>
    /// Rozhraní pro správu tréninků, rozšiřuje základní CRUD operace.
    /// </summary>
    public interface ITrainingRepository : IBaseRepository<Training>
    {
        /// <summary>
        /// Vrátí Training včetně Registrations a v nich načtených Person (pro detail/tracing).
        /// </summary>
        /// <param name="id">Identifikátor trainingu.</param>
        /// <returns>Training s navigacemi nebo null pokud neexistuje.</returns>
        Training? GetWithRegistrationsAndPersons(int id);

        /// <summary>
        /// Smaže training a všechny související záznamy (registrace, příslušné transakce apod.)
        /// </summary>
        /// <param name="id">Identifikátor trainingu, který se má smazat.</param>
        void DeleteWithDependencies(int id);
    }
}
