using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TrainingManage.Data.Interfaces;
using TrainingManage.Api.Models.Registration;
using TrainingManage.Api.Interfaces;

namespace TrainingManage.Api.Controllers
{

    /// <summary>
    /// API kontroler pro správu registrací na tréninky.
    /// </summary>
    [Route("api/registrations")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {

        /// <summary>
        /// Správce registrací poskytující operace pro vytváření a mazání registrací.
        /// </summary>
        private readonly IRegistrationManager registrationManager;

        /// <summary>
        /// Repozitář pro přístup k uloženým registracím a jejich dotazování.
        /// </summary>
        private readonly IRegistrationRepository registrationRepository;

        /// <summary>
        /// Mapper (AutoMapper) pro převod entit na DTO a zpět.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Inicializuje instanci <see cref="RegistrationsController"/> s injektovanými závislostmi.
        /// </summary>
        /// <param name="registrationManager">Injektovaná implementace <see cref="IRegistrationManager"/>.</param>
        /// <param name="registrationRepository">Injektovaná implementace <see cref="IRegistrationRepository"/>.</param>
        /// <param name="mapper">Injektovaný AutoMapper (<see cref="IMapper"/>).</param>
        public RegistrationsController(
            IRegistrationManager registrationManager,
            IRegistrationRepository registrationRepository,
            IMapper mapper)
        {
            this.registrationManager = registrationManager;
            this.registrationRepository = registrationRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vrátí seznam registrací pro zadanou osobu.
        /// </summary>
        /// <param name="personId">Identifikátor osoby, pro kterou se mají registrace vrátit.</param>
        /// <returns>Seznam <see cref="RegistrationDto"/>.</returns>
        [HttpGet("person/{personId}")]
        public ActionResult<IList<RegistrationDto>> GetByPerson(int personId)
        {
            var registrations = registrationRepository.GetByPerson(personId);
            var registrationDtos = mapper.Map<IList<RegistrationDto>>(registrations);
            return Ok(registrationDtos);
        }

        /// <summary>
        /// Vrátí seznam registrací pro zadaný trénink.
        /// </summary>
        /// <param name="trainingId">Identifikátor tréninku.</param>
        /// <returns>Seznam <see cref="RegistrationDto"/>.</returns>
        [HttpGet("training/{trainingId}")]
        public ActionResult<IList<RegistrationDto>> GetByTraining(int trainingId)
        {
            var registrations = registrationRepository.GetByTraining(trainingId);
            var registrationDtos = mapper.Map<IList<RegistrationDto>>(registrations);
            return Ok(registrationDtos);
        }

        /// <summary>
        /// Vytvoří novou registraci (účast na tréninku).
        /// </summary>
        /// <param name="registrationCreateDto">Data pro vytvoření registrace.</param>
        /// <returns>Vytvořená <see cref="RegistrationDto"/> s odpovědí 201 Created a hlavičkou Location.</returns>
        [HttpPost]
        public ActionResult<RegistrationDto> Create([FromBody] RegistrationCreateDto registrationCreateDto)
        {
            var createdRegistrationDto = registrationManager.CreateRegistration(registrationCreateDto);
            return CreatedAtAction(nameof(Create), new { id = createdRegistrationDto.Id }, createdRegistrationDto);
        }

        /// <summary>
        /// Odstraní registraci podle ID.
        /// </summary>
        /// <param name="id">Identifikátor registrace k odstranění.</param>
        /// <returns>204 NoContent při úspěšném smazání.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            registrationManager.DeleteRegistration(id);
            return NoContent();
        }    
    }
}





