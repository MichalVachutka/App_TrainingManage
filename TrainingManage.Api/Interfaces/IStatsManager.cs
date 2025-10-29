using System.Threading.Tasks;
using TrainingManage.Api.Models.Stats;

namespace TrainingManage.Api.Managers
{
    public interface IStatsManager
    {
        Task<PersonStatsDto> GetPersonStatsAsync(int personId);
        Task<PersonIncomeExpenseDto> GetIncomeVsExpenseAsync(int personId);
        Task<ExpenseBreakdownDto> GetExpenseBreakdownAsync(int personId);
        Task<ParticipationDto> GetParticipationAsync(int personId);
    }
}
