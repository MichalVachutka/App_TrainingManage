namespace TrainingManage.Api.Models.Registration
{
    /// <summary>
    /// Datový přenosový objekt pro registraci, obsahuje informace o osobě i tréninku.
    /// </summary>
    public class RegistrationDto
    {
        /// <summary>
        /// Identifikátor registrace.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identifikátor osoby.
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// Jméno osoby.
        /// </summary>
        public string PersonName { get; set; }

        /// <summary>
        /// Název tréninku.
        /// </summary>
        public string TrainingTitle { get; set; }

        /// <summary>
        /// Datum tréninku.
        /// </summary>
        public DateTime TrainingDate { get; set; }

        /// <summary>
        /// Zaplacená částka.
        /// </summary>
        public decimal Payment { get; set; }

        /// <summary>
        /// Volitelná poznámka k registraci.
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Identifikátor tréninku.
        /// </summary>
        public int TrainingId { get; set; }
    }
}
