using AutoMapper;
using TrainingManage.Api.Interfaces;
using TrainingManage.Api.Models.Registration;
using TrainingManage.Api.Models.Training;
using TrainingManage.Data.Interfaces;
using TrainingManage.Data.Models;

namespace TrainingManage.Api.Managers
{
    /// <summary>
    /// Manager pro logiku nad entitou Training.
    /// </summary>
    public class TrainingManager : ITrainingManager
    {
        private readonly ITrainingRepository trainingRepository;
        private readonly IMapper mapper;

        public TrainingManager(ITrainingRepository trainingRepository, IMapper mapper)
        {
            this.trainingRepository = trainingRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vrátí všechny tréninky.
        /// </summary>
        public IList<TrainingDto> GetAllTrainings()
        {
            var trainingEntities = trainingRepository.GetAll();
            return mapper.Map<IList<TrainingDto>>(trainingEntities);
        }

        /// <summary>
        /// Najde trénink podle id.
        /// </summary>
        public TrainingDto? GetTraining(int id)
        {
            var trainingEntity = trainingRepository.FindById(id);
            return trainingEntity is null ? null : mapper.Map<TrainingDto>(trainingEntity);
        }

        /// <summary>
        /// Vytvoří nový trénink.
        /// </summary>
        public TrainingDto CreateTraining(TrainingDto trainingDto)
        {
            var trainingToCreate = mapper.Map<Training>(trainingDto);
            trainingToCreate.Id = default;
            var createdTrainingEntity = trainingRepository.Insert(trainingToCreate);
            return mapper.Map<TrainingDto>(createdTrainingEntity);
        }

        /// <summary>
        /// Aktualizuje existující trénink. Vrací null pokud entita neexistuje.
        /// </summary>
        public TrainingDto? UpdateTraining(int id, TrainingDto trainingDto)
        {
            var existingTraining = trainingRepository.FindById(id);
            if (existingTraining == null)
                return null;

            existingTraining.Title = trainingDto.Title;
            existingTraining.Date = trainingDto.Date;
            existingTraining.Notes = trainingDto.Notes;

            var updatedTrainingEntity = trainingRepository.Update(existingTraining);
            return mapper.Map<TrainingDto>(updatedTrainingEntity);
        }

        /// <summary>
        /// Smaže trénink a související záznamy.
        /// </summary>
        public void DeleteTraining(int id)
        {
            trainingRepository.DeleteWithDependencies(id);
        }

        /// <summary>
        /// Vrátí detail tréninku včetně registrací a (participant count, totals).
        /// Vrací null pokud training neexistuje.
        /// </summary>
        public TrainingDetailDto? GetTrainingDetail(int id)
        {
            var trainingWithRegistrations = trainingRepository.GetWithRegistrationsAndPersons(id);
            if (trainingWithRegistrations == null)
                return null;

            var trainingDto = mapper.Map<TrainingDto>(trainingWithRegistrations);
            var registrationDtos = mapper.Map<List<RegistrationDto>>(trainingWithRegistrations.Registrations ?? new List<Registration>());

            var participantCount = registrationDtos.Count;
            var totalCollected = registrationDtos.Sum(r => r.Payment);

            var trainingDetailDto = new TrainingDetailDto
            {
                Training = trainingDto,
                Registrations = registrationDtos,
                ParticipantCount = participantCount,
                TotalCollected = totalCollected,
                RentCost = trainingWithRegistrations.RentCost
            };

            return trainingDetailDto;
        }
    }
}
