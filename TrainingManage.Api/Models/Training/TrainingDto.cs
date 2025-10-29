using System.Text.Json.Serialization;

namespace TrainingManage.Api.Models.Training
{
    /// <summary>
    /// Datový přenosový objekt (DTO) pro entitu Training.
    /// </summary>
    public class TrainingDto
    {
        /// <summary>
        /// Identifikátor tréninku.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Název tréninku.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = "";

        /// <summary>
        /// Datum tréninku.
        /// </summary>
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Poznámky k tréninku (volitelné).
        /// </summary>
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        /// <summary>
        /// Cena za pronájem (rent cost).
        /// </summary>
        public decimal RentCost { get; set; }
    }
}
