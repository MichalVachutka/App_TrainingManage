using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingManage.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_registration_person_PersonId",
                table: "registration");

            migrationBuilder.DropForeignKey(
                name: "FK_registration_training_TrainingId",
                table: "registration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_training",
                table: "training");

            migrationBuilder.DropPrimaryKey(
                name: "PK_registration",
                table: "registration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_person",
                table: "person");

            migrationBuilder.RenameTable(
                name: "training",
                newName: "Trainings");

            migrationBuilder.RenameTable(
                name: "registration",
                newName: "Registrations");

            migrationBuilder.RenameTable(
                name: "person",
                newName: "Persons");

            migrationBuilder.RenameIndex(
                name: "IX_registration_TrainingId",
                table: "Registrations",
                newName: "IX_Registrations_TrainingId");

            migrationBuilder.RenameIndex(
                name: "IX_registration_PersonId",
                table: "Registrations",
                newName: "IX_Registrations_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainings",
                table: "Trainings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Persons_PersonId",
                table: "Registrations",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Trainings_TrainingId",
                table: "Registrations",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Persons_PersonId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Trainings_TrainingId",
                table: "Registrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainings",
                table: "Trainings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.RenameTable(
                name: "Trainings",
                newName: "training");

            migrationBuilder.RenameTable(
                name: "Registrations",
                newName: "registration");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "person");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_TrainingId",
                table: "registration",
                newName: "IX_registration_TrainingId");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_PersonId",
                table: "registration",
                newName: "IX_registration_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_training",
                table: "training",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_registration",
                table: "registration",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_person",
                table: "person",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_registration_person_PersonId",
                table: "registration",
                column: "PersonId",
                principalTable: "person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_registration_training_TrainingId",
                table: "registration",
                column: "TrainingId",
                principalTable: "training",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
