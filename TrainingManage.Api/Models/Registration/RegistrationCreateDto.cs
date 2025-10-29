using System.ComponentModel.DataAnnotations;

namespace TrainingManage.Api.Models.Registration
{
    /// <summary>
    /// Datový přenosový objekt pro vytvoření nové registrace na trénink.
    /// </summary>
    public class RegistrationCreateDto
    {
        /// <summary>
        /// ID osoby, která se registruje.
        /// </summary>
        [Required]
        public int PersonId { get; set; }

        /// <summary>
        /// ID tréninku, na který se osoba registruje.
        /// </summary>
        [Required]
        public int TrainingId { get; set; }

        /// <summary>
        /// Částka zaplacená za registraci.
        /// </summary>
        [Required]
        public decimal Payment { get; set; }

        /// <summary>
        /// Volitelná poznámka k registraci.
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Datum registrace.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
