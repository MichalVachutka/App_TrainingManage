using TrainingManage.Api.Models.Stats;
using TrainingManage.Data.Interfaces;

namespace TrainingManage.Api.Managers
{
    /// <summary>
    /// Správce pro získávání statistik osoby.
    /// Deleguje agregované dotazy na IStatsRepository a převádí výsledky do DTO.
    /// </summary>
    public class StatsManager : IStatsManager
    {
        private readonly IStatsRepository statsRepository;

        /// <summary>
        /// Vytvoří instanci StatsManager s injektovaným repozitářem statistik.
        /// </summary>
        /// <param name="statsRepository">Implementace IStatsRepository pro dotazy statistik.</param>
        public StatsManager(IStatsRepository statsRepository) => this.statsRepository = statsRepository;

        /// <summary>
        /// Získá měsíční součty plateb osoby za posledních 12 měsíců a vrátí je jako DTO.
        /// </summary>
        /// <param name="personId">Identifikátor osoby.</param>
        /// <returns>PersonStatsDto obsahující pole měsíčních popisků a odpovídajících součtů plateb.</returns>
        public async Task<PersonStatsDto> GetPersonStatsAsync(int personId)
        {
            var (labels, values) = await statsRepository.GetPersonPaymentsPerLast12MonthsAsync(personId);
            return new PersonStatsDto 
            { 
                Labels = labels, 
                Values = values 
            };
        }

        /// <summary>
        /// Získá porovnání příjmů a výdajů osoby rozdělené po měsících za posledních 12 měsíců.
        /// </summary>
        /// <param name="personId">Identifikátor osoby.</param>
        /// <returns>PersonIncomeExpenseDto obsahující popisky, pole příjmů a pole výdajů.</returns>
        public async Task<PersonIncomeExpenseDto> GetIncomeVsExpenseAsync(int personId)
        {
            var (labels, income, expense) = await statsRepository.GetIncomeVsExpenseForLast12MonthsAsync(personId);
            return new PersonIncomeExpenseDto 
            { 
                Labels = labels, 
                Income = income, 
                Expense = expense 
            };
        }

        /// <summary>
        /// Získá rozdělení výdajů osoby podle kategorií a vrátí je jako DTO.
        /// </summary>
        /// <param name="personId">Identifikátor osoby.</param>
        /// <returns>ExpenseBreakdownDto obsahující názvy kategorií a součty výdajů pro každou kategorii.</returns>
        public async Task<ExpenseBreakdownDto> GetExpenseBreakdownAsync(int personId)
        {
            var (categories, values) = await statsRepository.GetExpenseBreakdownAsync(personId);
            return new ExpenseBreakdownDto 
            { 
                Categories = categories, 
                Values = values 
            };
        }

        /// <summary>
        /// Získá měsíční přehled počtu účastí osoby na trénincích za posledních 12 měsíců.
        /// </summary>
        /// <param name="personId">Identifikátor osoby.</param>
        /// <returns>ParticipationDto obsahující popisky měsíců a pole počtů účastí v jednotlivých měsících.</returns>
        public async Task<ParticipationDto> GetParticipationAsync(int personId)
        {
            var (labels, values) = await statsRepository.GetParticipationLast12MonthsAsync(personId);
            return new ParticipationDto 
            { 
                Labels = labels, 
                Values = values 
            };
        }
    }
}
