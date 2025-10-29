using TrainingManage.Data.Models;

namespace TrainingManage.Data.Interfaces
{
    /// <summary>
    /// Rozhraní pro správu registrací, rozšiřuje základní CRUD operace.
    /// </summary>
    public interface IRegistrationRepository : IBaseRepository<Registration>
    {
        /// <summary>
        /// Získá seznam registrací pro danou osobu.
        /// </summary>
        /// <param name="personId">ID osoby.</param>
        IList<Registration> GetByPerson(int personId);

        /// <summary>
        /// Získá seznam registrací pro daný trénink.
        /// </summary>
        /// <param name="trainingId">ID tréninku.</param>
        IList<Registration> GetByTraining(int trainingId);
    }
}
