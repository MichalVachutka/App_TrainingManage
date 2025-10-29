using TrainingManage.Api.Models.Registration;
using TrainingManage.Api.Models.filters;

namespace TrainingManage.Api.Models.Person
{
    /// <summary>
    /// Datový přenosový objekt obsahující detailní informace o osobě včetně jejích registrací, plateb a transakcí.
    /// </summary>
    public class PersonDetailDto
    {
        /// <summary>
        /// Informace o osobě.
        /// </summary>
        public PersonDto Person { get; set; } = null!;

        /// <summary>
        /// Seznam registrací osoby na tréninky.
        /// </summary>
        public IList<RegistrationDto> Registrations { get; set; } = new List<RegistrationDto>();

        /// <summary>
        /// Celková částka zaplacená osobou.
        /// </summary>
        public decimal TotalPaid { get; set; }

        /// <summary>
        /// Částka zaplacená osobou v tomto měsíci.
        /// </summary>
        public decimal PaidThisMonth { get; set; }

        /// <summary>
        /// Částka zaplacená osobou v tomto roce.
        /// </summary>
        public decimal PaidThisYear { get; set; }

        /// <summary>
        /// Celkový podíl osoby na nákladech za pronájem.
        /// </summary>
        public decimal TotalRentShare { get; set; }

        /// <summary>
        /// Celkový podíl osoby na nákladech za vybavení.
        /// </summary>
        public decimal TotalEquipmentShare { get; set; }

        /// <summary>
        /// Celkový podíl osoby na ostatních nákladech.
        /// </summary>
        public decimal TotalExpenseShare { get; set; }

        /// <summary>
        /// Zůstatek (saldo) osoby.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Seznam transakcí osoby.
        /// </summary>
        public IList<PersonTransactionDto> PersonTransactions { get; set; } = new List<PersonTransactionDto>();

        /// <summary>
        /// Skupiny transakcí podle nějakého filtru či kategorie.
        /// </summary>
        public List<TransactionGroupDto> TransactionGroups { get; set; }
    }
}
