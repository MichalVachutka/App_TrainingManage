using Microsoft.AspNetCore.Mvc;
using TrainingManage.Api.Interfaces;
using TrainingManage.Api.Models.Person;

namespace TrainingManage.Api.Controllers
{

    /// <summary>
    /// API kontroler pro správu osob a jejich detailů.
    /// </summary>
    [Route("api/people")]
    [ApiController]
    public class PeopleController : ControllerBase
    {

        /// <summary>
        /// Správce osob poskytující operace pro CRUD a získávání detailů.
        /// </summary>
        private readonly IPersonManager personManager;

        /// <summary>
        /// Inicializuje instanci <see cref="PeopleController"/> s implementací správce osob.
        /// </summary>
        /// <param name="personManager">Injektovaná implementace <see cref="IPersonManager"/>.</param>
        public PeopleController(IPersonManager personManager)
        {
            this.personManager = personManager;
        }

        /// <summary>
        /// Vrátí seznam všech osob.
        /// </summary>
        /// <returns>Seznam <see cref="PersonDto"/>.</returns>
        [HttpGet]
        public IEnumerable<PersonDto> GetPeople()
            => personManager.GetAllPeople();

        /// <summary>
        /// Vrátí jednu osobu podle id.
        /// </summary>
        /// <param name="id">Identifikátor osoby.</param>
        /// <returns><see cref="PersonDto"/> pokud existuje, jinak 404 NotFound.</returns>
        [HttpGet("{id}")]
        public IActionResult GetPeople(int id)
        {
            var person = personManager.GetPeople(id);
            return person is null ? NotFound() : Ok(person);
        }

        /// <summary>
        /// Přidá novou osobu.
        /// </summary>
        /// <param name="personDto">Data osoby k vytvoření.</param>
        /// <returns>Vytvořená <see cref="PersonDto"/> s odpovědí 201 Created a hlavičkou Location.</returns>
        [HttpPost]
        public IActionResult AddPeople([FromBody] PersonDto person)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = personManager.AddPeople(person);
            return CreatedAtAction(nameof(GetPeople), new { id = created.Id }, created);
        }

        /// <summary>
        /// Smaže existující osobu.
        /// </summary>
        /// <param name="id">Identifikátor osoby, která se má smazat.</param>
        /// <returns>204 NoContent při úspěšném smazání.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeletePeople(int id)
        {
            personManager.DeletePeople(id);
            return NoContent();
        }

        /// <summary>
        /// Aktualizuje existující osobu.
        /// </summary>
        /// <param name="id">Identifikátor osoby, která se má aktualizovat.</param>
        /// <param name="personDto">Data pro aktualizaci osoby.</param>
        /// <returns>Aktualizovaná <see cref="PersonDto"/> pokud aktualizace uspěje, jinak 404 NotFound.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdatePeople(int id, [FromBody] PersonDto person)
        {
            var updated = personManager.UpdatePeople(id, person);
            return updated is null ? NotFound() : Ok(updated);
        }

        /// <summary>
        /// Vrátí detailní informace o osobě podle id.
        /// </summary>
        /// <param name="id">Identifikátor osoby.</param>
        /// <returns><see cref="PersonDetailDto"/> pokud existuje, jinak 404 NotFound.</returns>
        [HttpGet("{id}/detail")]
        public ActionResult<PersonDetailDto> GetDetail(int id)
        => personManager.GetPersonDetail(id) is PersonDetailDto personDetailDto
                ? Ok(personDetailDto)
                : NotFound();
    }
}



