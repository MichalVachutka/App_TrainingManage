namespace TrainingManage.Data.Interfaces
{
    
    public interface IStatsRepository
    {
        Task<(string[] Labels, decimal[] Values)> GetPersonPaymentsPerLast12MonthsAsync(int personId);

        Task<(string[] Labels, decimal[] Income, decimal[] Expense)> GetIncomeVsExpenseForLast12MonthsAsync(int personId);

        Task<(string[] Categories, decimal[] Values)> GetExpenseBreakdownAsync(int personId);

        Task<(string[] Labels, int[] Values)> GetParticipationLast12MonthsAsync(int personId);
    }
}
