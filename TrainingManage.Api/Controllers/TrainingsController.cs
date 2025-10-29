using Microsoft.AspNetCore.Mvc;
using TrainingManage.Api.Interfaces;
using TrainingManage.Api.Models.Training;

namespace TrainingManage.Api.Controllers
{

    /// <summary>
    /// API kontroler pro správu tréninků a získávání jejich detailních informací.
    /// </summary>
    [ApiController]                  
    [Route("api/trainings")]
    public class TrainingsController : ControllerBase
    {

        /// <summary>
        /// Správce tréninků poskytující operace pro CRUD a získávání detailů tréninků.
        /// </summary>
        private readonly ITrainingManager trainingManager;

        /// <summary>
        /// Inicializuje instanci <see cref="TrainingsController"/> s implementací správce tréninků.
        /// </summary>
        /// <param name="trainingManager">Injektovaná implementace <see cref="ITrainingManager"/>.</param>
        public TrainingsController(ITrainingManager trainingManager)
        {
            this.trainingManager = trainingManager;
        }

        /// <summary>
        /// Vrátí seznam všech tréninků.
        /// </summary>
        /// <returns>Seznam <see cref="TrainingDto"/>.</returns>
        [HttpGet]                     
        public ActionResult<IList<TrainingDto>> GetAll()
        {
            var trainingDtos = trainingManager.GetAllTrainings();
            return Ok(trainingDtos);
        }

        /// <summary>
        /// Vrátí jeden trénink podle id.
        /// </summary>
        /// <param name="id">Identifikátor tréninku.</param>
        /// <returns><see cref="TrainingDto"/> pokud existuje, jinak 404 NotFound.</returns>
        [HttpGet("{id}")]            
        public ActionResult<TrainingDto> Get(int id)
        {
            var trainingDto = trainingManager.GetTraining(id);
            return trainingDto is null ? NotFound() : Ok(trainingDto);
        }

        /// <summary>
        /// Vytvoří nový trénink.
        /// </summary>
        /// <param name="trainingDto">Data tréninku k vytvoření.</param>
        /// <returns>Vytvořený <see cref="TrainingDto"/> s odpovědí 201 Created a hlavičkou Location.</returns>
        [HttpPost]                    
        public ActionResult<TrainingDto> Create([FromBody] TrainingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdTrainingDto = trainingManager.CreateTraining(dto);
            return CreatedAtAction(nameof(Get), new { id = createdTrainingDto.Id }, createdTrainingDto);
        }

        /// <summary>
        /// Aktualizuje existující trénink.
        /// </summary>
        /// <param name="id">Identifikátor tréninku, který se má aktualizovat.</param>
        /// <param name="trainingDto">Data pro aktualizaci tréninku.</param>
        /// <returns>Aktualizovaný <see cref="TrainingDto"/> pokud aktualizace uspěje, jinak 404 NotFound.</returns>
        [HttpPut("{id}")]             
        public ActionResult<TrainingDto> Update(int id, [FromBody] TrainingDto trainingDto)
        {
            var updatedTrainingDto = trainingManager.UpdateTraining(id, trainingDto);
            return updatedTrainingDto is null ? NotFound() : Ok(updatedTrainingDto);
        }

        /// <summary>
        /// Smaže trénink podle ID.
        /// </summary>
        /// <param name="id">Identifikátor tréninku, který se má smazat.</param>
        /// <returns>204 NoContent při úspěšném smazání.</returns>
        [HttpDelete("{id}")]          
        public IActionResult Delete(int id)
        {
            trainingManager.DeleteTraining(id);
            return NoContent();
        }

        /// <summary>
        /// Vrátí detailní informace o tréninku podle id.
        /// </summary>
        /// <param name="id">Identifikátor tréninku.</param>
        /// <returns><see cref="TrainingDetailDto"/> pokud existuje, jinak 404 NotFound.</returns>
        [HttpGet("{id}/detail")]
        public ActionResult<TrainingDetailDto> GetDetail(int id)
        {
            var trainingDetailDto = trainingManager.GetTrainingDetail(id);
            return trainingDetailDto is null
                ? NotFound()
                : Ok(trainingDetailDto);
        }
    }
}
