namespace TrainingManage.Api.Models.Stats
{
    public class ExpenseBreakdownDto
    {
        public string[] Categories { get; set; } = Array.Empty<string>();
        public decimal[] Values { get; set; } = Array.Empty<decimal>();
    }
}
