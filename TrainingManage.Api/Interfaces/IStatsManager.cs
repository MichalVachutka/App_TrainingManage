using TrainingManage.Api.Models.Stats;

namespace TrainingManage.Api.Managers
{
    /// <summary>
    /// Rozhraní pro správce statistik poskytující agregované přehledy pro jednu osobu.
    /// </summary>
    public interface IStatsManager
    {
        /// <summary>
        /// Získá měsíční součty plateb osoby za posledních 12 měsíců.
        /// </summary>
        /// <param name="personId">Identifikátor osoby.</param>
        /// <returns>DTO obsahující pole měsíčních popisků a odpovídající součty plateb.</returns>
        Task<PersonStatsDto> GetPersonStatsAsync(int personId);

        /// <summary>
        /// Získá rozpis příjmů a výdajů osoby rozdělený po měsících za posledních 12 měsíců.
        /// </summary>
        /// <param name="personId">Identifikátor osoby.</param>
        /// <returns>DTO obsahující popisky měsíců, pole příjmů a pole výdajů.</returns>
        Task<PersonIncomeExpenseDto> GetIncomeVsExpenseAsync(int personId);

        /// <summary>
        /// Vrátí rozdělení výdajů osoby podle kategorií za příslušné období.
        /// </summary>
        /// <param name="personId">Identifikátor osoby.</param>
        /// <returns>DTO obsahující názvy kategorií a odpovídající součty výdajů.</returns>
        Task<ExpenseBreakdownDto> GetExpenseBreakdownAsync(int personId);

        /// <summary>
        /// Poskytne měsíční přehled počtu účastí osoby na trénincích za posledních 12 měsíců.
        /// </summary>
        /// <param name="personId">Identifikátor osoby.</param>
        /// <returns>DTO obsahující pole měsíčních popisků a pole počtů účastí.</returns>
        Task<ParticipationDto> GetParticipationAsync(int personId);
    }
}
