using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingManage.Data.Models
{
    /// <summary>
    /// Reprezentuje účast osoby na konkrétním výdaji, včetně jejího podílu na částce.
    /// </summary>
    public class ExpenseParticipant
    {
        /// <summary>
        /// Identifikátor výdaje.
        /// </summary>
        [Key, Column(Order = 0)]
        public int ExpenseId { get; set; }

        /// <summary>
        /// Navigační vlastnost na výdaj.
        /// </summary>
        public virtual Expense Expense { get; set; } = null!;

        /// <summary>
        /// Identifikátor osoby účastnící se na výdaji.
        /// </summary>
        [Key, Column(Order = 1)]
        public int PersonId { get; set; }

        /// <summary>
        /// Navigační vlastnost na osobu.
        /// </summary>
        public virtual Person Person { get; set; } = null!;

        /// <summary>
        /// Výše finančního podílu osoby na výdaji.
        /// </summary>
        public decimal ShareAmount { get; set; }
    }
}
