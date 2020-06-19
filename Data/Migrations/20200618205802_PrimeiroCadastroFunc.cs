using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace gestao.Migrations
{
    public partial class PrimeiroCadastroFunc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    FuncionarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    matricula = table.Column<string>(maxLength: 10, nullable: false),
                    nome = table.Column<string>(maxLength: 100, nullable: false),
                    cargo = table.Column<string>(maxLength: 50, nullable: false),
                    setor = table.Column<string>(maxLength: 50, nullable: false),
                    email = table.Column<string>(maxLength: 50, nullable: false),
                    datanomeacao = table.Column<DateTimeOffset>(nullable: false),
                    dataposse = table.Column<DateTimeOffset>(nullable: false),
                    dataexercicio = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.FuncionarioId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funcionarios");
        }
    }
}
