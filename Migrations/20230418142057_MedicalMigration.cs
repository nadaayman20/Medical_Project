using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doctor_Appointment.Migrations
{
    /// <inheritdoc />
    public partial class MedicalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkDays",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkDays",
                table: "Doctors");
        }
    }
}
