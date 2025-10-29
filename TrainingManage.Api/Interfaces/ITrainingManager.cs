using TrainingManage.Api.Models.Training;

namespace TrainingManage.Api.Interfaces
{
    /// <summary>
    /// Rozhraní pro správu tréninků.
    /// </summary>
    public interface ITrainingManager
    {
        /// <summary>
        /// Vrátí seznam všech tréninků.
        /// </summary>
        IList<TrainingDto> GetAllTrainings();

        /// <summary>
        /// Vrátí trénink podle ID.
        /// </summary>
        TrainingDto? GetTraining(int id);

        /// <summary>
        /// Vytvoří nový trénink.
        /// </summary>
        TrainingDto CreateTraining(TrainingDto trainingDto);

        /// <summary>
        /// Aktualizuje existující trénink podle ID.
        /// </summary>
        TrainingDto? UpdateTraining(int id, TrainingDto trainingDto);

        /// <summary>
        /// Odstraní trénink podle ID.
        /// </summary>
        void DeleteTraining(int id);

        /// <summary>
        /// Vrátí detail tréninku podle ID.
        /// </summary>
        TrainingDetailDto GetTrainingDetail(int id);
    }
}
