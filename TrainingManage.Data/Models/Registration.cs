using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingManage.Data.Models
{
    /// <summary>
    /// Reprezentuje registraci osoby na konkrétní trénink, včetně platby a poznámky.
    /// </summary>
    public class Registration
    {
        /// <summary>
        /// Jedinečný identifikátor registrace.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// ID osoby, která se registrovala.
        /// </summary>
        [Required] public int PersonId { get; set; }

        /// <summary>
        /// Navigační vlastnost na osobu.
        /// </summary>
        public virtual Person Person { get; set; } = null!;

        /// <summary>
        /// ID tréninku, na který se osoba registrovala.
        /// </summary>
        [Required] public int TrainingId { get; set; }

        /// <summary>
        /// Navigační vlastnost na trénink.
        /// </summary>
        public virtual Training Training { get; set; } = null!;

        /// <summary>
        /// Uhrazená částka za registraci.
        /// </summary>
        [Required] public decimal Payment { get; set; }

        /// <summary>
        /// Nepovinná poznámka k registraci.
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Datum konání tréninku (uloženo pro rychlý přístup).
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Datum a čas vytvoření registrace.
        /// </summary>
        [Required] public DateTime RegisteredOn { get; set; } = DateTime.UtcNow;
    }
}
