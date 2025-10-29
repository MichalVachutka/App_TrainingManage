using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingManage.Data.Models
{
    /// <summary>
    /// Reprezentuje trénink včetně základních informací a souvisejících registrací.
    /// </summary>
    public class Training
    {
        /// <summary>
        /// Jedinečný identifikátor tréninku.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Název tréninku.
        /// </summary>
        [Required, StringLength(100)]
        public string Title { get; set; } = "";

        /// <summary>
        /// Datum a čas konání tréninku.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Nepovinné poznámky k tréninku.
        /// </summary>
        [StringLength(250)]
        public string? Notes { get; set; }

        /// <summary>
        /// Náklady na pronájem pro tento trénink.
        /// </summary>
        public decimal RentCost { get; set; }

        /// <summary>
        /// Seznam registrací spojených s tímto tréninkem.
        /// </summary>
        public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
