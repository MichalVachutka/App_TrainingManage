using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrainingManage.Api.Interfaces;
using TrainingManage.Api.Models.Person;
using TrainingManage.Api.Models.Registration;
using TrainingManage.Data;
using TrainingManage.Data.Models;

namespace TrainingManage.Api.Managers
{
    public class RegistrationManager : IRegistrationManager
    {
        private readonly TrainingDbContext trainingDbContext;
        private readonly IMapper mapper;

        public RegistrationManager(TrainingDbContext trainingDbContext, IMapper mapper)
        {
            this.trainingDbContext = trainingDbContext;
            this.mapper = mapper;
        }
 
        public IList<RegistrationDto> GetAllRegistrations() =>
            mapper.Map<IList<RegistrationDto>>(trainingDbContext.Registrations.ToList());

        public RegistrationDto? GetRegistration(int id)
        {
            var entity = trainingDbContext.Registrations.Find(id);
            return entity == null
                ? null
                : mapper.Map<RegistrationDto>(entity);
        }
        
        public RegistrationDto CreateRegistration(RegistrationCreateDto dto)
        {
            
            var reg = new Registration
            {
                PersonId = dto.PersonId,
                TrainingId = dto.TrainingId,
                Payment = dto.Payment,
                Note = dto.Note,
                Date = DateTime.UtcNow,
                RegisteredOn = DateTime.UtcNow
            };
            trainingDbContext.Registrations.Add(reg);
            trainingDbContext.SaveChanges();

            
            var payTx = new PersonTransaction
            {
                PersonId = reg.PersonId,
                Date = reg.Date,
                Amount = dto.Payment,
                Description = $"Trénink #{reg.TrainingId}",
                TrainingId = reg.TrainingId    
            };
            trainingDbContext.PersonTransactions.Add(payTx);
            trainingDbContext.SaveChanges();
           
            RecalculateRentShare(reg.TrainingId);

            return mapper.Map<RegistrationDto>(reg);
        }

        public RegistrationDto? UpdateRegistration(int id, RegistrationCreateDto dto)
        {
            var existing = trainingDbContext.Registrations.Find(id);
            if (existing == null) return null;

            existing.TrainingId = dto.TrainingId;
            existing.PersonId = dto.PersonId;
            existing.Payment = dto.Payment;
            

            trainingDbContext.SaveChanges();
            return mapper.Map<RegistrationDto>(existing);
        }

        public void DeleteRegistration(int registrationId)
        {
            
            var reg = trainingDbContext.Registrations.Find(registrationId);
            if (reg == null)
                return;

            var trainingId = reg.TrainingId;
            var personId = reg.PersonId;
            var payment = reg.Payment;

            
            trainingDbContext.Registrations.Remove(reg);

            
            var payTx = trainingDbContext.PersonTransactions.FirstOrDefault(t =>
                t.PersonId == personId &&
                t.Description == $"Trénink #{trainingId}" &&
                t.Amount == payment);
            if (payTx != null)
                trainingDbContext.PersonTransactions.Remove(payTx);

            
            trainingDbContext.SaveChanges();

            
            RecalculateRentShare(trainingId);
        }

        private void RecalculateRentShare(int trainingId)
        {
            
            var oldRentTxs = trainingDbContext.PersonTransactions
                .Where(t => t.Description == $"Rent share #{trainingId}")
                .ToList();
            if (oldRentTxs.Any())
                trainingDbContext.PersonTransactions.RemoveRange(oldRentTxs);

            
            var regs = trainingDbContext.Registrations
                .Where(r => r.TrainingId == trainingId)
                .ToList();
            var training = trainingDbContext.Trainings.Find(trainingId);

            
            if (training == null || training.RentCost <= 0 || !regs.Any())
            {
                trainingDbContext.SaveChanges();
                return;
            }

            
            var share = training.RentCost / regs.Count;

            
            foreach (var r in regs)
            {
                trainingDbContext.PersonTransactions.Add(new PersonTransaction
                {
                    PersonId = r.PersonId,
                    Date = DateTime.UtcNow,
                    Amount = -share,
                    Description = $"Rent share #{trainingId}",
                    TrainingId = trainingId   
                });
            }

            
            trainingDbContext.SaveChanges();
        }

        public PersonDetailDto? GetPersonDetail(int personId)
        {
            var person = trainingDbContext.Persons
                .Include(p => p.Registrations)
                   .ThenInclude(r => r.Training)
                .Include(p => p.PersonTransactions)
                .Include(p => p.ExpenseParticipants)
                   .ThenInclude(ep => ep.Expense)
                .FirstOrDefault(p => p.Id == personId);

            if (person == null) return null;

            
            var regs = mapper.Map<List<RegistrationDto>>(person.Registrations);

            
            var txs = mapper.Map<List<PersonTransactionDto>>(person.PersonTransactions)
                          .OrderByDescending(t => t.Date)
                          .ToList();

            
            decimal totalPaid = txs.Where(t => t.Amount > 0).Sum(t => t.Amount);
            decimal paidThisMonth = txs
                .Where(t => t.Amount > 0
                         && t.Date.Month == DateTime.UtcNow.Month
                         && t.Date.Year == DateTime.UtcNow.Year)
                .Sum(t => t.Amount);
            decimal paidThisYear = txs
                .Where(t => t.Amount > 0
                         && t.Date.Year == DateTime.UtcNow.Year)
                .Sum(t => t.Amount);

            
            decimal rentShare = person.PersonTransactions
                .Where(t => t.Description.StartsWith("Rent share #"))
                .Sum(t => -t.Amount);
            decimal equipShare = person.ExpenseParticipants
                .Where(ep => ep.Expense.Type == "Equipment")
                .Sum(ep => ep.ShareAmount);

            
            return new PersonDetailDto
            {
                Person = mapper.Map<PersonDto>(person),
                Registrations = regs,
                TotalPaid = totalPaid,
                PaidThisMonth = paidThisMonth,
                PaidThisYear = paidThisYear,
                TotalRentShare = rentShare,
                TotalEquipmentShare = equipShare
            };
        }
    }
}


