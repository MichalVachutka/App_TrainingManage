using System.Text.Json.Serialization;

namespace TrainingManage.Api.Models.ExpenseParticipant
{
    /// <summary>
    /// Datový přenosový objekt představující podíl osoby na výdaji.
    /// </summary>
    public class ExpenseParticipantDto
    {
        /// <summary>
        /// Identifikátor výdaje.
        /// </summary>
        [JsonPropertyName("expenseId")]
        public int ExpenseId { get; set; }

        /// <summary>
        /// Identifikátor osoby.
        /// </summary>
        [JsonPropertyName("personId")]
        public int PersonId { get; set; }

        /// <summary>
        /// Jméno osoby.
        /// </summary>
        [JsonPropertyName("personName")]
        public string PersonName { get; set; } = string.Empty;

        /// <summary>
        /// Výše podílu osoby na výdaji.
        /// </summary>
        [JsonPropertyName("shareAmount")]
        public decimal ShareAmount { get; set; }
    }
}
