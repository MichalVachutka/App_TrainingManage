using TrainingManage.Data.Models;

namespace TrainingManage.Data.Interfaces
{
    /// <summary>
    /// Rozhraní pro správu tréninků, rozšiřuje základní CRUD operace.
    /// </summary>
    public interface ITrainingRepository : IBaseRepository<Training>
    {
    }
}
