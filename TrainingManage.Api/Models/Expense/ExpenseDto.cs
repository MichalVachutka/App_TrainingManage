using System.Text.Json.Serialization;

namespace TrainingManage.Api.Models.Expense
{
    /// <summary>
    /// Datový přenosový objekt představující výdaj včetně jeho podílů na osobách.
    /// </summary>
    public class ExpenseDto
    {
        /// <summary>
        /// Identifikátor výdaje.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Typ výdaje.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = "";

        /// <summary>
        /// Celková částka výdaje.
        /// </summary>
        [JsonPropertyName("totalAmount")]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Datum výdaje.
        /// </summary>
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Seznam podílů jednotlivých účastníků na výdaji.
        /// </summary>
        [JsonPropertyName("participantShares")]
        public IList<ParticipantShareDto> ParticipantShares { get; set; } = new List<ParticipantShareDto>();
    }
}
