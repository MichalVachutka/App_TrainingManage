using Microsoft.AspNetCore.Mvc;
using TrainingManage.Api.Models.Expense;
using TrainingManage.Api.Interfaces;
using TrainingManage.Api.Models.ExpenseParticipant;

namespace TrainingManage.Api.Controllers
{

    /// <summary>
    /// API kontroler pro správu výdajů a účastníků výdajů.
    /// </summary>
    [ApiController]
    [Route("api/expenses")]
    public class ExpensesController : ControllerBase
    {

        /// <summary>
        /// Správce výdajů poskytující metody pro práci s výdaji a jejich účastníky.
        /// </summary>
        private readonly IExpenseManager expenseManager;

        /// <summary>
        /// Vytvoří instanci <see cref="ExpensesController"/>.
        /// </summary>
        /// <param name="expenseManager">Injektovaná implementace správce výdajů.</param>
        public ExpensesController(IExpenseManager expenseManager)
            => this.expenseManager = expenseManager;

        /// <summary>
        /// Vrátí seznam všech výdajů.
        /// </summary>
        /// <returns>Seznam <see cref="ExpenseDto"/> zabalený do <see cref="ActionResult"/>.</returns>
        [HttpGet]
        public ActionResult<IList<ExpenseDto>> GetAll()
            => Ok(expenseManager.GetAllExpenses());

        /// <summary>
        /// Vrátí jeden výdaj podle id.
        /// </summary>
        /// <param name="id">Identifikátor výdaje.</param>
        /// <returns><see cref="ExpenseDto"/> pokud existuje, jinak 404 NotFound.</returns>
        [HttpGet("{id}")]
        public ActionResult<ExpenseDto> Get(int id)
            => expenseManager.GetExpense(id) is ExpenseDto expenseDto
                ? Ok(expenseDto) : NotFound();

        /// <summary>
        /// Vrátí detailní informace o výdaji podle id.
        /// </summary>
        /// <param name="id">Identifikátor výdaje.</param>
        /// <returns><see cref="ExpenseDetailDto"/> pokud existuje, jinak 404 NotFound.</returns>
        [HttpGet("{id}/detail")]
        public ActionResult<ExpenseDetailDto> GetDetail(int id)
            => expenseManager.GetExpenseDetail(id) is ExpenseDetailDto expenseDetailDto 
                ? Ok(expenseDetailDto) : NotFound();

        /// <summary>
        /// Vytvoří nový výdaj.
        /// </summary>
        /// <param name="expenseCreateDto">Data pro vytvoření výdaje.</param>
        /// <returns>Vytvořený <see cref="ExpenseDto"/> s odpovědí 201 Created a hlavičkou Location.</returns>
        [HttpPost]
        public ActionResult<ExpenseDto> Create([FromBody] ExpenseCreateDto expenseCreateDto)
        {
            var createdExpenseDto = expenseManager.CreateExpense(expenseCreateDto);
            return CreatedAtAction(nameof(Get), new { id = createdExpenseDto.Id }, createdExpenseDto);
        }

        /// <summary>
        /// Aktualizuje existující výdaj.
        /// </summary>
        /// <param name="id">Identifikátor výdaje, který se má aktualizovat.</param>
        /// <param name="expenseCreateDto">Data pro aktualizaci výdaje.</param>
        /// <returns>Aktualizovaný <see cref="ExpenseDto"/> pokud aktualizace uspěje, jinak 404 NotFound.</returns>
        [HttpPut("{id}")]
        public ActionResult<ExpenseDto> Update(int id, [FromBody] ExpenseCreateDto expenseCreateDto)
            => expenseManager.UpdateExpense(id, expenseCreateDto) is ExpenseDto updatedExpenseDto
                ? Ok(updatedExpenseDto) : NotFound();

        /// <summary>
        /// Smaže výdaj.
        /// </summary>
        /// <param name="id">Identifikátor výdaje, který se má smazat.</param>
        /// <returns>204 NoContent při úspěšném smazání.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            expenseManager.DeleteExpense(id);
            return NoContent();
        }

        /// <summary>
        /// Přidá účastníka k existujícímu výdaji.
        /// </summary>
        /// <param name="id">Identifikátor výdaje, ke kterému se účastník přidává.</param>
        /// <param name="participantCreateDto">Data pro vytvoření účastníka.</param>
        /// <returns>Vytvořený <see cref="ExpenseParticipantDto"/> s odpovědí 201 Created a hlavičkou Location.</returns>
        [HttpPost("{id}/participants")]
        public ActionResult<ExpenseParticipantDto> AddParticipant(int id, [FromBody] ExpenseParticipantCreateDto participantCreateDto)
        {
            // metoda v IExpenseManager se jmenuje AddParticipant
            var createdParticipantDto = expenseManager.AddParticipant(id, participantCreateDto);
            return CreatedAtAction(nameof(AddParticipant), new { id }, createdParticipantDto);
        }

        /// <summary>
        /// Odstraní účastníka z výdaje podle ID osoby.
        /// </summary>
        /// <param name="id">Identifikátor výdaje.</param>
        /// <param name="personId">Identifikátor osoby, která se má odebrat.</param>
        /// <returns>204 NoContent při úspěšném odstranění.</returns>
        [HttpDelete("{id}/participants/{personId}")]
        public IActionResult RemoveParticipant(int id, int personId)
        {
            expenseManager.DeleteParticipant(id, personId);
            return NoContent();
        }
    }
}

