using TrainingManage.Api.Models.Person;

namespace TrainingManage.Api.Models.filters
{
    /// <summary>
    /// Skupina transakcí seskupená podle období.
    /// </summary>
    public class TransactionGroupDto
    {
        /// <summary>
        /// Popisek období, například "červen 2025".
        /// </summary>
        public string PeriodLabel { get; set; }

        /// <summary>
        /// Seznam transakcí v dané skupině.
        /// </summary>
        public List<PersonTransactionDto> Transactions { get; set; }
    }
}
