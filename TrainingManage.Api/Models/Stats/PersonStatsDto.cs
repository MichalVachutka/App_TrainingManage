namespace TrainingManage.Api.Models.Stats
{
    public class PersonStatsDto
    {
        // Popisky osy X (např. "7.2025", "6.2025", …)
        public string[] Labels { get; set; } = Array.Empty<string>();

        // Hodnoty osy Y (součet plateb v daném měsíci)
        public decimal[] Values { get; set; } = Array.Empty<decimal>();
    }
}
