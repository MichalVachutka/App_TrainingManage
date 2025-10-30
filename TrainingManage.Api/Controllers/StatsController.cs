using Microsoft.AspNetCore.Mvc;
using TrainingManage.Api.Managers;
using TrainingManage.Api.Models.Stats;

namespace TrainingManage.Api.Controllers
{
    /// <summary>
    /// API kontroler pro získávání statistických přehledů pro konkrétní osobu.
    /// Cesta: api/people/{personId}/stats
    /// </summary>
    [ApiController]
    [Route("api/people/{personId:int}/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly IStatsManager statsManager;

        /// <summary>
        /// Vytvoří instanci StatsController s injektovaným správcem statistik.
        /// </summary>
        /// <param name="statsManager">Implementace IStatsManager poskytující logiku pro statistiky.</param>
        public StatsController(IStatsManager statsManager) => this.statsManager = statsManager;

        /// <summary>
        /// Získá měsíční součty plateb osoby za posledních 12 měsíců.
        /// </summary>
        /// <param name="personId">Identifikátor osoby, pro kterou se statistiky získávají.</param>
        /// <returns>PersonStatsDto obsahující pole popisků měsíců a odpovídajících součtů plateb.</returns>
        [HttpGet]
        public async Task<ActionResult<PersonStatsDto>> GetPersonStats(int personId)
            => Ok(await statsManager.GetPersonStatsAsync(personId));


        /// <summary>
        /// Získá porovnání příjmů vs výdajů osoby rozdělené po měsících za posledních 12 měsíců.
        /// </summary>
        /// <param name="personId">Identifikátor osoby, pro kterou se přehled generuje.</param>
        /// <returns>PersonIncomeExpenseDto obsahující popisky měsíců, pole příjmů a pole výdajů.</returns>
        [HttpGet("income-vs-expense")]
        public async Task<ActionResult<PersonIncomeExpenseDto>> GetIncomeVsExpense(int personId)
            => Ok(await statsManager.GetIncomeVsExpenseAsync(personId));

        /// <summary>
        /// Získá rozklad výdajů osoby podle kategorií za příslušné období.
        /// </summary>
        /// <param name="personId">Identifikátor osoby, pro kterou se rozklad získává.</param>
        /// <returns>ExpenseBreakdownDto obsahující kategorie a odpovídající součty výdajů.</returns>
        [HttpGet("expense-breakdown")]
        public async Task<ActionResult<ExpenseBreakdownDto>> GetExpenseBreakdown(int personId)
            => Ok(await statsManager.GetExpenseBreakdownAsync(personId));

        /// <summary>
        /// Poskytne měsíční přehled počtu účastí osoby na trénincích za posledních 12 měsíců.
        /// </summary>
        /// <param name="personId">Identifikátor osoby, pro kterou se účasti získávají.</param>
        /// <returns>ParticipationDto obsahující popisky měsíců a počty účastí v jednotlivých měsících.</returns>
        [HttpGet("participation")]
        public async Task<ActionResult<ParticipationDto>> GetParticipation(int personId)
            => Ok(await statsManager.GetParticipationAsync(personId));
    }
}
