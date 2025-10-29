using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TrainingManage.Api.Models.ExpenseParticipant
{
    /// <summary>
    /// Datový přenosový objekt pro vytvoření podílu osoby na výdaji.
    /// </summary>
    public class ExpenseParticipantCreateDto
    {
        /// <summary>
        /// Identifikátor výdaje.
        /// </summary>
        [Required]
        [JsonPropertyName("expenseId")]
        public int ExpenseId { get; set; }

        /// <summary>
        /// Identifikátor osoby.
        /// </summary>
        [Required]
        [JsonPropertyName("personId")]
        public int PersonId { get; set; }

        /// <summary>
        /// Výše podílu osoby na výdaji.
        /// </summary>
        [Required]
        [JsonPropertyName("shareAmount")]
        public decimal ShareAmount { get; set; }
    }
}
