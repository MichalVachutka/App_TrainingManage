using TrainingManage.Api.Models.Expense;
using TrainingManage.Api.Models.ExpenseParticipant;

namespace TrainingManage.Api.Interfaces
{
    /// <summary>
    /// Rozhraní pro správu výdajů a jejich účastníků.
    /// </summary>
    public interface IExpenseManager
    {
        /// <summary>
        /// Vrátí seznam všech výdajů.
        /// </summary>
        IList<ExpenseDto> GetAllExpenses();

        /// <summary>
        /// Vrátí detail výdaje podle jeho ID.
        /// </summary>
        ExpenseDto? GetExpense(int id);

        /// <summary>
        /// Vytvoří nový výdaj.
        /// </summary>
        ExpenseDto CreateExpense(ExpenseCreateDto dto);

        /// <summary>
        /// Aktualizuje existující výdaj podle ID.
        /// </summary>
        ExpenseDto? UpdateExpense(int id, ExpenseCreateDto dto);

        /// <summary>
        /// Odstraní výdaj podle ID.
        /// </summary>
        void DeleteExpense(int id);

        /// <summary>
        /// Vrátí detailní informace o výdaji včetně účastníků.
        /// </summary>
        ExpenseDetailDto? GetExpenseDetail(int id);

        /// <summary>
        /// Přidá účastníka k výdaji.
        /// </summary>
        ExpenseParticipantDto CreateParticipant(ExpenseParticipantCreateDto dto);

        /// <summary>
        /// Odstraní účastníka z výdaje podle ID výdaje a osoby.
        /// </summary>
        void DeleteParticipant(int expenseId, int personId);

        /// <summary>
        /// Přidá účastníka k výdaji podle ID výdaje.
        /// </summary>
        ExpenseParticipantDto AddParticipant(int expenseId, ExpenseParticipantCreateDto dto);
    }
}
