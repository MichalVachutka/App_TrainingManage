namespace TrainingManage.Data.Interfaces
{

    /// <summary>
    /// Rozhraní pro repozitář statistik obsahující agregované dotazy a přehledy pro osobu.
    /// </summary>
    public interface IStatsRepository
    {
        /// <summary>
        /// Získá měsíční součty kladných plateb (příjmů) pro zadanou osobu za posledních 12 měsíců.
        /// </summary>
        /// <param name="personId">Identifikátor osoby.</param>
        /// <returns>Tuple s polem popisků (Labels) pro jednotlivé měsíce a odpovídajícími součty plateb (Values).</returns>
        Task<(string[] Labels, decimal[] Values)> GetPersonPaymentsPerLast12MonthsAsync(int personId);

        /// <summary>
        /// Získá rozpis příjmů a výdajů pro zadanou osobu za posledních 12 měsíců, rozdělený po měsících.
        /// </summary>
        /// <param name="personId">Identifikátor osoby.</param>
        /// <returns>Tuple s poli Labels, Income (příjmy) a Expense (výdaje) pro každé z 12 období.</returns>
        Task<(string[] Labels, decimal[] Income, decimal[] Expense)> GetIncomeVsExpenseForLast12MonthsAsync(int personId);

        /// <summary>
        /// Vrátí rozdělení (kategorie a hodnota) výdajů dané osoby za celé období dotazu.
        /// </summary>
        /// <param name="personId">Identifikátor osoby.</param>
        /// <returns>Tuple s poli Categories (názvy kategorií) a Values (součty výdajů pro každou kategorii).</returns>
        Task<(string[] Categories, decimal[] Values)> GetExpenseBreakdownAsync(int personId);

        /// <summary>
        /// Poskytne měsíční přehled počtu účastí osoby na trénincích za posledních 12 měsíců.
        /// </summary>
        /// <param name="personId">Identifikátor osoby.</param>
        /// <returns>Tuple s poli Labels (měsíční popisky) a Values (počet účastí v každém měsíci).</returns>
        Task<(string[] Labels, int[] Values)> GetParticipationLast12MonthsAsync(int personId);
    }
}
