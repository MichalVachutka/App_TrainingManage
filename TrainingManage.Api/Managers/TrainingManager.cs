using TrainingManage.Api.Interfaces;
using TrainingManage.Data.Interfaces;
using TrainingManage.Data.Models;
using AutoMapper;
using TrainingManage.Data;
using TrainingManage.Api.Models.Training;
using TrainingManage.Api.Models.Registration;
using Microsoft.EntityFrameworkCore;

namespace TrainingManage.Api.Managers
{
    public class TrainingManager : ITrainingManager
    {
        private readonly ITrainingRepository trainingRepository;
        private readonly TrainingDbContext trainingDbContext;
        private readonly IMapper mapper;

    public TrainingManager(ITrainingRepository trainingRepository, TrainingDbContext trainingDbContext, IMapper mapper)
        {
            this.trainingRepository = trainingRepository;        
            this.trainingDbContext = trainingDbContext;
            this.mapper = mapper;
        }

        public IList<TrainingDto> GetAllTrainings()
        {
            var trainings = trainingRepository.GetAll();
            return mapper.Map<IList<TrainingDto>>(trainings);
        }

        public TrainingDto? GetTraining(int id)
        {
            var training = trainingRepository.FindById(id);
            return training is null ? null : mapper.Map<TrainingDto>(training);
        }

        public TrainingDto CreateTraining(TrainingDto trainingDto)
        {
            var trainingEntity = mapper.Map<Training>(trainingDto);
            trainingEntity.Id = default;
            var created = trainingRepository.Insert(trainingEntity);
            return mapper.Map<TrainingDto>(created);
        }

        public TrainingDto UpdateTraining(int id, TrainingDto dto)
        {
            var entity = trainingRepository.FindById(id);
            if (entity == null)
                return null;

            entity.Title = dto.Title;
            entity.Date = dto.Date;
            entity.Notes = dto.Notes;

            var updated = trainingRepository.Update(entity);

            return mapper.Map<TrainingDto>(updated);
        }

        public void DeleteTraining(int id)
        {
            var training = trainingDbContext.Trainings
                .Include(t => t.Registrations)
                .FirstOrDefault(t => t.Id == id)
                ?? throw new KeyNotFoundException($"Training {id} not found");

            var payTxs = trainingDbContext.PersonTransactions
                .Where(t => t.Description == $"Trénink #{id}")
                .ToList();
            if (payTxs.Any())
                trainingDbContext.PersonTransactions.RemoveRange(payTxs);

            var rentTxs = trainingDbContext.PersonTransactions
                .Where(t => t.Description == $"Rent share #{id}")
                .ToList();
            if (rentTxs.Any())
                trainingDbContext.PersonTransactions.RemoveRange(rentTxs);

            trainingDbContext.Registrations.RemoveRange(training.Registrations);

            trainingDbContext.Trainings.Remove(training);

            trainingDbContext.SaveChanges();
        }

        public TrainingDetailDto? GetTrainingDetail(int id)
        {

            var trainingEntity = trainingDbContext.Trainings
                .Include(t => t.Registrations)
                    .ThenInclude(r => r.Person)
                .FirstOrDefault(t => t.Id == id);

            if (trainingEntity == null)
                return null;

            var trainingDto = mapper.Map<TrainingDto>(trainingEntity);

            var registrationDtos = mapper
                .Map<List<RegistrationDto>>(trainingEntity.Registrations);

            var participantCount = registrationDtos.Count;
            var totalCollected = registrationDtos.Sum(r => r.Payment);

            var detailDto = new TrainingDetailDto
            {
                Training = trainingDto,
                Registrations = registrationDtos,
                ParticipantCount = participantCount,
                TotalCollected = totalCollected,
                RentCost = trainingEntity.RentCost

            };

            return detailDto;
        }
    }
}

