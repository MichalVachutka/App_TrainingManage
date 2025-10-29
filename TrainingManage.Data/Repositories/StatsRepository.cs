using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainingManage.Data.Interfaces;
using TrainingManage.Data; // TrainingDbContext

namespace TrainingManage.Data.Repositories
{
    /// <summary>
    /// Implementace repozitáře pro statistiky — vrací ValueTupley, žádné DTO.
    /// </summary>
    public class StatsRepository : IStatsRepository
    {
        private readonly TrainingDbContext db;

        public StatsRepository(TrainingDbContext db) => this.db = db;

        private List<(int Year, int Month, string Label)> GetLast12Periods()
        {
            var now = DateTime.UtcNow;
            return Enumerable.Range(0, 12)
                .Select(i =>
                {
                    var d = now.AddMonths(-i);
                    return (Year: d.Year, Month: d.Month, Label: $"{d.Month}.{d.Year}");
                })
                .Reverse()
                .ToList();
        }

        public async Task<(string[] Labels, decimal[] Values)> GetPersonPaymentsPerLast12MonthsAsync(int personId)
        {
            var periods = GetLast12Periods();
            var start = new DateTime(periods.First().Year, periods.First().Month, 1);

            var sums = await db.PersonTransactions
                .Where(t => t.PersonId == personId && t.Amount > 0 && t.Date >= start)
                .GroupBy(t => new { t.Date.Year, t.Date.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Sum = g.Sum(t => t.Amount) })
                .ToListAsync();

            var labels = periods.Select(p => p.Label).ToArray();
            var values = periods.Select(p => sums.FirstOrDefault(s => s.Year == p.Year && s.Month == p.Month)?.Sum ?? 0m).ToArray();

            return (labels, values);
        }

        public async Task<(string[] Labels, decimal[] Income, decimal[] Expense)> GetIncomeVsExpenseForLast12MonthsAsync(int personId)
        {
            var periods = GetLast12Periods();
            var start = new DateTime(periods.First().Year, periods.First().Month, 1);

            var grouped = await db.PersonTransactions
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
            var income = periods.Select(p => grouped.FirstOrDefault(g => g.Year == p.Year && g.Month == p.Month)?.Income ?? 0m).ToArray();
            var expense = periods.Select(p => -(grouped.FirstOrDefault(g => g.Year == p.Year && g.Month == p.Month)?.Expense ?? 0m)).ToArray();

            return (labels, income, expense);
        }

        public async Task<(string[] Categories, decimal[] Values)> GetExpenseBreakdownAsync(int personId)
        {
            var categorizedQuery = db.PersonTransactions
                .Where(t => t.PersonId == personId && t.Amount < 0)
                .Select(t => new
                {
                    Category = t.Description != null && t.Description.StartsWith("Rent share") ? "Rent"
                             : t.Description != null && t.Description.StartsWith("Výdaj") ? "Equipment"
                             : "Other",
                    Amount = -t.Amount
                });

            var groups = await categorizedQuery
                .GroupBy(x => x.Category)
                .Select(g => new { Category = g.Key, Sum = g.Sum(x => x.Amount) })
                .ToListAsync();

            var categories = groups.Select(x => x.Category).ToArray();
            var values = groups.Select(x => x.Sum).ToArray();

            return (categories, values);
        }

        public async Task<(string[] Labels, int[] Values)> GetParticipationLast12MonthsAsync(int personId)
        {
            var periods = GetLast12Periods();
            var start = new DateTime(periods.First().Year, periods.First().Month, 1);

            var grouped = await db.Registrations
                .Where(r => r.PersonId == personId && r.Date >= start)
                .GroupBy(r => new { r.Date.Year, r.Date.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Count = g.Count() })
                .ToListAsync();

            var labels = periods.Select(p => p.Label).ToArray();
            var values = periods.Select(p => grouped.FirstOrDefault(g => g.Year == p.Year && g.Month == p.Month)?.Count ?? 0).ToArray();

            return (labels, values);
        }
    }
}
