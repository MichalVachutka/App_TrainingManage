using System.Text.Json.Serialization;

namespace TrainingManage.Api.Models.Expense
{
    /// <summary>
    /// Datový přenosový objekt představující podíl osoby na výdaji.
    /// </summary>
    public class ParticipantShareDto
    {
        /// <summary>
        /// Identifikátor osoby.
        /// </summary>
        [JsonPropertyName("personId")]
        public int PersonId { get; set; }

        /// <summary>
        /// Jméno osoby.
        /// </summary>
        [JsonPropertyName("personName")]
        public string PersonName { get; set; } = "";

        /// <summary>
        /// Výše podílu osoby na výdaji.
        /// </summary>
        [JsonPropertyName("shareAmount")]
        public decimal ShareAmount { get; set; }
    }
}
