using Microsoft.EntityFrameworkCore.Migrations;

namespace PUC.LDSI.DataBase.Migrations
{
    public partial class AppMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Matricula",
                table: "Alunos");

            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Alunos",
                newName: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Alunos",
                newName: "Senha");

            migrationBuilder.AddColumn<int>(
                name: "Matricula",
                table: "Alunos",
                nullable: false,
                defaultValue: 0);
        }
    }
}
