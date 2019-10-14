using Microsoft.EntityFrameworkCore.Migrations;

namespace PUC.LDSI.DataBase.Migrations
{
    public partial class AppMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvaOpcoes_AvaliacaoOpcoes_IdAvaliacaoOpcao",
                table: "ProvaOpcoes");

            migrationBuilder.DropIndex(
                name: "IX_ProvaOpcoes_IdAvaliacaoOpcao",
                table: "ProvaOpcoes");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Turmas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AvaliacaoOpcaoId",
                table: "ProvaOpcoes",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Professores",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Professores",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Professores",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Disciplinas",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Materia",
                table: "Avaliacoes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Avaliacoes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Enunciado",
                table: "AvaliacaoQuestoes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "AvaliacaoOpcoes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Alunos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Alunos",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProvaOpcoes_AvaliacaoOpcaoId",
                table: "ProvaOpcoes",
                column: "AvaliacaoOpcaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvaOpcoes_AvaliacaoOpcoes_AvaliacaoOpcaoId",
                table: "ProvaOpcoes",
                column: "AvaliacaoOpcaoId",
                principalTable: "AvaliacaoOpcoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvaOpcoes_AvaliacaoOpcoes_AvaliacaoOpcaoId",
                table: "ProvaOpcoes");

            migrationBuilder.DropIndex(
                name: "IX_ProvaOpcoes_AvaliacaoOpcaoId",
                table: "ProvaOpcoes");

            migrationBuilder.DropColumn(
                name: "AvaliacaoOpcaoId",
                table: "ProvaOpcoes");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Turmas",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Professores",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Professores",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Professores",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Disciplinas",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Materia",
                table: "Avaliacoes",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Avaliacoes",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Enunciado",
                table: "AvaliacaoQuestoes",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "AvaliacaoOpcoes",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Alunos",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Alunos",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.CreateIndex(
                name: "IX_ProvaOpcoes_IdAvaliacaoOpcao",
                table: "ProvaOpcoes",
                column: "IdAvaliacaoOpcao");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvaOpcoes_AvaliacaoOpcoes_IdAvaliacaoOpcao",
                table: "ProvaOpcoes",
                column: "IdAvaliacaoOpcao",
                principalTable: "AvaliacaoOpcoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
