using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingManage.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHidden : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Hidden",
                table: "person",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hidden",
                table: "person");
        }
    }
}
