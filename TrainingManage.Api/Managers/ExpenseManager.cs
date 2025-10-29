using Microsoft.EntityFrameworkCore;
using TrainingManage.Api.Interfaces;
using TrainingManage.Api.Models.Expense;
using TrainingManage.Api.Models.ExpenseParticipant;
using TrainingManage.Data;
using TrainingManage.Data.Models;
using AutoMapper;

namespace TrainingManage.Api.Managers
{
    public class ExpenseManager : IExpenseManager
    {
        private readonly TrainingDbContext trainingDbContext;
        private readonly IMapper mapper;

        public ExpenseManager(TrainingDbContext trainingDbContext, IMapper mapper)
        {
            this.trainingDbContext = trainingDbContext;
            this.mapper = mapper;
        }

        public IList<ExpenseDto> GetAllExpenses()
        {
            return mapper.Map<IList<ExpenseDto>>(trainingDbContext.Expenses.ToList());
        }

        public ExpenseDto? GetExpense(int id)
        {
            var entity = trainingDbContext.Expenses.Find(id);
            return entity == null ? null : mapper.Map<ExpenseDto>(entity);
        }


        public ExpenseDto CreateExpense(ExpenseCreateDto dto)
        {

            var expense = new Expense
            {
                Type = dto.Type,
                TotalAmount = dto.TotalAmount,
                Date = DateTime.UtcNow
            };
            trainingDbContext.Expenses.Add(expense);
            trainingDbContext.SaveChanges();  

            var parts = new List<ExpenseParticipant>();
            if (dto.ParticipantIds.Any())
            {
                var share = expense.TotalAmount / dto.ParticipantIds.Count;
                parts = dto.ParticipantIds
                    .Select(pid => new ExpenseParticipant
                    {
                        ExpenseId = expense.Id,
                        PersonId = pid,
                        ShareAmount = share
                    })
                    .ToList();

                trainingDbContext.ExpenseParticipants.AddRange(parts);
                trainingDbContext.SaveChanges();
                expense.Participants = parts;
            }

            foreach (var part in parts)
            {
                var expTx = new PersonTransaction
                {
                    PersonId = part.PersonId,
                    Date = expense.Date,
                    Amount = -part.ShareAmount,
                    Description = $"Výdaj #{expense.Id}",
                    ExpenseId = expense.Id    
                };
                trainingDbContext.PersonTransactions.Add(expTx);
            }
            trainingDbContext.SaveChanges();

            return mapper.Map<ExpenseDto>(expense);
        }


        public ExpenseDto? UpdateExpense(int id, ExpenseCreateDto dto)
        {
            var existing = trainingDbContext.Expenses
                .Include(e => e.Participants)
                .FirstOrDefault(e => e.Id == id);
            if (existing == null) return null;

            existing.Type = dto.Type;
            existing.TotalAmount = dto.TotalAmount;

            var toRemove = existing.Participants
                .Where(ep => !dto.ParticipantIds.Contains(ep.PersonId))
                .ToList();
            trainingDbContext.ExpenseParticipants.RemoveRange(toRemove);

            var currentIds = existing.Participants.Select(ep => ep.PersonId).ToHashSet();
            var toAdd = dto.ParticipantIds
                .Where(pid => !currentIds.Contains(pid))
                .Select(pid => new ExpenseParticipant
                {
                    ExpenseId = id,
                    PersonId = pid,
                    ShareAmount = existing.TotalAmount / dto.ParticipantIds.Count
                });
            trainingDbContext.ExpenseParticipants.AddRange(toAdd);

            trainingDbContext.SaveChanges();

            return mapper.Map<ExpenseDto>(existing);
        }

        public void DeleteExpense(int id)
        {
            var parts = trainingDbContext.ExpenseParticipants
                .Where(ep => ep.ExpenseId == id)
                .ToList();

            var expTxs = trainingDbContext.PersonTransactions
                .Where(t => t.Description == $"Výdaj #{id}")
                .ToList();
            if (expTxs.Any())
                trainingDbContext.PersonTransactions.RemoveRange(expTxs);

            trainingDbContext.ExpenseParticipants.RemoveRange(parts);

            var exp = trainingDbContext.Expenses.Find(id)
                  ?? throw new KeyNotFoundException($"Expense {id} not found");
            trainingDbContext.Expenses.Remove(exp);

            trainingDbContext.SaveChanges();
        }

        public ExpenseDetailDto? GetExpenseDetail(int id)
        {
            var expense = trainingDbContext.Expenses
                .Include(e => e.Participants)
                    .ThenInclude(ep => ep.Person)
                .FirstOrDefault(e => e.Id == id);
            if (expense == null) return null;

            var expDto = mapper.Map<ExpenseDto>(expense);
            var parts = mapper.Map<List<ExpenseParticipantDto>>(expense.Participants);
            var total = parts.Sum(p => p.ShareAmount);

            return new ExpenseDetailDto
            {
                Expense = expDto,
                ParticipantShares = parts,
                TotalShares = total
            };
        }

        public ExpenseParticipantDto CreateParticipant(ExpenseParticipantCreateDto dto)
        {
            var ep = mapper.Map<ExpenseParticipant>(dto);
            trainingDbContext.ExpenseParticipants.Add(ep);
            trainingDbContext.SaveChanges();

            var trx = new PersonTransaction
            {
                PersonId = dto.PersonId,
                Date = DateTime.UtcNow,
                Amount = -ep.ShareAmount,
                Description = $"Výdaj #{ep.ExpenseId}: {ep.Expense.Type}"
            };
            trainingDbContext.PersonTransactions.Add(trx);
            trainingDbContext.SaveChanges();
                
            return mapper.Map<ExpenseParticipantDto>(ep);
        }

        public void DeleteParticipant(int expenseId, int personId)
        {
            var toRemove = trainingDbContext.ExpenseParticipants
                .FirstOrDefault(ep => ep.ExpenseId == expenseId
                                   && ep.PersonId == personId);
            if (toRemove != null)
            {
                var trx = trainingDbContext.PersonTransactions.FirstOrDefault(t =>
                    t.PersonId == personId
                 && t.Description == $"Výdaj #{expenseId}"
                 && t.Amount == -toRemove.ShareAmount);
                if (trx != null)
                    trainingDbContext.PersonTransactions.Remove(trx);

                trainingDbContext.ExpenseParticipants.Remove(toRemove);
                trainingDbContext.SaveChanges();
            }

            var remaining = trainingDbContext.ExpenseParticipants
                .Where(ep => ep.ExpenseId == expenseId)
                .ToList();

            if (!remaining.Any())
            {
                return;
            }

            var exp = trainingDbContext.Expenses.Find(expenseId)
                  ?? throw new KeyNotFoundException($"Expense {expenseId} not found");

            var newShare = exp.TotalAmount / remaining.Count;

            foreach (var ep in remaining)
            {
                ep.ShareAmount = newShare;

                var rentTx = trainingDbContext.PersonTransactions.FirstOrDefault(t =>
                    t.PersonId == ep.PersonId
                 && t.Description == $"Výdaj #{expenseId}"
                 && t.Amount < 0);
                if (rentTx != null)
                {
                    rentTx.Amount = -newShare;
                }
                else
                {
                    trainingDbContext.PersonTransactions.Add(new PersonTransaction
                    {
                        PersonId = ep.PersonId,
                        Date = DateTime.UtcNow,
                        Amount = -newShare,
                        Description = $"Výdaj #{expenseId}"
                    });
                }
            }

            trainingDbContext.SaveChanges();
        }

        public ExpenseParticipantDto AddParticipant(int expenseId, ExpenseParticipantCreateDto dto)
        {
            var ep = new ExpenseParticipant
            {
                ExpenseId = expenseId,
                PersonId = dto.PersonId,
                ShareAmount = dto.ShareAmount
            };
            trainingDbContext.ExpenseParticipants.Add(ep);
            trainingDbContext.SaveChanges();

            var trx = new PersonTransaction
            {
                PersonId = ep.PersonId,
                Date = DateTime.UtcNow,
                Amount = -ep.ShareAmount,
                Description = $"Výdaj #{expenseId}"
            };
            trainingDbContext.PersonTransactions.Add(trx);
            trainingDbContext.SaveChanges();

            return mapper.Map<ExpenseParticipantDto>(ep);
        }
    }
}


