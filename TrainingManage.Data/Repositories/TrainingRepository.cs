using TrainingManage.Data.Interfaces;
using TrainingManage.Data.Models;

namespace TrainingManage.Data.Repositories
{
    /// <summary>
    /// Implementace repozitáře pro entitu Training, využívá základní CRUD operace.
    /// </summary>
    public class TrainingRepository : BaseRepository<Training>, ITrainingRepository
    {
        /// <summary>
        /// Konstruktor repozitáře přijímající databázový kontext.
        /// </summary>
        /// <param name="trainingDbContext">Databázový kontext.</param>
        public TrainingRepository(TrainingDbContext trainingDbContext)
            : base(trainingDbContext)
        {
        }
    }
}
