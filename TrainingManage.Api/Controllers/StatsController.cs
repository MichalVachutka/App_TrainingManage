using Microsoft.AspNetCore.Mvc;
using TrainingManage.Api.Managers;
using TrainingManage.Api.Models.Stats;

namespace TrainingManage.Api.Controllers
{
    [ApiController]
    [Route("api/people/{personId:int}/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly IStatsManager statsManager;

        public StatsController(IStatsManager statsManager) => this.statsManager = statsManager;

        [HttpGet]
        public async Task<ActionResult<PersonStatsDto>> GetPersonStats(int personId)
            => Ok(await statsManager.GetPersonStatsAsync(personId));

        [HttpGet("income-vs-expense")]
        public async Task<ActionResult<PersonIncomeExpenseDto>> GetIncomeVsExpense(int personId)
            => Ok(await statsManager.GetIncomeVsExpenseAsync(personId));

        [HttpGet("expense-breakdown")]
        public async Task<ActionResult<ExpenseBreakdownDto>> GetExpenseBreakdown(int personId)
            => Ok(await statsManager.GetExpenseBreakdownAsync(personId));

        [HttpGet("participation")]
        public async Task<ActionResult<ParticipationDto>> GetParticipation(int personId)
            => Ok(await statsManager.GetParticipationAsync(personId));
    }
}
