using TrainingManage.Api.Models.Registration;

namespace TrainingManage.Api.Models.Training
{
    /// <summary>
    /// Detailní datový přenosový objekt tréninku, obsahuje informace o registracích a finanční přehled.
    /// </summary>
    public class TrainingDetailDto
    {
        /// <summary>
        /// Data tréninku.
        /// </summary>
        public TrainingDto Training { get; set; } = null!;

        /// <summary>
        /// Seznam registrací na trénink.
        /// </summary>
        public IList<RegistrationDto> Registrations { get; set; } = new List<RegistrationDto>();

        /// <summary>
        /// Počet účastníků tréninku.
        /// </summary>
        public int ParticipantCount { get; set; }

        /// <summary>
        /// Celková vybraná částka od účastníků.
        /// </summary>
        public decimal TotalCollected { get; set; }

        /// <summary>
        /// Celková částka za pronájem (fixní hodnota 450).
        /// </summary>
        public decimal RentTotal { get; set; } = 450m;

        /// <summary>
        /// Náklady na pronájem podle dat tréninku.
        /// </summary>
        public decimal RentCost { get; set; }

        /// <summary>
        /// Výpočet podílu na pronájmu na jednu osobu.
        /// </summary>
        public decimal RentSharePerPerson
            => ParticipantCount > 0
               ? Math.Round(RentTotal / ParticipantCount, 2)
               : 0m;

        /// <summary>
        /// Zůstatek - rozdíl mezi vybranými penězi a celkovým pronájmem.
        /// </summary>
        public decimal Balance => TotalCollected - RentTotal;
    }
}
