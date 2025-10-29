using AutoMapper;
using TrainingManage.Api.Interfaces;
using TrainingManage.Api.Models.Person;
using TrainingManage.Api.Models.Registration;
using TrainingManage.Data;
using TrainingManage.Data.Interfaces;
using TrainingManage.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace TrainingManage.Api.Managers
{
    public class PersonManager : IPersonManager
    {
        private readonly IPersonRepository personRepository;
        private readonly IRegistrationRepository registrationRepository;
        private readonly IMapper mapper;
        private readonly TrainingDbContext trainingDbContext;


        public PersonManager(IPersonRepository personRepository, IRegistrationRepository registrationRepository, IMapper mapper, TrainingDbContext trainingDbContext)
        {
            this.trainingDbContext = trainingDbContext;
            this.personRepository = personRepository;
            this.registrationRepository = registrationRepository;
            this.mapper = mapper;
        }

        public IList<PersonDto> GetAllPeople(int page = 0, int pageSize = int.MaxValue)
        {
            IList<Person> people = personRepository.GetAll(page, pageSize);
            return mapper.Map<IList<PersonDto>>(people);
        }

        public PersonDto? GetPeople(int Id)
        {
            Person? people = personRepository.FindById(Id);

            if (people is null)
                return null;

            return mapper.Map<PersonDto>(people);
        }

        public IList<PersonDto> GetAllPeople()
        {
            IList<Person> people = personRepository.GetAllByHidden(false);
            return mapper.Map<IList<PersonDto>>(people);
        }

        public PersonDto AddPeople(PersonDto personDto)
        {
            Person person = mapper.Map<Person>(personDto);
            person.Id = default;
            Person addedPerson = personRepository.Insert(person);

            return mapper.Map<PersonDto>(addedPerson);
        }

        /// <summary>
        /// Metoda pro upravu osoby v databazi
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personDto"></param>
        /// <returns></returns>
        public PersonDto? UpdatePeople(int id, PersonDto personDto)
        {

            Person? dbPerson = personRepository.FindById(id);
            if (dbPerson is null)
                return null;

            mapper.Map(personDto, dbPerson);

            Person updated = personRepository.Update(dbPerson);

            return mapper.Map<PersonDto>(updated);
        }

        public void DeletePeople(int id)
        {
            HidePeople(id);
        }

        private Person? HidePeople(int Id)
        {
            Person? people = personRepository.FindById(Id);

            if (people is null)
                return null;

            people.Hidden = true;
            return personRepository.Update(people);
        }

        public PersonDetailDto? GetPersonDetail(int id)
        {
            var person = trainingDbContext.Persons
                .Include(p => p.Registrations)
                .Include(p => p.PersonTransactions)
                .Include(p => p.ExpenseParticipants)
                   .ThenInclude(ep => ep.Expense)
                .FirstOrDefault(p => p.Id == id);
            if (person == null)
                return null;

            var regDtos = mapper.Map<List<RegistrationDto>>(
                person.Registrations
                      .OrderByDescending(r => r.Date));

            var txDtos = person.PersonTransactions
                .OrderByDescending(t => t.Date)
                .Select(t => new PersonTransactionDto
                {
                    Id = t.Id,
                    Date = t.Date,
                    Amount = t.Amount,
                    Description = t.Description,
                    TrainingId = t.TrainingId,
                    ExpenseId = t.ExpenseId
                })
                .ToList();

            var totalPaid = person.PersonTransactions
                .Where(t => t.Amount > 0)
                .Sum(t => t.Amount);

            var paidThisMonth = person.PersonTransactions
                .Where(t => t.Amount > 0
                         && t.Date.Year == DateTime.UtcNow.Year
                         && t.Date.Month == DateTime.UtcNow.Month)
                .Sum(t => t.Amount);

            var paidThisYear = person.PersonTransactions
                .Where(t => t.Amount > 0
                         && t.Date.Year == DateTime.UtcNow.Year)
                .Sum(t => t.Amount);

            var totalRentShare = person.PersonTransactions
                .Where(t => t.Amount < 0
                         && t.Description.StartsWith("Rent share"))
                .Sum(t => -t.Amount);

            var totalEquipmentShare = person.PersonTransactions
                .Where(t => t.Amount < 0
                         && t.Description.StartsWith("Výdaj #"))
                .Sum(t => -t.Amount);

            var totalExpenseShare = person.PersonTransactions
                .Where(t => t.Amount < 0)
                .Sum(t => -t.Amount);

            var balance = person.PersonTransactions
                .Sum(t => t.Amount);

            return new PersonDetailDto
            {
                Person = mapper.Map<PersonDto>(person),
                Registrations = regDtos,
                PersonTransactions = txDtos,
                TotalPaid = totalPaid,
                PaidThisMonth = paidThisMonth,
                PaidThisYear = paidThisYear,
                TotalRentShare = totalRentShare,
                TotalEquipmentShare = totalEquipmentShare,
                TotalExpenseShare = totalExpenseShare,
                Balance = balance
            };
        }
















        

        //public bool AttendTraining(string userId, int trainingId)
        //{
        //    var person = _ctx.Persons.FirstOrDefault(p => p.ApplicationUserId == userId);
        //    var training = _ctx.Trainings.FirstOrDefault(t => t.Id == trainingId);

        //    if (person == null || training == null)
        //        return false;

        //    bool alreadyRegistered = _ctx.Registrations
        //        .Any(r => r.PersonId == person.Id && r.TrainingId == training.Id);

        //    if (alreadyRegistered)
        //        return true;

        //    _ctx.Registrations.Add(new Registration
        //    {
        //        PersonId = person.Id,
        //        TrainingId = training.Id,
        //        Date = DateTime.UtcNow
        //    });

        //    _ctx.SaveChanges();
        //    return true;
        //}


    }
}
