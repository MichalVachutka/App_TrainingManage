using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrainingManage.Data.Models
{
    /// <summary>
    /// Reprezentuje výdaj v systému, včetně typu, částky, data a zapojených osob.
    /// </summary>
    public class Expense
    {
        /// <summary>
        /// Jedinečný identifikátor výdaje.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Typ výdaje (např. ubytování, doprava apod.).
        /// </summary>
        [Required, StringLength(50)]
        public string Type { get; set; } = "";

        /// <summary>
        /// Celková částka výdaje.
        /// </summary>
        [Required]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Datum uskutečnění výdaje.
        /// </summary>
        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Seznam osob, které se na výdaji podílejí.
        /// </summary>
        public virtual ICollection<ExpenseParticipant> Participants { get; set; } = new List<ExpenseParticipant>();
    }
}
