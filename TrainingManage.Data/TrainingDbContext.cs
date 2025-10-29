using Microsoft.EntityFrameworkCore;
using TrainingManage.Data.Models;


namespace TrainingManage.Data
{
    /// <summary>
    /// Kontext databáze pro aplikaci TrainingManage, obsahuje DbSety pro jednotlivé entity.
    /// </summary>
    public class TrainingDbContext : DbContext
    {
        /// <summary>
        /// Kolekce osob (tabulka Persons).
        /// </summary>
        public DbSet<Person> Persons { get; set; } = null!;

        /// <summary>
        /// Kolekce tréninků (tabulka Trainings).
        /// </summary>
        public DbSet<Training> Trainings { get; set; } = null!;

        /// <summary>
        /// Kolekce registrací (tabulka Registrations).
        /// </summary>
        public DbSet<Registration> Registrations { get; set; } = null!;

        /// <summary>
        /// Kolekce výdajů (tabulka Expenses).
        /// </summary>
        public DbSet<Expense> Expenses { get; set; } = null!;

        /// <summary>
        /// Kolekce účastníků výdajů (tabulka ExpenseParticipants).
        /// </summary>
        public DbSet<ExpenseParticipant> ExpenseParticipants { get; set; } = null!;

        /// <summary>
        /// Kolekce transakcí osob (tabulka PersonTransactions).
        /// </summary>
        public DbSet<PersonTransaction> PersonTransactions { get; set; } = null!;

        /// <summary>
        /// Konstruktor kontextu s možnostmi konfigurace.
        /// </summary>
        /// <param name="options">Nastavení DbContextu.</param>
        public TrainingDbContext(DbContextOptions<TrainingDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Konfigurace modelu a vztahů mezi entitami při vytváření modelu.
        /// </summary>
        /// <param name="modelBuilder">Builder modelu.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Změna kaskádového mazání na Restrict u všech FK, které mají Cascade
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(type => type.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            // Definice složeného primárního klíče pro ExpenseParticipant (ExpenseId + PersonId)
            modelBuilder.Entity<ExpenseParticipant>()
                   .HasKey(ep => new { ep.ExpenseId, ep.PersonId });

            // Konfigurace vztahu ExpenseParticipant - Expense (mnoho k jednomu)
            modelBuilder.Entity<ExpenseParticipant>()
                   .HasOne(ep => ep.Expense)
                   .WithMany(e => e.Participants)
                   .HasForeignKey(ep => ep.ExpenseId);

            // Konfigurace vztahu ExpenseParticipant - Person (mnoho k jednomu)
            modelBuilder.Entity<ExpenseParticipant>()
                   .HasOne(ep => ep.Person)
                   .WithMany(p => p.ExpenseParticipants)
                   .HasForeignKey(ep => ep.PersonId);
        }
    }
}
