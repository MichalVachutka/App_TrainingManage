using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingManage.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTraining : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fee",
                table: "Registrations");

            migrationBuilder.AddColumn<decimal>(
                name: "RentCost",
                table: "Trainings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentCost",
                table: "Trainings");

            migrationBuilder.AddColumn<decimal>(
                name: "Fee",
                table: "Registrations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
