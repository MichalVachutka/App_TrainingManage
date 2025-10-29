using System.Text.Json.Serialization;
using TrainingManage.Api.Models.ExpenseParticipant;

namespace TrainingManage.Api.Models.Expense
{
    /// <summary>
    /// Detailní datový přenosový objekt výdaje včetně podílů účastníků a souhrnu podílů.
    /// </summary>
    public class ExpenseDetailDto
    {
        /// <summary>
        /// Základní informace o výdaji.
        /// </summary>
        [JsonPropertyName("expense")]
        public ExpenseDto Expense { get; set; } = null!;

        /// <summary>
        /// Podíly jednotlivých účastníků na výdaji.
        /// </summary>
        [JsonPropertyName("participantShares")]
        public IList<ExpenseParticipantDto> ParticipantShares { get; set; } = new List<ExpenseParticipantDto>();

        /// <summary>
        /// Celkový součet podílů všech účastníků.
        /// </summary>
        [JsonPropertyName("totalShares")]
        public decimal TotalShares { get; set; }
    }
}
