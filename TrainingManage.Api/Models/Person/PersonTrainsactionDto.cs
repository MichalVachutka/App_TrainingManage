namespace TrainingManage.Api.Models.Person
{
    /// <summary>
    /// Datový přenosový objekt pro transakci osoby.
    /// </summary>
    public class PersonTransactionDto
    {
        /// <summary>
        /// Identifikátor transakce.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Datum transakce.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Částka transakce.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Popis transakce.
        /// </summary>
        public string Description { get; set; } = null!;

        /// <summary>
        /// Nepovinné ID tréninku, ke kterému transakce patří.
        /// </summary>
        public int? TrainingId { get; set; }

        /// <summary>
        /// Nepovinné ID výdaje, ke kterému transakce patří.
        /// </summary>
        public int? ExpenseId { get; set; }
    }
}
