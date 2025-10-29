using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TrainingManage.Api.Models.Expense
{
    /// <summary>
    /// Datový přenosový objekt pro vytvoření nového výdaje.
    /// </summary>
    public class ExpenseCreateDto
    {
        /// <summary>
        /// Typ výdaje.
        /// </summary>
        [Required]
        [JsonPropertyName("type")]
        public string Type { get; set; } = "";

        /// <summary>
        /// Celková částka výdaje.
        /// </summary>
        [Required]
        [JsonPropertyName("totalAmount")]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Seznam identifikátorů osob, které se podílejí na výdaji.
        /// </summary>
        [Required]
        [JsonPropertyName("participantIds")]
        public List<int> ParticipantIds { get; set; } = new List<int>();
    }
}
