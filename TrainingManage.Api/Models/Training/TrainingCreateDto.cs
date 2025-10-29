namespace TrainingManage.Api.Models.Training
{
    /// <summary>
    /// Datový přenosový objekt pro vytvoření nového tréninku.
    /// </summary>
    public class TrainingCreateDto
    {
        /// <summary>
        /// Název tréninku.
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        /// Datum konání tréninku.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Náklady na pronájem.
        /// </summary>
        public decimal RentCost { get; set; }
    }
}
