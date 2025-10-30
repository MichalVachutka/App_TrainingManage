using Microsoft.EntityFrameworkCore;
using TrainingManage.Data.Interfaces;

namespace TrainingManage.Data.Repositories
{
    /// <summary>
    /// Implementace repozitáře pro statistiky. Vrací hodnotové n-tice (ValueTuple), ne DTO.
    /// Obsahuje dotazy pro souhrn plateb, příjmů/výdajů, rozklad výdajů a účasti v posledních 12 měsících.
    /// </summary>
    public class StatsRepository : IStatsRepository
    {
        private readonly TrainingDbContext trainingDbContext;

        public StatsRepository(TrainingDbContext trainingDbContext)
        {
            this.trainingDbContext = trainingDbContext;
        }

        /// <summary>
        /// Vypočte seznam posledních 12 období (rok, měsíc, label) s aktuálním měsícem jako posledním.
        /// </summary>
        /// <returns>Seznam n-tic (Year, Month, Label) pro posledních 12 měsíců v chronologickém pořadí.</returns>
        private List<(int Year, int Month, string Label)> GetLast12Periods()
        {
            var utcNow = DateTime.UtcNow;
            return Enumerable.Range(0, 12)
                .Select(i =>
                {
                    var periodDate = utcNow.AddMonths(-i);
                    return (Year: periodDate.Year, Month: periodDate.Month, Label: $"{periodDate.Month}.{periodDate.Year}");
                })
                .Reverse()
                .ToList();
        }

        /// <summary>
        /// Získá součty plateb (kladné transakce) osoby za posledních 12 měsíců rozdělené po měsících.
        /// </summary>
        /// <param name="personId">Identifikátor osoby.</param>
        /// <returns> n-tice s polem popisků (Labels) a odpovídajících součtů (Values) pro každé z 12 období.</returns>
        public async Task<(string[] Labels, decimal[] Values)> GetPersonPaymentsPerLast12MonthsAsync(int personId)
        {
            var periods = GetLast12Periods();
            var periodStart = new DateTime(periods.First().Year, periods.First().Month, 1);

            var monthlySums = await trainingDbContext.PersonTransactions
                .Where(t => t.PersonId == personId && t.Amount > 0 && t.Date >= periodStart)
                .GroupBy(t => new { t.Date.Year, t.Date.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Sum = g.Sum(t => t.Amount) })
                .ToListAsync();

            var labels = periods.Select(p => p.Label).ToArray();
            var values = periods.Select(p => monthlySums.FirstOrDefault(s => s.Year == p.Year && s.Month == p.Month)?.Sum ?? 0m).ToArray();

            return (labels, values);
        }

        /// <summary>
        /// Získá pro danou osobu rozpis příjmů a výdajů za posledních 12 měsíců.
        /// </summary>
        /// <param name="personId">Identifikátor osoby.</param>
        /// <returns> n-tice s poli Labels, Income (kladné částky) a Expense (kladné částky výdajů) pro každé období.</returns>
        public async Task<(string[] Labels, decimal[] Income, decimal[] Expense)> GetIncomeVsExpenseForLast12MonthsAsync(int personId)
        {
            var periods = GetLast12Periods();
            var start = new DateTime(periods.First().Year, periods.First().Month, 1);

            var monthlyIncomeExpense = await trainingDbContext.PersonTransactions
                .Where(t => t.PersonId == personId && t.Date >= start)
                .GroupBy(t => new { t.Date.Year, t.Date.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    Income = g.Where(x => x.Amount > 0).Sum(x => (decimal?)x.Amount) ?? 0m,
                    Expense = g.Where(x => x.Amount < 0).Sum(x => (decimal?)x.Amount) ?? 0m
                })
                .ToListAsync();

            var labels = periods.Select(p => p.Label).ToArray();
            var income = periods.Select(p => monthlyIncomeExpense.FirstOrDefault(g => g.Year == p.Year && g.Month == p.Month)?.Income ?? 0m).ToArray();
            var expense = periods.Select(p => -(monthlyIncomeExpense.FirstOrDefault(g => g.Year == p.Year && g.Month == p.Month)?.Expense ?? 0m)).ToArray();

            return (labels, income, expense);
        }

        /// <summary>
        /// Vrátí rozdělení výdajů osoby podle kategorií (např. Rent, Equipment, Other) a jejich hodnoty.
        /// </summary>
        /// <param name="personId">Identifikátor osoby.</param>
        /// <returns> n-tice s poli Categories a Values odpovídajícími kategoriím a součtům výdajů.</returns>
        public async Task<(string[] Categories, decimal[] Values)> GetExpenseBreakdownAsync(int personId)
        {
            var expenseCategorizedQuery = trainingDbContext.PersonTransactions
                .Where(t => t.PersonId == personId && t.Amount < 0)
                .Select(t => new
                {
                    Category = t.Description != null && t.Description.StartsWith("Rent share") ? "Rent"
                             : t.Description != null && t.Description.StartsWith("Výdaj") ? "Equipment"
                             : "Other",
                    Amount = -t.Amount
                });

            var categorySums = await expenseCategorizedQuery
                .GroupBy(x => x.Category)
                .Select(g => new { Category = g.Key, Sum = g.Sum(x => x.Amount) })
                .ToListAsync();

            var categoryLabels = categorySums.Select(x => x.Category).ToArray();
            var categoryValues = categorySums.Select(x => x.Sum).ToArray();

            return (categoryLabels, categoryValues);
        }

        /// <summary>
        /// Vrátí počet účastí dané osoby na trénincích rozdělený po měsících za posledních 12 měsíců.
        /// </summary>
        /// <param name="personId">Identifikátor osoby.</param>
        /// <returns> n-tice s poli Labels a Values (počet účastí) pro každé období.</returns>
        public async Task<(string[] Labels, int[] Values)> GetParticipationLast12MonthsAsync(int personId)
        {
            var periods = GetLast12Periods();
            var periodStart = new DateTime(periods.First().Year, periods.First().Month, 1);

            var monthlyRegistrations = await trainingDbContext.Registrations
                .Where(r => r.PersonId == personId && r.Date >= periodStart)
                .GroupBy(r => new { r.Date.Year, r.Date.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Count = g.Count() })
                .ToListAsync();

            var labels = periods.Select(p => p.Label).ToArray();
            var values = periods.Select(p => monthlyRegistrations.FirstOrDefault(g => g.Year == p.Year && g.Month == p.Month)?.Count ?? 0).ToArray();

            return (labels, values);
        }
    }
}
