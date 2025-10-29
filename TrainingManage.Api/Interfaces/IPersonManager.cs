using TrainingManage.Api.Models.Person;

namespace TrainingManage.Api.Interfaces
{
    /// <summary>
    /// Rozhraní pro správu osob.
    /// </summary>
    public interface IPersonManager
    {
        /// <summary>
        /// Vrátí seznam všech osob.
        /// </summary>
        IList<PersonDto> GetAllPeople();

        /// <summary>
        /// Vrátí osobu podle jejího ID.
        /// </summary>
        PersonDto? GetPeople(int Id);

        /// <summary>
        /// Vrátí stránkovaný seznam osob.
        /// </summary>
        IList<PersonDto> GetAllPeople(int page = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Přidá novou osobu.
        /// </summary>
        PersonDto AddPeople(PersonDto personDto);

        /// <summary>
        /// Aktualizuje údaje osoby podle ID.
        /// </summary>
        PersonDto? UpdatePeople(int id, PersonDto personDto);

        /// <summary>
        /// Odstraní osobu podle ID.
        /// </summary>
        void DeletePeople(int id);

        /// <summary>
        /// Vrátí detail osoby včetně souvisejících informací.
        /// </summary>
        PersonDetailDto? GetPersonDetail(int id);
    }
}
