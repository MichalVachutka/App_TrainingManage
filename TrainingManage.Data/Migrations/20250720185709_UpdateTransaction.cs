using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingManage.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpenseId",
                table: "PersonTransactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainingId",
                table: "PersonTransactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonTransactions_ExpenseId",
                table: "PersonTransactions",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonTransactions_TrainingId",
                table: "PersonTransactions",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonTransactions_Expenses_ExpenseId",
                table: "PersonTransactions",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonTransactions_Trainings_TrainingId",
                table: "PersonTransactions",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonTransactions_Expenses_ExpenseId",
                table: "PersonTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonTransactions_Trainings_TrainingId",
                table: "PersonTransactions");

            migrationBuilder.DropIndex(
                name: "IX_PersonTransactions_ExpenseId",
                table: "PersonTransactions");

            migrationBuilder.DropIndex(
                name: "IX_PersonTransactions_TrainingId",
                table: "PersonTransactions");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "PersonTransactions");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "PersonTransactions");
        }
    }
}
