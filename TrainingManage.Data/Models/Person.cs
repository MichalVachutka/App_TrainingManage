using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingManage.Data.Models
{
    /// <summary>
    /// Reprezentuje osobu zúčastněnou na trénincích, včetně osobních údajů a souvisejících záznamů.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Jedinečný identifikátor osoby.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }

        /// <summary>
        /// Identifikační číslo osoby (např. členské nebo interní).
        /// </summary>
        [Required]
        public int IdentificationNumber { get; set; }

        /// <summary>
        /// Jméno osoby.
        /// </summary>
        [Required, StringLength(30)]
        public string Name { get; set; } = "";

        /// <summary>
        /// Věk osoby.
        /// </summary>
        [Required]
        public int Age { get; set; }

        /// <summary>
        /// Telefonní číslo osoby.
        /// </summary>
        [Required, StringLength(30)]
        public string Telephone { get; set; } = "";

        /// <summary>
        /// E-mailová adresa osoby.
        /// </summary>
        [Required, StringLength(50), EmailAddress]
        public string Email { get; set; } = "";

        /// <summary>
        /// Určuje, zda je osoba skryta v seznamech (např. archivovaná).
        /// </summary>
        public bool Hidden { get; set; } = false;

        /// <summary>
        /// Registrace osoby na trénincích.
        /// </summary>
        public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();

        /// <summary>
        /// Účast osoby na nákladech spojených s tréninky nebo jinými aktivitami.
        /// </summary>
        public virtual ICollection<ExpenseParticipant> ExpenseParticipants { get; set; } = new List<ExpenseParticipant>();

        /// <summary>
        /// Finanční transakce spojené s osobou.
        /// </summary>
        public virtual ICollection<PersonTransaction> PersonTransactions { get; set; } = new List<PersonTransaction>();
    }
}
