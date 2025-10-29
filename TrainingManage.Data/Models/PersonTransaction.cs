namespace TrainingManage.Data.Models
{
    /// <summary>
    /// Reprezentuje finanční transakci spojenou s konkrétní osobou, včetně částky, data a popisu.
    /// </summary>
    public class PersonTransaction
    {
        /// <summary>
        /// Jedinečný identifikátor transakce.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID osoby, které se transakce týká.
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// Navigační vlastnost na osobu.
        /// </summary>
        public virtual Person Person { get; set; } = null!;

        /// <summary>
        /// Datum provedení transakce.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Částka transakce (kladná i záporná).
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Popis transakce.
        /// </summary>
        public string Description { get; set; } = null!;

        /// <summary>
        /// ID tréninku, se kterým je transakce spojena (volitelné).
        /// </summary>
        public int? TrainingId { get; set; }

        /// <summary>
        /// Navigační vlastnost na trénink (volitelná vazba).
        /// </summary>
        public virtual Training? Training { get; set; }

        /// <summary>
        /// ID výdaje, se kterým je transakce spojena (volitelné).
        /// </summary>
        public int? ExpenseId { get; set; }

        /// <summary>
        /// Navigační vlastnost na výdaj (volitelná vazba).
        /// </summary>
        public virtual Expense? Expense { get; set; }
    }
}
