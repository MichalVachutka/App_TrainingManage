using Microsoft.EntityFrameworkCore;
using TrainingManage.Data.Interfaces;
using TrainingManage.Data.Models;

namespace TrainingManage.Data.Repositories
{
    /// <summary>
    /// Implementace repozitáře pro entitu Registration s rozšířenými dotazy.
    /// </summary>
    public class RegistrationRepository : BaseRepository<Registration>, IRegistrationRepository
    {
        /// <summary>
        /// Konstruktor repozitáře s databázovým kontextem.
        /// </summary>
        /// <param name="trainingDbContext">Databázový kontext.</param>
        public RegistrationRepository(TrainingDbContext trainingDbContext)
            : base(trainingDbContext)
        {
        }

        /// <summary>
        /// Získá seznam registrací pro danou osobu, včetně dat tréninku, seřazený podle data registrace sestupně.
        /// </summary>
        /// <param name="personId">ID osoby.</param>
        /// <returns>Seznam registrací.</returns>
        public IList<Registration> GetByPerson(int personId)
        {
            return dbSet
                .Include(r => r.Training)
                .Where(r => r.PersonId == personId)
                .OrderByDescending(r => r.RegisteredOn)
                .ToList();
        }

        /// <summary>
        /// Získá seznam registrací pro daný trénink, včetně dat osoby, seřazený podle data registrace sestupně.
        /// </summary>
        /// <param name="trainingId">ID tréninku.</param>
        /// <returns>Seznam registrací.</returns>
        public IList<Registration> GetByTraining(int trainingId)
        {
            return dbSet
                .Include(r => r.Person)
                .Where(r => r.TrainingId == trainingId)
                .OrderByDescending(r => r.RegisteredOn)
                .ToList();
        }
    }
}
