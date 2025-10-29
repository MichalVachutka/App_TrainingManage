namespace TrainingManage.Api.Models.Stats
{
    public class PersonIncomeExpenseDto
    {
        public string[] Labels { get; set; } = Array.Empty<string>();
        public decimal[] Income { get; set; } = Array.Empty<decimal>();
        public decimal[] Expense { get; set; } = Array.Empty<decimal>();
    }
}
