using TrainingManage.Api.Models.Registration;
using TrainingManage.Api.Models.Person;

namespace TrainingManage.Api.Interfaces
{
    /// <summary>
    /// Rozhraní pro správu registrací.
    /// </summary>
    public interface IRegistrationManager
    {
        /// <summary>
        /// Vrátí seznam všech registrací.
        /// </summary>
        IList<RegistrationDto> GetAllRegistrations();

        /// <summary>
        /// Vrátí registraci podle ID.
        /// </summary>
        RegistrationDto? GetRegistration(int id);

        /// <summary>
        /// Vytvoří novou registraci.
        /// </summary>
        RegistrationDto CreateRegistration(RegistrationCreateDto dto);

        /// <summary>
        /// Aktualizuje existující registraci podle ID.
        /// </summary>
        RegistrationDto? UpdateRegistration(int id, RegistrationCreateDto dto);

        /// <summary>
        /// Odstraní registraci podle ID.
        /// </summary>
        void DeleteRegistration(int registrationId);

        /// <summary>
        /// Vrátí detail osoby podle jejího ID (vztahující se k registraci).
        /// </summary>
        PersonDetailDto? GetPersonDetail(int personId);
    }
}
