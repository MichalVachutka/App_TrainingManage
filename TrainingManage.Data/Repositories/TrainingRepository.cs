using Microsoft.EntityFrameworkCore;
using TrainingManage.Data.Interfaces;
using TrainingManage.Data.Models;

namespace TrainingManage.Data.Repositories
{
    /// <summary>
    /// Implementace repozitáře pro entitu Training.
    /// </summary>
    public class TrainingRepository : BaseRepository<Training>, ITrainingRepository
    {
        public TrainingRepository(TrainingDbContext trainingDbContext)
            : base(trainingDbContext)
        {
        }

        /// <summary>
        /// Vrátí Training včetně Registrations a v nich načtených Person.
        /// </summary>
        /// <param name="id">Identifikátor trainingu.</param>
        /// <returns>Training s načtenými navigacemi nebo null, pokud neexistuje.</returns>
        public Training? GetWithRegistrationsAndPersons(int id)
        {
            return trainingDbContext.Trainings
                .Include(t => t.Registrations)
                    .ThenInclude(r => r.Person)
                .FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Transakčně smaže training a všechny související záznamy (registrace, transakce apod.).
        /// </summary>
        /// <param name="id">Identifikátor trainingu, který se má smazat.</param>
        public void DeleteWithDependencies(int id)
        {
            using var transaction = trainingDbContext.Database.BeginTransaction();
            try
            {
                var training = trainingDbContext.Trainings
                    .Include(t => t.Registrations)
                    .FirstOrDefault(t => t.Id == id);

                if (training == null)
                    throw new KeyNotFoundException($"Training {id} not found");

                // Odebrání transakcí souvisejících s daným tréninkem (platby + rent shares)
                var paymentTransaction = trainingDbContext.PersonTransactions
                    .Where(t => t.Description == $"Trénink #{id}")
                    .ToList();
                if (paymentTransaction.Any())
                    trainingDbContext.PersonTransactions.RemoveRange(paymentTransaction);

                var rentShareTransactions = trainingDbContext.PersonTransactions
                    .Where(t => t.Description == $"Rent share #{id}")
                    .ToList();
                if (rentShareTransactions.Any())
                    trainingDbContext.PersonTransactions.RemoveRange(rentShareTransactions);

                // Odebrání všech registrací pro trénink
                if (training.Registrations != null && training.Registrations.Any())
                    trainingDbContext.Registrations.RemoveRange(training.Registrations);

                // Nakonec smazat samotný trénink
                trainingDbContext.Trainings.Remove(training);

                trainingDbContext.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                try
                {
                    transaction.Rollback();
                }
                catch
                {

                }
                throw;
            }
        }
    }
}
