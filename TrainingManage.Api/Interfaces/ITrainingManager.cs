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
        /// Vrátí trénink podle ID nebo null, pokud neexistuje.
        /// </summary>
        TrainingDto? GetTraining(int id);

        /// <summary>
        /// Vytvoří nový trénink a vrátí vytvořený DTO.
        /// </summary>
        TrainingDto CreateTraining(TrainingDto trainingDto);

        /// <summary>
        /// Aktualizuje existující trénink podle ID, vrací aktualizovaný DTO nebo null, pokud neexistuje.
        /// </summary>
        TrainingDto? UpdateTraining(int id, TrainingDto trainingDto);

        /// <summary>
        /// Odstraní trénink podle ID.
        /// </summary>
        void DeleteTraining(int id);

        /// <summary>
        /// Vrátí detail tréninku (včetně registrací). Vrací null, pokud trénink neexistuje.
        /// </summary>
        TrainingDetailDto? GetTrainingDetail(int id);
    }
}
