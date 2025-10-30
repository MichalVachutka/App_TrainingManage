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
        private readonly ITrainingManager trainingManager;

        public TrainingsController(ITrainingManager trainingManager)
        {
            this.trainingManager = trainingManager;
        }

        /// <summary>
        /// Vrátí seznam všech tréninků.
        /// </summary>
        [HttpGet]
        public ActionResult<IList<TrainingDto>> GetAll()
        {
            var trainingDtos = trainingManager.GetAllTrainings();
            return Ok(trainingDtos);
        }

        /// <summary>
        /// Vrátí jeden trénink podle id.
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<TrainingDto> Get(int id)
        {
            var trainingDto = trainingManager.GetTraining(id);
            return trainingDto is null ? NotFound() : Ok(trainingDto);
        }

        /// <summary>
        /// Vytvoří nový trénink.
        /// </summary>
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
        [HttpPut("{id}")]
        public ActionResult<TrainingDto> Update(int id, [FromBody] TrainingDto trainingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedTrainingDto = trainingManager.UpdateTraining(id, trainingDto);
            return updatedTrainingDto is null ? NotFound() : Ok(updatedTrainingDto);
        }

        /// <summary>
        /// Smaže trénink podle ID.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                trainingManager.DeleteTraining(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Vrátí detailní informace o tréninku podle id.
        /// </summary>
        [HttpGet("{id}/detail")]
        public ActionResult<TrainingDetailDto> GetDetail(int id)
        {
            var trainingDetailDto = trainingManager.GetTrainingDetail(id);
            return trainingDetailDto is null ? NotFound() : Ok(trainingDetailDto);
        }
    }
}
