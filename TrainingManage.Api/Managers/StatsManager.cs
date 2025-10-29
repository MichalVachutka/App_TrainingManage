using System.Threading.Tasks;
using TrainingManage.Api.Models.Stats;
using TrainingManage.Data.Interfaces;

namespace TrainingManage.Api.Managers
{
    public class StatsManager : IStatsManager
    {
        private readonly IStatsRepository statsRepository;

        public StatsManager(IStatsRepository statsRepository) => this.statsRepository = statsRepository;

        public async Task<PersonStatsDto> GetPersonStatsAsync(int personId)
        {
            var (labels, values) = await statsRepository.GetPersonPaymentsPerLast12MonthsAsync(personId);
            return new PersonStatsDto { Labels = labels, Values = values };
        }

        public async Task<PersonIncomeExpenseDto> GetIncomeVsExpenseAsync(int personId)
        {
            var (labels, income, expense) = await statsRepository.GetIncomeVsExpenseForLast12MonthsAsync(personId);
            return new PersonIncomeExpenseDto { Labels = labels, Income = income, Expense = expense };
        }

        public async Task<ExpenseBreakdownDto> GetExpenseBreakdownAsync(int personId)
        {
            var (categories, values) = await statsRepository.GetExpenseBreakdownAsync(personId);
            return new ExpenseBreakdownDto { Categories = categories, Values = values };
        }

        public async Task<ParticipationDto> GetParticipationAsync(int personId)
        {
            var (labels, values) = await statsRepository.GetParticipationLast12MonthsAsync(personId);
            return new ParticipationDto { Labels = labels, Values = values };
        }
    }
}
