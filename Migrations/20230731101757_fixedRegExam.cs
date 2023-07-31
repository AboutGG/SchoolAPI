using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAPI.Migrations
{
    /// <inheritdoc />
    public partial class fixedRegExam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_registries_exams_exams_id_subject",
                table: "registries_exams");

            migrationBuilder.RenameColumn(
                name: "id_subject",
                table: "registries_exams",
                newName: "id_exam");

            migrationBuilder.AddForeignKey(
                name: "FK_registries_exams_exams_id_exam",
                table: "registries_exams",
                column: "id_exam",
                principalTable: "exams",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_registries_exams_exams_id_exam",
                table: "registries_exams");

            migrationBuilder.RenameColumn(
                name: "id_exam",
                table: "registries_exams",
                newName: "id_subject");

            migrationBuilder.AddForeignKey(
                name: "FK_registries_exams_exams_id_subject",
                table: "registries_exams",
                column: "id_subject",
                principalTable: "exams",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
